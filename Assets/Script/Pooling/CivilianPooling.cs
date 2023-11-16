using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class CivilianPooling : MonoBehaviour
{
    public List<GameObject> Npc = new List<GameObject>();
    [Header("Move")]
    [SerializeField] List<GameObject> NPCPoolMove = new List<GameObject>();
    public int maxNpcMove;
    public int totalNpcMove;
    [Header("Driver")]
    [SerializeField] List<GameObject> NPCPoolDriver = new List<GameObject>();
    public int maxNpcDriver;
    public int totalNpcDriver;
    private int index;


    public void  RandomNPC()
    {
        index = Random.Range(0, Npc.Count);

    }
    public GameObject GetPoolMove(Vector3 pos,Transform nextpoint,Transform lastpoint)
    {
        GameObject newNpc= GetPool(pos, NPCPoolMove, maxNpcMove);
          if (newNpc == null) return null;
        NPCControl npcControl = newNpc.GetComponent<NPCControl>();
        npcControl.pointtarget = nextpoint;
        npcControl.npcState.ChangeState(SelectState.Move);
        newNpc.transform.LookAt(nextpoint);
        npcControl.lastpoint = lastpoint;
        return newNpc;
    }

    public GameObject GetPoolDriver(Vector3 pos, Transform nextpoint, Transform lastpoint)
    {

        GameObject newNpc = GetPool(pos, NPCPoolDriver, maxNpcDriver);
        if (newNpc == null) return null;
        NPCControl npcControl = newNpc.GetComponent<NPCControl>();
        npcControl.pointtarget = nextpoint;
        npcControl.npcState.ChangeState(SelectState.Driver);
        newNpc.transform.LookAt(nextpoint);
        npcControl.lastpoint = lastpoint;
        return newNpc;
    }
    public  GameObject GetPool(Vector3 pos ,List<GameObject> NPCPool, float maxNpc)
    {
        foreach (var npc in NPCPool)
        {
            if (!npc.activeSelf)
            {
                npc.SetActive(true);
                npc.transform.position = pos;

                NPCControl NPC = npc.GetComponent<NPCControl>();
                NPC.npcHp.hp = 100;
                NPC.npcHp.isdead = false;
                return npc;
            }
        }
        RandomNPC();
        if (NPCPool.Count >= maxNpc)
        {
            return null;
        }

        GameObject newnpc = Instantiate(Npc[index], pos, Quaternion.identity);
        newnpc.GetComponent<NPCControl>().npcHp.isPolice = false;
        NPCPool.Add(newnpc);
        return newnpc;
    }

    public  void ReturnPool()
    {
        // Triển khai logic ReturnPool cho CivilianPooling
    }
}
