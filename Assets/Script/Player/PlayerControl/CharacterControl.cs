using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterControl : MonoBehaviour
{


    public Joystick joystick;
    public GameObject getInVehicles;
    public GameObject dissable;
    public GameObject rope;
    public bool isGetIn;
    public bool isJump;
    public bool isAttack;
    public bool isSprint;
    public bool isSwing;
    public bool isRope;
    public bool isLaser;
    public bool isRocket;
    public bool isSwimming;
    public void Jump()
    {
        isJump = true;
    }
    public void FinishJump()
    {
        isJump = false;
    }
    public void Sprint()
    {
        isSprint = true;
    }
    public void FinishSprint()
    {
        isSprint = false;
    }
    public void Actack()
    {
        isAttack = true;

    }
    public void FinishAttack()
    {
        isAttack = false;

    }
    public void GetVehicles()
    {
        if (isGetIn) return;
    }
    public void Swing()
    {
        isSwing = true;
    }
    public void FinishSwing()
    {
        isSwing = false;
    }
    public void ShotSilk()
    {
        isRope = true;
        Player.ins.playerControl.playerRope.SetTargetPoint();
    }
    public void FinishShotSilk()
    {
        isRope = false;
    }
    public void Laser()
    {
        if (isLaser)
        {
            isLaser = false;

        }
        else
        {
            isLaser = true;

        }

    }
    public void FinishLaser()
    {
        isLaser = false;
        FreeLookCameraControl.ins.cameraWhenShootLaser.ReturnCamBase();
    }
    public void Rocket()
    {
        if (Player.ins.playerHP.stamina < 15||!Player.ins.playerControl.onSurface) return;
        isRocket = true;
        //dissable.SetActive(true);
    }
    public void WaitShootRocket()
    {
        dissable.SetActive(true);
    }
    public void FinishRocket()
    {
        isRocket = false;
    }

    public void Swimming()
    {
        isSwimming = true;


    }

    public void EndSwimming()
    {
        isSwimming = false;
    }


}
