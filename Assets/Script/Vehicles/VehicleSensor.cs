using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VehicleSensor : MonoBehaviour
{
    public IDriverVehicles driverVehicles;
    public bool collisionwithhuman;
    public bool collisionwithobject;
    public bool collisionwithvehicles;
    public bool collisionwithtrafficlight;
    public bool cancheckcollision;
    public GameObject objcollisionwithhuman;
    public GameObject objcollisionwithobject;
    public GameObject objcollisionwithvehicles;
    public GameObject objcollisionwithtrafficlight;

    private void Start()
    {
        driverVehicles = transform.parent.GetComponent<IDriverVehicles>();
    }
    public void CheckExitTrafficlight()
    {
        if (objcollisionwithtrafficlight.activeSelf)
        {
            Invoke("CheckExitTrafficlight", 2);
        }
        else
        {
            collisionwithtrafficlight = false;
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {

        if (driverVehicles._driver == null) return;
        if (GameManager.ins.layerData.HumanLayer == (GameManager.ins.layerData.HumanLayer | (1 << other.gameObject.layer)))
        {
            
            if (other.gameObject.transform.parent.gameObject == driverVehicles._driver) return;
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.GetComponent<PlayerSensorVehicles>() != null)
                {
                    collisionwithhuman = true;
                    objcollisionwithhuman = Player.ins.gameObject;
                }
            }
            else
            {
                collisionwithhuman = true;
                objcollisionwithhuman = other.gameObject.transform.parent.gameObject;
            }

        }
        else if (GameManager.ins.layerData.VehiclesLayer == (GameManager.ins.layerData.VehiclesLayer | (1 << other.gameObject.layer)))
        {
            collisionwithvehicles = true;
            objcollisionwithvehicles = other.gameObject.transform.parent.gameObject;
        }
        else if (GameManager.ins.layerData.ObstacleLayer == (GameManager.ins.layerData.ObstacleLayer | (1 << other.gameObject.layer)))
        {
            collisionwithobject = true;
            objcollisionwithobject = other.gameObject.transform.parent.gameObject;

        }
        else if (GameManager.ins.layerData.Trafficlight == (GameManager.ins.layerData.Trafficlight | (1 << other.gameObject.layer)))
        {
            collisionwithtrafficlight = true;
            objcollisionwithtrafficlight = other.gameObject.transform.parent.gameObject;
            Invoke("CheckExitTrafficlight", 2);
        }


    }

    private void OnTriggerExit(Collider other)
    {

        if (GameManager.ins.layerData.HumanLayer == (GameManager.ins.layerData.HumanLayer | (1 << other.gameObject.layer)))
        {
            collisionwithhuman = false;


        }
        else if (GameManager.ins.layerData.VehiclesLayer == (GameManager.ins.layerData.VehiclesLayer | (1 << other.gameObject.layer)))
        {
            collisionwithvehicles = false;
        }
        else if (GameManager.ins.layerData.ObstacleLayer == (GameManager.ins.layerData.ObstacleLayer | (1 << other.gameObject.layer)))
        {
            collisionwithobject = false;
        }
        else if (GameManager.ins.layerData.Trafficlight == (GameManager.ins.layerData.Trafficlight | (1 << other.gameObject.layer)))
        {
            collisionwithtrafficlight = false;
        }

    }
}
