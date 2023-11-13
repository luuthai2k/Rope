using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager ins;
    public NPCPooling npcPooling;
    public float maxdistance=200;
    public float maxNpc;
    public float totalnpc;
    public List<NpcInfo> npcList = new List<NpcInfo>();
    public int maxOnNPC;

    void Awake()
    {
        ins = this;
    }

    public bool CheckCanSpawn()
    {

        if (totalnpc >= maxNpc)
        {
            return false;
        }
        else return true;
    }


}
[System.Serializable]  
public class NpcInfo
{
    public SelectRole selectRole;
    public NPCPooling npcPooling;
    public float maxNpc;
    public float totalNPC;
}


