using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensorHuman : IHumanSensor
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.ins.layerData.HumanLayer == (GameManager.ins.layerData.HumanLayer | (1 << other.gameObject.layer)))
        {
            if (other.gameObject.transform.parent.gameObject == transform.parent.gameObject) return;
            ObjectCollision = other.gameObject.transform.parent.gameObject;

        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.parent.gameObject == ObjectCollision)
        {

            ObjectCollision = null;
        }
    }
}
