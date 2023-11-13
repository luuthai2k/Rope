using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Vehicles", menuName = "VehiclesType")]
public class VehiclesType : ScriptableObject
{
    public SelectVehicles selectVehicles;
    public List<GameObject> vehicles;

}
public enum CarType
{
    Classic,
    Sport,
    Truck,
    Van,
    Tank



}
public enum MotorType
{
    Bycicklet,
    Skull,
    Sport,
    Chopper,
    Mountain


}
