using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlayerDriverMotor : MonoBehaviour
{
    public VehiclesControl vehiclesControl;
    public Motor motor;
    public bool canDriver;
    private Transform enterFromPos;
    public void GetInMotor(Transform _enterFromPos, int side)
    {
        this.enterFromPos = _enterFromPos;
        Animator _animator = Player.ins.animator;
        Player.ins.gameObject.transform.eulerAngles = motor.transform.eulerAngles;
        CameraManager.ins.ChangeCam(1, Player.ins.camTarget);
        _animator.SetTrigger("GetInVehicles");
        _animator.SetInteger("VehiclesType", 0);
        _animator.SetFloat("Side", side);
        _animator.SetFloat("MotorType", ((int)motor.motorData.motorType));
        if (motor._driver != null)
        {
            Player.ins.animator.SetBool("IsEject", true);
            Invoke("EnterMotor", 1f);
            motor._driver.GetComponent<NPCDriver>().GetOutVehicle(true);

        }
        else
        {
            Player.ins.animator.SetBool("IsEject", false);
            EnterMotor();

        }
        motor._driver = transform.parent.gameObject;
        motor.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
    public void EnterMotor()
    {
        transform.parent.DOMove(motor._driverSit.position, 1f)
            .OnComplete(FinishGetIn);

    }
    public void FinishGetIn()
    {
        canDriver = true;
        motor._rb.isKinematic = false;
        Player.ins.animator.applyRootMotion = false;
        transform.parent.parent = motor._driverSit;

    }
    void Update()
    {
        if (!canDriver) return;
        motor.DriverVehicles(vehiclesControl.Vertical, 0, vehiclesControl.Horizontal, motor._maxspeed);

        //motor.VerticalMove(vehiclesControl.Vertical,motor._maxspeed);
        //    Debug.Log(vehiclesControl.Vertical);
        //    motor.HorizontalMove(vehiclesControl.Horizontal);
        //    motor.TiltingToMotorcycle(0, vehiclesControl.Horizontal);
        //    if (Input.GetKey(KeyCode.A))
        //    {
        //        motor.HorizontalMove(-1);
        //        motor.TiltingToMotorcycle(0, -1);
        //    }
        //    else if (Input.GetKey(KeyCode.D))
        //    {
        //        motor.HorizontalMove(1);
        //        motor.TiltingToMotorcycle(0, 1);
        //    }
    }
    public void GetOutMotor()
    {
        canDriver = false;
        motor._driver = null;
        Player.ins.animator.applyRootMotion = true;
        CameraManager.ins.ChangeCam(0);
        if (motor._rb.velocity.magnitude > 5)
        {
            Player.ins.animator.Play("ex_motosikle_V_3");

        }
        else
        {

            Player.ins.animator.SetTrigger("GetOutVehicles");

        }
        Invoke("FinishGetOutMotor", 1.5f);
    }
    public void FinishGetOutMotor()
    {
        transform.parent.parent = null;
        Player.ins.ChangeControl(0);
        Player.ins.characterController.enabled = true;


    }
}
