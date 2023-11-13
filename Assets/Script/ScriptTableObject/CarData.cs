using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CarData", menuName = "Vehicles/CarData")]
public class CarData : ScriptableObject
{
    public CarType carType;
    [Header("Vehicle Force")]
    public float maxspeed;
    public float accelerationForce;
    [Header("Vehicle Steering")]
    public float wheelsTorque = 20f;
    [Header("Vehicle breaking")]
    public float breakingForce = 500f;
    public float breakingForceMax = 1000f;
}
