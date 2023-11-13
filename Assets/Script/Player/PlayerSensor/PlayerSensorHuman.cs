using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensorHuman : IHumanSensor
{
    private void OnTriggerEnter(Collider other)
    {
        ObjectCollision = other.gameObject;
        if (other.gameObject.layer == GameManager.ins.layerData.HumanLayer)
        {

            //ObjectCollision = other.gameObject;

        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == (GameManager.ins.layerData.HumanLayer | (1 << other.gameObject.layer)))
        {

            ControlsManager.ins.Control[0].GetComponent<CharacterControl>().getInVehicles.SetActive(false);
        }
        //if (enemylayerMask == (enemylayerMask | (1 << other.gameObject.layer)))
        //{

        //    enemy = null;

        //}
        //if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        //{
        //    characterControl.EndSwimming();
        //    Player.ins.animator.SetInteger("IsSwimming", 0);
        //    playerControl.ChangeState(PlayerState.Move);
        //}
    }
}
