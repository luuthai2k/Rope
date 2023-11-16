using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using DG.Tweening;

public class PlayerDriverCar : MonoBehaviour
{
    public VehiclesControl vehiclesControl;
    public Car car;
    public bool canDriver;
    [SerializeField]

    public void GetInCar(Transform enterFormPos)
    {
        canDriver = false;
        transform.parent.eulerAngles = car.transform.eulerAngles + new Vector3(0f, 90f, 0f);
        Player.ins.animator.SetTrigger("GetInVehicles");
        Player.ins.animator.SetInteger("VehiclesType", 1);
        Player.ins.animator.SetFloat("CarType", ((int)car.carData.carType));
        car.animOpenDoor.Play("Car_1_OpenLeft");
        CameraManager.ins.ChangeCam(1, car._camtarget);
        if (car._driver != null)
        {
            Player.ins.animator.SetBool("IsEject", true);


            car._driver.GetComponent<NPCDriver>().GetOutVehicle(true);

            //car.npcDrive.animator.SetBool("DeadOutVehicle", true);

            Invoke("EnterCar", 1.5f);

        }
        else
        {
            Player.ins.animator.SetBool("IsEject", false);
            Invoke("EnterCar", 1f);
        }



    }
    public void EnterCar()
    {
        car.animOpenDoor.Play("Car_1_CloseLeft");
        transform.parent.DOMove(car._driverSit.position, 1f)
            .OnComplete(FinishGetInCar);
    }

    public void FinishGetInCar()
    {
        transform.parent.parent = car._driverSit;
        car._rb.isKinematic = false;
        car._driver = transform.parent.gameObject;
        canDriver = true;
        Player.ins.animator.applyRootMotion = false;

    }
    void Update()
    {
        if (!canDriver) return;
        car.DriverVehicles(vehiclesControl.Vertical, 0, vehiclesControl.Horizontal, car._maxspeed);
    }
    public void GetOutCar(Transform enterFormPos)
    {
        
        canDriver = false;
        car.animOpenDoor.Play("Car_1_OpenLeft");
        car._driver = null;
        CameraManager.ins.ChangeCam(0, transform);
        if (car._rb.velocity.magnitude > 5)
        {
            Player.ins.transform.position = enterFormPos.position;
            Player.ins.animator.Play("exit_force3");
            Invoke("CloseDoor", 2f);
        }
        else
        {
            Player.ins.animator.Play("GetOutCar");
            car._rb.isKinematic = true;
            Player.ins.animator.applyRootMotion = true;
            Invoke("CloseDoor", 1.2f);
        }
        Invoke("FinishGetOutCar", 2f);
    }
    public void CloseDoor()
    {
        car.animOpenDoor.Play("Car_1_CloseLeft");
    }
    public void FinishGetOutCar()
    {
        transform.parent.parent = null;
        Player.ins.ChangeControl(0);
        Player.ins.characterController.enabled = true;
        car._rb.isKinematic = false;


    }

}
