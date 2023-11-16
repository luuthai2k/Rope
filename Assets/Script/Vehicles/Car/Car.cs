using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class Car : MonoBehaviour, IDriverVehicles
{
    [Header("CarInfo")]
    public PointVehicles pointVehicles;
    public CarData carData;
    public Animator animOpenDoor;
    public VehiclesHp vehiclesHp;
    public GameObject _vehicles { get; set; }
    public Rigidbody _rb { get; set; }
    public VehicleSensor sensor;
    public GameObject car;
    public VehicleSensor _sensor { get; set; }
    public List<Transform> _enterFormPos { get; set; }
    public GameObject _driver { get; set; }
    public Transform _driverSit { get; set; }
    public Transform _camtarget { get; set; }
    public Transform _exitForce { get; set; }
    public float _maxspeed { get; set; }
    [Header("Wheels Collider")]
    public WheelCollider frontRightWheelCollider;
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider backRightWheelCollider;
    public WheelCollider backLeftWheelCollider;

    [Header("Wheels Transform")]
    public Transform frontRightWheelTransform;
    public Transform frontLeftWheelTransform;
    public Transform backRightWheelTransform;
    public Transform backLeftWheelTransform;

    private float presentAcceleration = 0f;

    private float presentTurnAngle = 0f;

    public NPCControl npcDrive;
    public Transform transformNPC;
    void Awake()
    {
        _enterFormPos = pointVehicles.enterFormPos;
        _driverSit = pointVehicles.driverSit;
        _exitForce = pointVehicles.exitforce;
        _camtarget = pointVehicles.camtarget;
        _rb = GetComponent<Rigidbody>();
        _maxspeed = carData.maxspeed;
        _sensor = sensor;
        _vehicles = gameObject;


    }
    public void DriverVehicles(float acceleration, float vertical, float horizontal, float maxspeed)
    {
        MoveVehicle(acceleration, maxspeed);
        VehicleSteering(horizontal);
        UpdateVehicleSteering();
        _driver.transform.position = _driverSit.position;
        _driver.transform.localRotation = Quaternion.identity;
        if (Input.GetKey(KeyCode.A))
        {
            VehicleSteering(-1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            VehicleSteering(1);
        }
    }

    public void MoveVehicle(float Vertical, float speed)
    {
        _rb.centerOfMass = Vector3.zero;
        float forwardSpeed = transform.InverseTransformDirection(_rb.velocity).z;
        if (Vertical != 0)
        {
            ApplyBreaks(0);
            if (Vertical * forwardSpeed < 0 && _rb.velocity.magnitude >= 1)
            {

                ApplyBreaks(carData.breakingForceMax);
                presentAcceleration = 0;

            }
            else
            {
                ApplyBreaks(0);
                if (_rb.velocity.magnitude <= speed)
                {
                    presentAcceleration = Vertical * carData.accelerationForce;
                }
                else
                {
                    //ApplyBreaks(carData.breakingForceMax);
                    presentAcceleration = 0;
                }

            }

        }
        else
        {
            ApplyBreaks(carData.breakingForce);
            presentAcceleration = 0;
        }
        backRightWheelCollider.motorTorque = presentAcceleration*Mathf.Clamp01(speed);
        backLeftWheelCollider.motorTorque = presentAcceleration * Mathf.Clamp01(speed);
      

    }
    public void UpdateVehicleSteering()
    {
        SteeringWheels(frontRightWheelCollider, frontRightWheelTransform);
        SteeringWheels(frontLeftWheelCollider, frontLeftWheelTransform);
        SteeringWheels(backRightWheelCollider, backRightWheelTransform);
        SteeringWheels(backLeftWheelCollider, backLeftWheelTransform);

    }
    public void VehicleSteering(float Horizontal)
    {

        presentTurnAngle = Horizontal * carData.wheelsTorque;
        frontRightWheelCollider.steerAngle = presentTurnAngle;
        frontLeftWheelCollider.steerAngle = presentTurnAngle;
       


    }

    void SteeringWheels(WheelCollider WC, Transform WT)
    {
        Vector3 pos;
        Quaternion rotation;
        WC.GetWorldPose(out pos, out rotation);
        WT.rotation = rotation;
        WT.position = pos;
    }

    public void ApplyBreaks(float breakingForce)
    {

        frontRightWheelCollider.brakeTorque = breakingForce;
        frontLeftWheelCollider.brakeTorque = breakingForce;
        backRightWheelCollider.brakeTorque = breakingForce;
        backLeftWheelCollider.brakeTorque = breakingForce;
    }




}
