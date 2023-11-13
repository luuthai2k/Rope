using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnQuestStart : MonoBehaviour
{
    [SerializeField]
    private List<Transform> startPoint;

    public Transform Pos()
    {
        int index = Random.Range(0, startPoint.Count);

        return startPoint[index];
    }
}
