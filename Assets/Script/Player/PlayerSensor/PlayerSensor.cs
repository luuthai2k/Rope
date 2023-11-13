using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    [SerializeField] PlayerSensorHuman human;
    [SerializeField] PlayerSensorVehicles vehicle;

    public GameObject ReturnHuman()
    {
        if (human.ObjectCollision != null)
        {
            return human.ObjectCollision;
        }
        return null;
    }
    public GameObject ReturnVehicle()
    {
        if (vehicle.ObjectCollision != null)
        {
            return vehicle.ObjectCollision;
        }
        return null;
    }

}

