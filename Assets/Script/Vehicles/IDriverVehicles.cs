using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDriverVehicles
{
    GameObject _vehicles { get; set; }
    List<Transform> _enterFormPos { get; set; }
    GameObject _driver { get; set; }
    Transform _driverSit { get; set; }
    Transform _exitForce { get; set; }
    Transform _camtarget { get; set; }
    Rigidbody _rb { get; set; }
    float _maxspeed { get; set; }
    VehicleSensor _sensor { get; set; }
    void DriverVehicles(float acceleration = 0, float vertical = 0, float horizontal = 0, float maxspeed = 0);

}
