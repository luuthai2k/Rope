using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
//using System.Diagnostics;

public class PlayerMove : MonoBehaviour
{
    [Header("Player Movement")]
    public PlayerControl playerControl;
    public float speed;
    public float currenspeed;
    public float foward;
    float curentfoward = 0;
    public Quaternion rot;
    float turnCalmVelocity;
    [SerializeField] float turnCalmTime = 1f;
    public float speedrotatesprint;
    private float angle;
    public float Gravity;
    public float _verticalVelocity;

    [SerializeField]

    public float damping;
    public bool isSwim;
    Vector3 direction;

    //public void OffRagdoll()
    //{

    //    Player.ins.OffRagdoll();


    //}
    public void HandleInput(CharacterControl characterControl)
    {
        direction = new Vector3(characterControl.joystick.Horizontal, 0f, characterControl.joystick.Vertical);
        float targetrotate = Mathf.Atan2(direction.normalized.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        float CosAngle = Vector3.Dot(Quaternion.Euler(0, targetrotate, 0) * Vector3.forward, transform.forward);
        float Angle = Mathf.Acos(CosAngle) * Mathf.Rad2Deg;
        Player.ins.animator.SetBool("IsSwing", false);
        isSwim = Player.ins.animator.GetBool("IsWater");
        if (characterControl.isSprint)
        {
            if (Player.ins.playerHP.stamina > 0)
            {
                curentfoward = 1.5f;
                Debug.Log(UpgradeXpManager.ins.dataGame.maxSprintSpeed);
                Player.ins.characterController.Move((transform.forward * foward * UpgradeXpManager.ins.dataGame.maxSprintSpeed) * Time.deltaTime);
                angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, Camera.main.transform.eulerAngles.y, ref turnCalmVelocity, turnCalmTime);
                if (!Player.ins.playerHP.infiniteStamina)
                {
                    Player.ins.playerHP.LoseStamina(0.25f);
                }
            }
            else
            {
                angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetrotate, ref turnCalmVelocity, turnCalmTime);
                curentfoward = 0;
            }


        }
        else
        {
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetrotate, ref turnCalmVelocity, turnCalmTime);
            curentfoward = direction.magnitude;
        }
        rot = Quaternion.Euler(0, angle, 0);
        if (isSwim)
        {
            foward = Mathf.MoveTowards(foward, curentfoward, damping * Time.deltaTime);
            if (Player.ins.playerHP.stamina > 0)
            {
                if (!Player.ins.playerHP.infiniteStamina)
                {
                    Player.ins.playerHP.LoseStamina(0.25f);
                }
            }
        }
        else
        {

            if (characterControl.isSprint)
            {
                foward = Mathf.MoveTowards(foward, curentfoward, damping * Time.deltaTime);
            }
            else
            {
                float i = (180 - Angle) / 180 > 0.5 ? 1 : 0;
                foward = Mathf.MoveTowards(foward, curentfoward * i, damping * Time.deltaTime);
            }
        }

        //Debug.Log(foward);
    }
    public void Move(CharacterControl characterControl)
    {

        HandleInput(characterControl);
        if (playerControl.onSurface)
        {
            _verticalVelocity = 0;
            Player.ins.animator.SetFloat("Fall", _verticalVelocity);
            if (!Player.ins.animator.applyRootMotion)
            {
                Player.ins.animator.applyRootMotion = true;
            }
            Player.ins.animator.SetFloat("Forward", foward);
            Player.ins.animator.SetFloat("Turn", characterControl.joystick.Horizontal);
            if (characterControl.joystick.Vertical != 0 || characterControl.joystick.Horizontal != 0 || characterControl.isSprint)
            {
                transform.rotation = rot;
                FreeLookCameraControl.ins.TargetHeading(false);
            }
        }
        else
        {
            //Player.ins.animator.applyRootMotion = false;
            _verticalVelocity += Gravity * Time.deltaTime;
            Player.ins.animator.SetFloat("Fall", _verticalVelocity);
            Player.ins.characterController.Move((transform.forward * speed * direction.magnitude + new Vector3(0.0f, _verticalVelocity, 0.0f)) * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 50 * Time.deltaTime);
        }


    }



}






