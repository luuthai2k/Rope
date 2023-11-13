using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataQuest", menuName = "Quest/Quest")]
public class DataQuest : ScriptableObject
{
    public int indexEnemy, indexCar,indexPickup,indexStar,indexPoint;
    //public List<GameObject> checkPoint;
    public int timeQuest;
    //public int indexQuest;
    public int reward;
    public string achevement;
}
