using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour, IDriverVehicles
{
    [Header("TankInfo")]
    public CarData carData;
    public PointVehicles pointVehicles;
    public ShootingTank shootingTank;
    public VehicleSensor sensor;
    public GameObject _vehicles { get; set; }
    public VehicleSensor _sensor { get; set; }
    public Rigidbody _rb { get; set; }
    public List<Transform> _enterFormPos { get; set; }
    public GameObject _driver { get; set; }
    public Transform _driverSit { get; set; }
    public Transform _camtarget { get; set; }
    public Transform _exitForce { get; set; }
    public float _maxspeed { get; set; }

    [Header("Tank Chain")]
    public float offset = 0f;
    public float offsetValue = 0f;
    public Renderer trackLeft;
    public Renderer trackRight;
    [Header("Wheels Collider")]
    public List<WheelCollider> LeftWheelCollider;
    public List<WheelCollider> RightWheelCollider;
    [Header("Wheels Transform")]
    public List<Transform> LeftWheelColliderTransform;
    public List<Transform> RightWheelColliderTransform;
    public List<Transform> LeftUselessGrearTransform;
    public List<Transform> RightUselessGrearTransform;
    [Header("Chain Bone")]
    [SerializeField] private Transform[] LeftTrackBones;
    [SerializeField] private Transform[] RightTrackBones;
    [Header("Wheel Engine")]
    private float presentAcceleration = 0f;

    [Header("Vehicle Steering")]
    public float trackThiccness = 0.1f;
    public float speedrotate;
    [Header("Vehicle breaking")]
   
    [Header("TankTurret")]
    public GameObject mainGun;
    public GameObject barrel;
    public int maxGunAngle_elevation = 35;
    public int minGunAngle_depression = 8;
    public Transform target;
    public float speed;
    void Awake()
    {
        _enterFormPos = pointVehicles.enterFormPos;
        _maxspeed = carData.maxspeed;
        _driverSit = pointVehicles.driverSit;
        _exitForce = pointVehicles.exitforce;
        _camtarget = pointVehicles.camtarget;
        _rb = GetComponent<Rigidbody>();

        _sensor = sensor;
        _vehicles = gameObject;
    }
    public void DriverVehicles(float acceleration, float vertical, float horizontal, float maxspeed)
    {
       
        HorizontalMove(horizontal);
        UpdateVehicleSteering();
        //shootingTank.Shooting(tankControl.isShooting);
        MoveTurret();
        if (horizontal != 0 && acceleration == 0)
        {
            MoveVehicle(1, 5);
        }
        else
        {
            MoveVehicle(acceleration, maxspeed);
            //if(acceleration == 0&& _rb.velocity.magnitude <= 1)
            //{
            //    _rb.isKinematic = true;
            //}
            //if(_rb.velocity.magnitude >= 1)
            //{
            //    _rb.isKinematic = true;
            //}

        }
        if (Input.GetKey(KeyCode.A))
        {
            HorizontalMove(-1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            HorizontalMove(1);
        }
    }
    public void MoveVehicle(float Vertical, float speed)
    {
        _rb.centerOfMass = Vector3.zero;
        float forwardSpeed = transform.InverseTransformDirection(_rb.velocity).z;
        ChainSteering(forwardSpeed);
        if (Vertical != 0)
        {
            _rb.isKinematic = false;
            for (int i = 0; i < LeftWheelCollider.Count; i++)
            {
                LeftWheelCollider[i].brakeTorque = 0;
                RightWheelCollider[i].brakeTorque = 0;
            }
            if (Vertical * forwardSpeed < 0 && _rb.velocity.magnitude >= 1)
            {

                for (int i = 0; i < LeftWheelCollider.Count; i++)
                {

                    LeftWheelCollider[i].brakeTorque = carData.breakingForceMax;
                    RightWheelCollider[i].brakeTorque = carData.breakingForceMax;
                }
                presentAcceleration = 0;

            }
            else
            {
                if (_rb.velocity.magnitude <= speed)
                {
                    presentAcceleration = Vertical * carData.accelerationForce;
                }
                else
                {

                    presentAcceleration = 0;
                }

            }


        }
        else
        {
            presentAcceleration = 0;
            for (int i = 0; i < LeftWheelCollider.Count; i++)
            {

                LeftWheelCollider[i].brakeTorque = carData.breakingForceMax;
                RightWheelCollider[i].brakeTorque = carData.breakingForceMax;
            }
            //if(_rb.velocity.magnitude >= 1)
            //{
            //    _rb.isKinematic = true;
            //}
           
        }

        for (int i = 0; i < LeftWheelCollider.Count; i++)
        {
            LeftWheelCollider[i].motorTorque = presentAcceleration;
            RightWheelCollider[i].motorTorque = presentAcceleration;
        }


    }

    public void ChainSteering(float forwardSpeed)
    {
        if (forwardSpeed > 0)
        {
            offset += _rb.velocity.magnitude / offsetValue;
        }
        if (forwardSpeed < 0)
        {
            offset -= _rb.velocity.magnitude / offsetValue;
        }
        trackLeft.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        trackRight.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
    public void HorizontalMove(float Horizontal)
    {
        //if (Horizontal != 0)
        //{
            Quaternion newRotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.eulerAngles.y + Horizontal * carData.wheelsTorque, transform.rotation.z));

            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speedrotate * Time.fixedDeltaTime);
        //}


    }


    public void UpdateVehicleSteering()
    {

        for (int i = 0; i < LeftWheelCollider.Count; i++)
        {
            SteeringWheels(LeftWheelCollider[i], LeftWheelColliderTransform[i], LeftTrackBones[i]);
            SteeringWheels(RightWheelCollider[i], RightWheelColliderTransform[i], RightTrackBones[i]);
            float speed = LeftWheelCollider[i].attachedRigidbody.velocity.magnitude;


        }
        SteeringUselessGrear(LeftWheelCollider[0], LeftUselessGrearTransform[0]);
        SteeringUselessGrear(LeftWheelCollider[5], LeftUselessGrearTransform[1]);
        SteeringUselessGrear(RightWheelCollider[0], RightUselessGrearTransform[0]);
        SteeringUselessGrear(RightWheelCollider[5], RightUselessGrearTransform[1]);


    }
    void SteeringWheels(WheelCollider WC, Transform WT, Transform TrackBones)
    {
        Vector3 pos;
        Quaternion rotation;
        WC.GetWorldPose(out pos, out rotation);
        WT.rotation = rotation;
        WT.position = pos + new Vector3(0, trackThiccness, 0);
        TrackBones.position = WT.position + transform.up * -1f * (WC.radius + 0.1f);
    }
    void SteeringUselessGrear(WheelCollider WC, Transform WT)
    {
        Quaternion rotation;
        WC.GetWorldPose(out _, out rotation);
        WT.rotation = rotation;


    }

    public void MoveTurret()
    {


        mainGun.transform.rotation = Quaternion.RotateTowards(mainGun.transform.rotation, Quaternion.Euler(new Vector3(0f, Camera.main.transform.rotation.eulerAngles.y, 0f)), speed * Time.deltaTime);


        Vector3 directionToTarget = target.position - barrel.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);

        float targetXAngle = targetRotation.eulerAngles.x;

        if (targetXAngle > 180f)
        {
            targetXAngle -= 360f;
        }
        float clampedXAngle = Mathf.Clamp(targetXAngle, maxGunAngle_elevation, minGunAngle_depression);
        barrel.transform.localRotation = Quaternion.Euler(clampedXAngle, 0f, 0f);

    }
}
