using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public PlayerState playerState;
    public PlayerRigControl playerRigControl;
    public CharacterControl characterControl;
    public PlayerMove playerMove;
    public PlayerClimb playerClimb;
    public PlayerSwing playerSwing;
    public PlayerRope playerRope;
    public PlayerAttack playerAttack;
    public PlayerShootRoket playerShootRoket;
    public PlayerShootLaser playerShootLaser;
    [Header("On Ground ")]
    public bool onSurface;
    public bool top;
    public bool bot;
    [SerializeField] Transform surfaceCheck;
    [SerializeField] float surfaceDistance = 1.2f;
    public bool isSwing;
    private float timedelay;



    void Update()
    {
        if (playerState != PlayerState.Dead)
        {
            HandleInput();
            if (playerState != PlayerState.Swimming)
            {
                CheckOnGround();
            }
        }

        HandleState();
    }
    public bool CheckOnGround()
    {
        RaycastHit hit;
        onSurface = Physics.Raycast(surfaceCheck.position, Vector3.down, out hit, surfaceDistance, GameManager.ins.layerData.Surface);
        Player.ins.animator.SetBool("OnGround", onSurface);
        if (onSurface)
        {
            playerRigControl.HeadLookAt();
            if (hit.collider.gameObject.CompareTag("Water"))
            {
                Player.ins.animator.SetBool("IsWater", true);
            }
            else
            {
                Player.ins.animator.SetBool("IsWater", false);
            }
        }
        else
        {
            playerRigControl.ReturnHeadLookAt();
            if (characterControl.isLaser)
            {
                characterControl.isLaser = false;
            }

        }
        return onSurface;
    }
    private void HandleState()
    {
        switch (playerState)
        {
            case PlayerState.Move:
                playerMove.Move(characterControl);
                break;
            case PlayerState.Climb:
                playerClimb.Climb(characterControl);
                break;
            case PlayerState.Swing:
                playerSwing.Swing(characterControl);
                break;
            case PlayerState.Rope:
                playerRope.Rope(characterControl);
                break;
            case PlayerState.Attack:
                playerAttack.Attack(characterControl.isAttack);
                break;
            case PlayerState.Rocket:
                playerShootRoket.ShootRoket();
                break;
            case PlayerState.Laser:
                playerShootLaser.ShootLaser();
                break;
            case PlayerState.Dead:

                break;
            //case SelectState.Driver:
            //    npcControl.DoDriver();
            //    break;
            default:
                break;
        }
    }
    public void HandleInput()
    {

        if (characterControl.isRope || Input.GetKey(KeyCode.R))
        {
            if (playerState == PlayerState.Climb)
            {
                timedelay += Time.deltaTime;
                if (timedelay >= 0.5f)
                {
                    timedelay = 0;
                    ChangeState(PlayerState.Rope);


                }
                return;
            }
            ChangeState(PlayerState.Rope);
            return;
        }
        if (characterControl.isSwing || Input.GetKey(KeyCode.LeftControl))
        {


            if (playerState == PlayerState.Climb && !isSwing)
            {
                timedelay += Time.deltaTime;
                if (timedelay >= 0.5f)
                {
                    timedelay = 0;
                    ChangeState(PlayerState.Swing);
                    isSwing = true;

                }
                return;
            }
            if (playerClimb.CheckNearWall() && !onSurface)
            {
                ChangeState(PlayerState.Climb);
                return;
            }
            isSwing = true;
            ChangeState(PlayerState.Swing);
            return;
        }
        else
        {
            isSwing = false;
            //if (playerState == PlayerState.Rope) return;
            if (!onSurface)
            {
                if (playerState == PlayerState.Climb) return;
                if (playerClimb.CheckNearWall())
                {
                    ChangeState(PlayerState.Climb);
                    return;
                }
                if (playerState == PlayerState.Swing) return;
                ChangeState(PlayerState.Move);
            }
            else
            {
                if (characterControl.isAttack || Input.GetKey(KeyCode.A))
                {
                    ChangeState(PlayerState.Attack);
                    return;
                }
                if (characterControl.isRocket || Input.GetKey(KeyCode.N))
                {
                    ChangeState(PlayerState.Rocket);
                    return;
                }
                if (characterControl.isLaser || Input.GetKey(KeyCode.L))
                {
                    ChangeState(PlayerState.Laser);
                    return;
                }
                ChangeState(PlayerState.Move);

            }

        }

    }
    public void ChangeState(PlayerState _playerState)
    {
        if (playerState == _playerState || !FinishState()) return;
        if (_playerState == PlayerState.Laser)
        {
            //FreeLookCameraControl.ins.TargetHeading(true);
        }
        else if (_playerState == PlayerState.Swing)
        {
            FreeLookCameraControl.ins.TargetHeading(true, 1);
        }
        else if (_playerState == PlayerState.Climb)
        {
            Player.ins.animator.SetBool("NearWall", true);
        }
        else if (_playerState == PlayerState.Rocket)
        {
            characterControl.WaitShootRocket();
        }
        else if (_playerState == PlayerState.Move)
        {
            FreeLookCameraControl.ins.TargetHeading(false);
        }
        else if (_playerState == PlayerState.Rope)
        {
            playerRope.PrepareShoot();
        }



        playerState = _playerState;
    }
    public bool FinishState()
    {
        if (playerState == PlayerState.Attack)
        {
            timedelay += Time.deltaTime;
            if (timedelay >= 1.5f)
            {
                timedelay = 0;
                playerAttack.FinishActack(0f);
                return true;
               
            }
            return false;
        }
        else if (playerState == PlayerState.Laser)
        {
            playerShootLaser.FinishShootLaser();

        }
        else if (playerState == PlayerState.Swing)
        {
            playerSwing.FinishSwing(0);
            playerSwing.fall = 1;
            Player.ins.animator.SetBool("IsSwing", false);
        }
        else if (playerState == PlayerState.Climb)
        {
            Player.ins.animator.SetBool("NearWall", false);
            return true;

        }
        else if (playerState == PlayerState.Rocket)
        {
            return !playerShootRoket.isShoot;
        }
        else if (playerState == PlayerState.Rope)
        {
            playerRope.FinishRope();
        }
        return true;
    }
}
public enum PlayerState
{
    Move,
    Climb,
    Swing,
    Rope,
    Fall,
    Attack,
    Rocket,
    Laser,
    Swimming,
    Dead

}
