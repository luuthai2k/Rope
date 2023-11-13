using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MotorData", menuName = "Vehicles/MotorData")]
public class MotorData : ScriptableObject
{
    public MotorType motorType;
    [Header("Vehicle Force")]
    public float maxspeed;
    public float accelerationForce;
    [Header("Vehicle Steering")]
    public float wheelsTorque = 20f;
    [Header("Vehicle breaking")]
    public float breakingForce = 500f;
    public float breakingForceMax = 1000f;
    [Header("Vehicle Tilting")]
    public float ForwardtiltFoce;
    public float TurntiltFoce;

}
