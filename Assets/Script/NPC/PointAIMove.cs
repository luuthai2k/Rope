using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAIMove : MonoBehaviour
{

    public SelectPoint selectPoint;
    public SelectState currentState;
    public List<Transform> nextpoint;
    public NPCPooling npcPooling;
    private float time;
    public Transform _nextpoint;
    private GameObject newNpc;
    public bool canSpawn;

    private void OnEnable()
    {
        time = Random.Range(SpawnNpcManager.ins.mintime, SpawnNpcManager.ins.maxtime);
        Invoke("SpawnNpc", time);
        //StartCoroutine(SpawnNPCCouroutine());
    }
    public void SpawnNpc()
    {
        if (!canSpawn) return;
        Debug.Log("spawn");
        Transform nextpoint = RandomNextPoint();
        float dis = Vector3.Distance(transform.position, Player.ins.transform.position);
        if (dis < SpawnNpcManager.ins.mindistance || dis > SpawnNpcManager.ins.maxdistance) return;
        if (currentState == SelectState.Move)
        {
            newNpc = PoolingManager.ins.civilianPooling.GetPoolMove(transform.position,nextpoint,transform);
            if (newNpc == null) return;

        }
        else if (currentState == SelectState.Driver)
        {
            newNpc = PoolingManager.ins.civilianPooling.GetPoolDriver(transform.position,nextpoint,transform);
            if (newNpc == null) return;
        }
    }
    public void ContinueSpawn()
    {
        CancelInvoke("SpawnNPC");
        if (gameObject.activeSelf)
        {
            Invoke("SpawnNPC", SpawnNpcManager.ins.timedelay);
        }
        else
        {
            return;
        }
       
    }
    public void Init()
    {
        //_nextpoint = RandomNextPoint();
        time = Random.Range(SpawnNpcManager.ins.mintime, SpawnNpcManager.ins.maxtime);
        //StartCoroutine(SpawnNPCCouroutine());
    }

    public Transform RandomNextPoint()
    {
        ContinueSpawn();
        if (nextpoint.Count > 0)
        {

            int randomIndex = Random.Range(0, nextpoint.Count);

           
            return nextpoint[randomIndex];
        }
        else
        {

            return null;
        }
    }
    public IEnumerator SpawnNPCCouroutine()
    {
        if (selectPoint == SelectPoint.Drive)
        {
            yield break;
        }
        yield return new WaitForSeconds(time);

            float dis = Vector3.Distance(transform.position, Player.ins.transform.position);
        time = Random.Range(SpawnNpcManager.ins.mintime, SpawnNpcManager.ins.maxtime);
        //Invoke("SpawnNPCCouroutine", time);

        if (dis < SpawnNpcManager.ins.mindistance || dis > SpawnNpcManager.ins.maxdistance || !NPCManager.ins.CheckCanSpawn())
        {
            StartCoroutine(SpawnNPCCouroutine());
            yield break;
        }
        GameObject newnpc = npcPooling.GetPool(transform.position);


        if (newnpc == null)
        {
            StartCoroutine(SpawnNPCCouroutine());
            yield break;
            
        }

        NPCControl npcControl = newnpc.GetComponent<NPCControl>();

        npcControl.pointtarget = RandomNextPoint();
        newnpc.transform.LookAt(_nextpoint);
        npcControl.lastpoint = transform;


        if (selectPoint == SelectPoint.Walk)
        {
            npcControl.npcState.ChangeState(SelectState.Move);
            

        }
        StartCoroutine(SpawnNPCCouroutine());
        yield break;

    }

}
public enum SelectPoint
{
    Walk,
    Drive
}