using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNpcManager : MonoBehaviour
{
    public static SpawnNpcManager ins;
    public float maxdistance;
    public float mindistance;
    public float mintime;
    public float maxtime;
    public float timedelay;
    private void Awake()
    {
        ins = this;
    }
}
