using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCPool 
{
    [SerializeField] List<GameObject> NPC = new List<GameObject>();

    [SerializeField] List<GameObject> NPCPooling = new List<GameObject>();

    public abstract void RandomNPC();
    public abstract void GetPool();
    public abstract void ReturnPool();
}
