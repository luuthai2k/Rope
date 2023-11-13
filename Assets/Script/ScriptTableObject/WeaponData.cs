using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/WeaponData")]
public class WeaponData : ScriptableObject
{
    public SelectWeapon _weapon;
    public string _name;
    public float _dame;
    public float _fireRate;
    public float _startTime;
    public float _clipSize;
    public float _power;
    public GameObject _bullet;

}
