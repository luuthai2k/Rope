using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Charactor", menuName = "Charactor/CharactorData")]
public class ChacractorData : ScriptableObject
{
    [Header("CharactorInfo")]
    public SelectRole role;
    [Header("Weapon")]
    public SelectWeapon meleeWeapons;
    public SelectWeapon rangedWeapons; 
    public GameObject meleeWeapon;
    public GameObject rangedWeapon;
    [Header("Vehicle")]
    public SelectVehicles vehicle;
    public GameObject vehiclePrefab;
    public float speed;
    public float maxspeed;
    public float speedRotate;
    public float dame;
    public float rangeDame;

}
public enum SelectRole
{
    Civilian,
    Police,
    Quest,
    Gangster
}
public enum SelectVehicles
{
    Motor,
    Car,
    Helicoptor,
    Tank,
    Bicycle,



}

