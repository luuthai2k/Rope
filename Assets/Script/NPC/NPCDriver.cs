using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NPCDriver : MonoBehaviour
{

    [Header("Control")]
    public DriverType driverType;
    public NPCControl npcControl;
    public FindThePath findThePath;
    public bool ischangedirection;
    [Header("State")]
    public bool candriver;
    float forward = 1;
    [Header("Vehicle")]
    public GameObject Vehiclel;
    private IDriverVehicles driverVehicles;
    public float currentspeed;
    public float steeringAngle;
    public VehicleSensor sensor;
    private float timedelay;
    public bool isDriver;

    public void DriverVehicle()
    {
        GetInVehicles();
        Driver();
    }
    public void GetInVehicles()
    {

        if (driverVehicles == null)
        {
            Vehiclel = Instantiate(npcControl.chacractorData.vehiclePrefab, transform.position, Quaternion.identity);
            Vehiclel.transform.LookAt(npcControl.pointtarget);
            driverVehicles = Vehiclel.GetComponent<IDriverVehicles>();
            sensor = driverVehicles._sensor;
            driverVehicles._driver = gameObject;
        }
        else
        {
            driverVehicles._vehicles.transform.position = transform.position;
            Vehiclel.transform.LookAt(npcControl.pointtarget);
            driverVehicles._vehicles.SetActive(true);
        }
        transform.parent = driverVehicles._driverSit;
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
            driverVehicles.DriverVehicles(forward, 0, steeringAngle * npcControl.chacractorData.speedRotate * forward, currentspeed);
            if (Vector3.Distance(transform.position, npcControl.pointtarget.position) < 5f)
            {
                NextPoint();
            }

            yield return null;


        }
        yield break;

    }
    public void DriverManager()
    {

        switch (driverType)
        {
            case DriverType.Normal:
                NormalDriver();
                break;
            case DriverType.Runaway:
                RunAwayDriver();
                break;
            case DriverType.Pursue:
                PursueDriver();
                break;
            default:
                break;
        }
    }
    public void NormalDriver()
    {


        if (sensor.collisionwithhuman || sensor.collisionwithtrafficlight)
        {
            driverVehicles._rb.isKinematic = true;
            currentspeed = 0;
            //forward = 0;
        }
        else if ( sensor.collisionwithobject)
        {
            float CosAngle = Vector3.Dot(sensor.objcollisionwithvehicles.transform.position - Vehiclel.transform.position, Vehiclel.transform.forward);
            if (CosAngle < 0)
            {
                driverVehicles._rb.isKinematic = false;
                currentspeed = npcControl.chacractorData.speed;
                return;
            }
            driverVehicles._rb.isKinematic = true;
            currentspeed = 0;
        }


        else
        {
            currentspeed = npcControl.chacractorData.speed;
            driverVehicles._rb.isKinematic = false;
        }

        Vector3 relativeVector = Vehiclel.transform.InverseTransformPoint(npcControl.pointtarget.position);
        steeringAngle = (relativeVector.x / relativeVector.magnitude);
        currentspeed -= currentspeed * Mathf.Clamp(steeringAngle, 0, 0.5f);
        //Vehiclel.transform.rotation = Quaternion.RotateTowards(Vehiclel.transform.rotation, Quaternion.LookRotation(npcControl.pointtarget.position - Vehiclel.transform.position), 50 * Time.deltaTime);

    }
    public void RunAwayDriver()
    {
        findThePath.PathProgress(npcControl.pointtarget, Vehiclel.transform);
        if (sensor.collisionwithhuman || sensor.collisionwithvehicles || sensor.collisionwithobject)
        {

            if (sensor.collisionwithhuman)
            {
                currentspeed = 0;
                driverVehicles._rb.isKinematic = true;
            }
            else
            {
                timedelay += Time.deltaTime;
                if (timedelay > 2 && !ischangedirection)
                {

                    timedelay = 0;
                    StartCoroutine(ChangeDirection());
                }
            }
        }

        else
        {
            currentspeed = npcControl.chacractorData.maxspeed;
            driverVehicles._rb.isKinematic = false;
        }

        Vector3 relativeVector = Vehiclel.transform.InverseTransformPoint(findThePath.PostionToFollow);
        steeringAngle = (relativeVector.x / relativeVector.magnitude);
        currentspeed -= currentspeed * Mathf.Clamp(steeringAngle, 0, 0.5f);
        //Vehiclel.transform.rotation = Quaternion.RotateTowards(Vehiclel.transform.rotation, Quaternion.LookRotation(findThePath.PostionToFollow - Vehiclel.transform.position), 50 * Time.deltaTime);

    }
    public void PursueDriver()
    {
        findThePath.PathProgress(Player.ins.transform, Vehiclel.transform);
        if (sensor.collisionwithhuman || sensor.collisionwithvehicles || sensor.collisionwithobject)
        {

            if (sensor.collisionwithhuman)
            {
                currentspeed = 0;
                driverVehicles._rb.isKinematic = true;
            }
            else
            {
                timedelay += Time.deltaTime;
                if (timedelay > 2 && !ischangedirection)
                {

                    timedelay = 0;
                    StartCoroutine(ChangeDirection());
                }
            }
        }

        else
        {
            currentspeed = npcControl.chacractorData.maxspeed;
            driverVehicles._rb.isKinematic = false;
        }

        Vector3 relativeVector = Vehiclel.transform.InverseTransformPoint(findThePath.PostionToFollow);
        steeringAngle = (relativeVector.x / relativeVector.magnitude);
        currentspeed -= currentspeed * Mathf.Clamp(steeringAngle, 0, 0.5f);
       
    }


    public void NextPoint()
    {
        if (driverType == DriverType.Pursue)
        {
            GetOutVehicle(false);
            return;
        }
        else if (npcControl.pointtarget.gameObject.GetComponent<PointAIMove>() != null)
        {
            npcControl.pointtarget = npcControl.pointtarget.gameObject.GetComponent<PointAIMove>().RandomNextPoint();
            return;
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
        driverVehicles = null;
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
    //IEnumerator StopVehicles()
    //{
    //    //while (pursue)
    //    //{
    //    //    if (npcControl.chacractorData.vehicle == SelectVehicles.Car)
    //    //    {
    //    //        //car.MoveVehicle(forward, 0);
    //    //        driverVehicles.DriverVehicles();
    //    //        if (driverVehicles._rb.velocity.magnitude <= 0.1f)
    //    //        {
    //    //            pursue = false;
    //    //        }
    //    //    }
    //    //    else if (npcControl.chacractorData.vehicle == SelectVehicles.Motor)
    //    //    {
    //    //        //motor.VerticalMove(forward, 0);
    //    //        driverVehicles.DriverVehicles();
    //    //        if (driverVehicles._rb.velocity.magnitude <= 0.1f)
    //    //        {
    //    //            pursue = false;
    //    //        }
    //    //    }
    //    ////    yield return null;
    //    //}
    //    //transform.parent = null;
    //    //npcControl.animator.SetTrigger("GetOutVehicle");

    //    //Invoke("EndDeadVehicle", 2f);
    //}

    public void EndDeadVehicle()
    {
        npcControl.DoFallAction();
    }


}

