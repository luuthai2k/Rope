using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NPCDriver : MonoBehaviour
{

    [Header("Control")]
    public NPCControl npcControl;
    public FindThePath findThePath;
    public bool ischangedirection;
    [Header("State")]
    public bool candriver;
    public bool runaway;
    public bool pursue;
    float forward = 1;
    [Header("Vehicle")]
    public GameObject Vehiclel;
    private IDriverVehicles driverVehicles;
    public float currentspeed;
    public float steeringAngle;
    public VehicleSensor sensor;
    private float timedelay;
    public bool isDriver;

    public void ChangeState()
    {

    }
    public void DriverVehicle()
    {
        GetInVehicles();
        Driver();
    }
    public void GetInVehicles()
    {

        if (driverVehicles == null)
        {
            Vehiclel = Instantiate(npcControl.chacractorData.vehiclePrefab, transform.position, Quaternion.LookRotation(npcControl.pointtarget.position));
            driverVehicles = Vehiclel.GetComponent<IDriverVehicles>();
            sensor = driverVehicles._sensor;
            driverVehicles._driver = gameObject;
        }
        else
        {
            driverVehicles._vehicles.SetActive(true);
        }
        transform.parent = driverVehicles._driverSit;
        transform.position = driverVehicles._driverSit.position;
        transform.localRotation = Quaternion.identity;
        driverVehicles._driver = gameObject;
        if (npcControl.chacractorData.vehicle == SelectVehicles.Car)
        {

            npcControl.animator.Play("SitInCar");
            npcControl.animator.SetFloat("CarType", ((int)Vehiclel.GetComponent<Car>().carData.carType));

        }
        else if (npcControl.chacractorData.vehicle == SelectVehicles.Motor)
        {
            npcControl.animator.Play("SitInMotor");
            npcControl.animator.SetFloat("MotorType", ((int)Vehiclel.GetComponent<Motor>().motorData.motorType));

        }
    }
    public void Driver()
    {

        StartCoroutine(DriverCourotine());
    }

    public void EndDriver()
    {
        StopCoroutine(DriverCourotine());
    }
    IEnumerator DriverCourotine()
    {

        while (candriver)
        {
            if (Vehiclel == null)
            {
                yield break;
            }

            DriverManager();
            findThePath.PathProgress(npcControl.pointtarget, Vehiclel.transform);
            driverVehicles.DriverVehicles(forward, 0, steeringAngle * npcControl.chacractorData.speedRotate * forward, currentspeed);
            if (Vector3.Distance(transform.position, npcControl.pointtarget.position) < 5f)
            {
                NextPoint();
            }

            yield return null;


        }

    }
    public void DriverManager()
    {

        if (sensor.collisionwithhuman || sensor.collisionwithvehicles || sensor.collisionwithobject)
        {

            if (runaway)
            {
                currentspeed = npcControl.chacractorData.maxspeed;
                //Vehiclel.GetComponent<Rigidbody>().isKinematic = false;
                timedelay += Time.deltaTime;
                if (timedelay > 2 && !ischangedirection)
                {

                    timedelay = 0;
                    StartCoroutine(ChangeDirection());
                }


            }
            else
            {
                currentspeed = 0;
                //Vehiclel.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        else
        {
            if (runaway)
            {
                currentspeed = npcControl.chacractorData.maxspeed;
            }
            else
            {
                currentspeed = npcControl.chacractorData.speed;
            }
        }

        Vector3 relativeVector = Vehiclel.transform.InverseTransformPoint(findThePath.PostionToFollow);
        float dis = Vector3.Distance(Vehiclel.transform.position, npcControl.pointtarget.position);
        steeringAngle = (relativeVector.x / relativeVector.magnitude);

        currentspeed -= currentspeed * Mathf.Clamp(steeringAngle, 0, 0.5f);
        if (pursue)
        {
            runaway = true;
        }


    }

    //public void DriverCar()
    //{
    //    //car.MoveVehicle(forward, currentspeed);
    //    //car.UpdateVehicleSteering();
    //    //car.VehicleSteering(steeringAngle * npcControl.chacractorData.speedRotate * forward);
    //    driverVehicles.DriverVehicles(forward, 0, steeringAngle * npcControl.chacractorData.speedRotate * forward, currentspeed);
    //}
    //public void DriverMotor()
    //{
    //    //motor.VerticalMove(forward, currentspeed);
    //    //motor.HorizontalMove(steeringAngle * npcControl.chacractorData.speedRotate * forward);
    //    //motor.TiltingToMotorcycle(0, steeringAngle * 0.5f * forward);
    //    driverVehicles.DriverVehicles(forward, 0, steeringAngle * npcControl.chacractorData.speedRotate * forward, currentspeed);
    //}
    public void NextPoint()
    {


        if (npcControl.pointtarget.gameObject.GetComponent<PointAIMove>() != null)
        {
            npcControl.pointtarget = npcControl.pointtarget.gameObject.GetComponent<PointAIMove>().RandomNextPoint();
            return;
        }
        else if (pursue)
        {

            GetOutVehicle(false);
        }
    }
    IEnumerator ChangeDirection()
    {
        ischangedirection = true;
        findThePath.ClearWayPoint();
        forward = -1;
        yield return new WaitForSeconds(2f);
        forward = 1;

        ischangedirection = false;

    }
    public void GetOutVehicle(bool isEject=false,float side=0)
    {
      
        candriver = false;
        transform.parent = null;
        if (npcControl.chacractorData.vehicle == SelectVehicles.Car)
        {

            GetOutCar(isEject,side);

        }
        else if (npcControl.chacractorData.vehicle == SelectVehicles.Motor)
        {
            GetOutMotor(isEject, side);

        }
        npcControl.npcState.ChangeState(SelectState.Attack);
    }
    public void GetOutCar(bool isEject, float side )
    {
        if (isEject)
        {
            transform.position = driverVehicles._enterFormPos[0].position;
            npcControl.animator.Play("exit_force3");
        }
        else
        {
            npcControl.animator.Play("GetOutCar");
            npcControl.animator.applyRootMotion = true;
        }
    }
    public void GetOutMotor(bool isEject, float side)
    {
        if (isEject)
        {
            npcControl.animator.SetTrigger("GetOutVehicles");
        }
        else
        {
            npcControl.animator.SetTrigger("GetOutVehicles");
        }
       
    }
    IEnumerator StopVehicles()
    {
        while (pursue)
        {
            if (npcControl.chacractorData.vehicle == SelectVehicles.Car)
            {
                //car.MoveVehicle(forward, 0);
                driverVehicles.DriverVehicles();
                if (driverVehicles._rb.velocity.magnitude <= 0.1f)
                {
                    pursue = false;
                }
            }
            else if (npcControl.chacractorData.vehicle == SelectVehicles.Motor)
            {
                //motor.VerticalMove(forward, 0);
                driverVehicles.DriverVehicles();
                if (driverVehicles._rb.velocity.magnitude <= 0.1f)
                {
                    pursue = false;
                }
            }
            yield return null;
        }
        transform.parent = null;
        npcControl.animator.SetTrigger("GetOutVehicle");

        Invoke("EndDeadVehicle", 2f);
    }

    public void EndDeadVehicle()
    {
        npcControl.DoFallAction();
    }


}

