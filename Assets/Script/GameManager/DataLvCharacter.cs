using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/UpgradeCharacter")]
public class DataLvCharacter : ScriptableObject
{
    public int maxHp;
    public int maxStamina;
    public int maxArmor;
    public int maxLv;
    public int maxXp;
}
