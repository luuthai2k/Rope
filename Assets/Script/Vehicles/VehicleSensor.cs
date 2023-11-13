using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VehicleSensor : MonoBehaviour
{
    public bool collisionwithhuman;
    public bool collisionwithobject;
    public bool collisionwithvehicles;
    public bool cancheckcollision;
    public bool forward;
    public bool back;


    private void OnCollisionEnter(Collision collision)
    {

        if (GameManager.ins.layerData.HumanLayer == (GameManager.ins.layerData.HumanLayer | (1 << collision.gameObject.layer)))
        {
            collisionwithhuman = true;


        }
        else if (GameManager.ins.layerData.VehiclesLayer == (GameManager.ins.layerData.VehiclesLayer | (1 << collision.gameObject.layer)))
        {
            collisionwithvehicles = true;
        }
        else if (GameManager.ins.layerData.ObstacleLayer == (GameManager.ins.layerData.ObstacleLayer | (1 << collision.gameObject.layer)))
        {
            collisionwithobject = true;
            if (collision.gameObject.isStatic) return;
            else
            {
                if (collision.gameObject.GetComponent<NavMeshObstacle>() == null)
                {
                    NavMeshObstacle navMeshObstacle = collision.gameObject.AddComponent<NavMeshObstacle>();
                    navMeshObstacle.carving = true;

                }
                else
                {
                    collision.gameObject.GetComponent<NavMeshObstacle>().enabled = true;
                }
            }




        }
        float CosAngle = Vector3.Dot(collision.transform.position - transform.position, transform.forward);
        if (CosAngle >= 0)
        {
            forward = true;
            back = false;
        }
        else
        {
            forward = false;
            back = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {

        if (GameManager.ins.layerData.HumanLayer == (GameManager.ins.layerData.HumanLayer | (1 << collision.gameObject.layer)))
        {
            collisionwithhuman = false;


        }
        else if (GameManager.ins.layerData.VehiclesLayer == (GameManager.ins.layerData.VehiclesLayer | (1 << collision.gameObject.layer)))
        {
            collisionwithvehicles = false;
        }
        else if (GameManager.ins.layerData.ObstacleLayer == (GameManager.ins.layerData.ObstacleLayer | (1 << collision.gameObject.layer)))
        {
            collisionwithobject = false;
            //if (other.GetComponent<NavMeshObstacle>() != null)
            //{
            //    other.gameObject.GetComponent<NavMeshObstacle>().enabled = false;
            //}


        }

    }
    public void AICollision(bool _collisionwithhuman, bool _collisionwitobject, bool _collisionwithvehicles)
    {
        cancheckcollision = true;
        _collisionwithhuman = collisionwithhuman;
        _collisionwitobject = collisionwithobject;
        _collisionwithvehicles = collisionwithvehicles;
    }
}
