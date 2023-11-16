using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public static PoolingManager ins;
    public FxPooling fxPooling;
    public BulletPooling bulletPooling;
    public FxShotPooling fxShotPooling;
    public NPCPooling npcPooling;
    public CivilianPooling civilianPooling;
    void Awake()
    {
        ins = this;
    }
}
