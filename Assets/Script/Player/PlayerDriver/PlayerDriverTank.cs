using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDriverTank : MonoBehaviour
{
    public TankControl tankControl;
    public Tank tank;
    private Transform _enterFromPos;
    public void GetInTank(Transform enterFromPos)
    {
        _enterFromPos = enterFromPos;
        tank._driver = transform.parent.gameObject;
        tank._rb.isKinematic = false;
        transform.parent.position = tank._driverSit.position;
        CameraManager.ins.ChangeCam(2, tank._camtarget);
        Player.ins.animator.applyRootMotion = false;
        transform.parent.parent = tank._driverSit;

    }
    private void Update()
    {
        tank.DriverVehicles(tankControl.Vertical, 0, tankControl.Horizontal, tank._maxspeed);
        //tank.MoveVehicle(tankControl.Vertical,tank._maxspeed);
        //tank.HorizontalMove(tankControl.Horizontal);
        //tank.UpdateVehicleSteering();
        tank.shootingTank.Shooting(tankControl.isShooting);
        //tank.MoveTurret();
        //if (tankControl.Horizontal != 0 && tankControl.Vertical == 0)
        //{
        //    tank.MoveVehicle(0.2f, tank._maxspeed);
        //}
    }
    public void GetOutTank()
    {

        transform.parent.position = _enterFromPos.position;
        Player.ins.player.SetActive(true);
        Player.ins.ChangeControl(0);
        Player.ins.characterController.enabled = true;
    }
}
