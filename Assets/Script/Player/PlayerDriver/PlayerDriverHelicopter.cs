using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerDriverHelicopter : MonoBehaviour
{

    public HelicopterControl helicopterControl;
    public Helicopter helicopter;
    public bool canDriver;
    public void GetInHelicopter(Transform enterFormPos, int side)
    {
        canDriver = false;
        Player.ins.animator.SetTrigger("GetInVehicles");
        Player.ins.animator.SetInteger("VehiclesType", 2);
        Player.ins.animator.SetFloat("Side", side);
        CameraManager.ins.ChangeCam(3, helicopter._camtarget);
        if (side == 0)
        {
            Player.ins.gameObject.transform.eulerAngles = helicopter.transform.eulerAngles + new Vector3(0f, 90f, 0f);
        }
        if (side == 1)
        {
            Player.ins.gameObject.transform.eulerAngles = helicopter.transform.eulerAngles + new Vector3(0f, -90f, 0f);
        }
        helicopter._driver = Player.ins.player;
        Invoke("EnterHelicopter", 1.5f);
    }

    public void EnterHelicopter()
    {
        helicopter.animOpenDoor.SetTrigger("Open");
        transform.parent.parent = helicopter._driverSit;
        transform.parent.DOLocalRotate(Vector3.zero, 1f);
        transform.parent.DOMove(helicopter._driverSit.position, 1f)
          .OnComplete(FinishGetIn);

    }
    public void FinishGetIn()
    {

        canDriver = true;
        helicopter._rb.isKinematic = false;
        Player.ins.animator.applyRootMotion = false;

    }

    private void Update()
    {
        if (!canDriver) return;
        helicopter.DriverVehicles(helicopterControl.Vertical, helicopterControl.joystick.Vertical, helicopterControl.joystick.Horizontal, helicopter._maxspeed);
        //helicopter.CheckOnGround(helicopterControl.joystick.Vertical, helicopterControl.joystick.Horizontal, helicopterControl.Vertical);
        helicopter.shootingHelicopter.ShootingBullet(helicopterControl.shootingBullet);
        helicopter.shootingHelicopter.ShootingRocket(helicopterControl.shootingRocket);
        //helicopter.HelicopterHover();
        //helicopter.HelicopterMovement();
        //helicopter.HelicopterTilting();
        //Player.ins.animator.SetBool("OnGround", helicopter.onSurface);
    }

    public void GetOutHelicopter()
    {
        helicopter.animOpenDoor.SetTrigger("Open");
        transform.parent.position = helicopter._enterFormPos[0].position;
        if (helicopter.onSurface)
        {
            Player.ins.animator.Play("helic_exit");
        }
        else
        {
            Player.ins.animator.Play("helic_exit_fly");
        }

        CameraManager.ins.ChangeCam(0);
        Invoke("FinishGetOutHelicopter", 2.5f);
    }
    public void FinishGetOutHelicopter()
    {
        helicopter._driver = null;
        Player.ins.ChangeControl(0);
        Player.ins.characterController.enabled = true;
        helicopter.Fall();

    }
}
