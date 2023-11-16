using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckZoneCanSpawn : MonoBehaviour
{
    public GameObject pointsSpawn;
    void OnBecameVisible()
    {
        pointsSpawn.SetActive(true);
        Debug.Log("on");
    }

    void OnBecameInvisible()
    {
        pointsSpawn.SetActive(false);
        Debug.Log("off");
    }
}
