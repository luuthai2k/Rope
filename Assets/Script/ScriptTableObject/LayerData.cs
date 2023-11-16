using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Layer", menuName = "Layer/LayerData")]
public class LayerData : ScriptableObject
{
    public LayerMask HumanLayer;
    public LayerMask MetalLayer;
    public LayerMask StoneLayer;
    public LayerMask WoodLayer;
    public LayerMask VehiclesLayer;
    public LayerMask ObstacleLayer;
    public LayerMask Ground;
    public LayerMask PlayerSensor;
    public LayerMask NPCSensor;
    public LayerMask Surface;
    public LayerMask AimColliderMask;
    public LayerMask WallMask;
    public LayerMask Trafficlight;

}
