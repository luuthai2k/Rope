using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public static Player ins;
    public SelectControl Control;
    public Animator animator;
    public CharacterController characterController;
    public Rigidbody rb;
    public GameObject bone;
    public GameObject player;
    public Transform camTarget;
    [Header("PlayerControl")]
    public PlayerControl playerControl;
    public PlayerDriverCar playerDriverCar;
    public PlayerDriverMotor playerDriverMotor;
    public PlayerDriverTank playerDriverTank;
    public PlayerDriverHelicopter playerDriverHelicopter;
    public Joystick joystick;
    public PlayerHP playerHP;
    public PlayerSensor playerSensor;
    public PlayerMove playerMove;

    void Awake()
    {
        ins = this;

    }
    private void Start()
    {
        //bone.SetActive(false);
    }
    public void ChangeControl(int i)
    {
        Control = (SelectControl)i;
        playerControl.enabled = false;
        playerDriverCar.enabled = false;
        playerDriverMotor.enabled = false;
        playerDriverTank.enabled = false;
        playerDriverHelicopter.enabled = false;
        switch (i)
        {
            case 0:
                playerControl.enabled = true;
                break;
            case 1:
                playerDriverCar.enabled = true;
                break;
            case 2:
                playerDriverMotor.enabled = true;
                break;
            case 3:
                playerDriverTank.enabled = true;
                break;
            case 4:
                playerDriverHelicopter.enabled = true;
                break;
            default:
                playerControl.enabled = true;
                break;
        }
        ControlsManager.ins.ChangeControl(i);
    }
    public void OnCollider()
    {
        //animator.enabled = false;
        //rb.isKinematic = false;
        //rb.AddForce(transform.forward, ForceMode.Impulse);
        //bone.SetActive(true);



    }
    public void OffCollider()
    {
        //animator.enabled = true;
        //rb.isKinematic = true;         
        //bone.SetActive(false);

    }

}
public enum SelectControl
{
    Charactor,
    Car,
    Motobike,
    Bicycle,
    Tank,
    Copter,
    Mech,
    Aircraft

}



