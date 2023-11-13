using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAIMove : MonoBehaviour
{

    public SelectPoint selectPoint;
    public List<Transform> nextpoint;
    public NPCPooling npcPooling;
    public float maxdistance;
    public float mindistance;
    public float mintime;
    public float maxtime;
    private float time;
    public float timedelay;
    public Transform _nextpoint;

    //void Start()
    //{
    //    Init();      
    //}

    public void Init()
    {
        _nextpoint = RandomNextPoint();
        time = Random.Range(mintime, maxtime);
        //Invoke("SpawnNPCCouroutine", time);
        StartCoroutine(SpawnNPCCouroutine());
    }

    public Transform RandomNextPoint()
    {
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
        yield return new WaitForSeconds(time);

     
        float dis = Vector3.Distance(transform.position, Player.ins.transform.position);
        time = Random.Range(mintime, maxtime);
        //Invoke("SpawnNPCCouroutine", time);

        if (dis < mindistance || dis > maxdistance || !NPCManager.ins.CheckCanSpawn())
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
        else if (selectPoint == SelectPoint.Drive)
        {
            for(int i = 0; i < npcPooling.CarPool.Count; i++)
            {
                if (Vector3.Distance(transform.position, npcPooling.CarPool[i].transform.position) < 10)
                {
                    npcPooling.ReturnPool(npcControl.gameObject, 0f);
                    StartCoroutine(SpawnNPCCouroutine());
                    yield break;
                }
            }

            if (SpawnNPCInit.ins.onVehicles < SpawnNPCInit.ins.maxOnVehicles)
            {
               
                npcControl.npcState.ChangeState(SelectState.Driver);

                if (npcControl.transform.parent != null)
                {
                    npcControl.transform.parent.GetComponent<Car>().npcDrive = npcControl;
                   
                }
                else
                {
                    npcPooling.ReturnPool(npcControl.gameObject, 0f);
                    
                }

            }
            else
            {
                
                npcPooling.ReturnPool(npcControl.gameObject, 0f);
               
            }


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