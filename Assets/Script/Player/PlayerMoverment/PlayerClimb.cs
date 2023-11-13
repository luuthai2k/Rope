using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    public float raydistance = 0.5f;
    public float dis1 = 0.5f;
    public float dis2 = 1f;
    [SerializeField] Transform wallCheck;
    public float speed;
    public float speedrotate;
    public bool canclimb;
    public bool isPlane;
    public bool isCorner;
    public bool isGround;
    public Vector3 target;
    public bool cancheck = true;
    public Vector3 rot;
    public Vector3 climbDirection;
    public float rad;
    public float Gravity;
    public float jumpHeight;
    private float _verticalVelocity;
    private bool isStartClimb;
    public bool isStart;
    Vector3 directionJump;

    public bool CheckNearWall()
    {

        RaycastHit hit;
        if (Physics.Raycast(wallCheck.position, transform.forward + Vector3.left, out hit, raydistance, GameManager.ins.layerData.WallMask) || Physics.Raycast(wallCheck.position, transform.forward - Vector3.left, out hit, raydistance, GameManager.ins.layerData.WallMask))
        {
            rot = -hit.normal;
            target = hit.point + hit.normal * rad + Vector3.down * Vector3.Distance(transform.position, wallCheck.position);
            transform.position = target;
            Quaternion newRotation = Quaternion.Euler(new Vector3(0, Quaternion.LookRotation(rot).eulerAngles.y, 0));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, 300 * Time.deltaTime);
            Player.ins.animator.SetBool("NearWall", true);
            return true;
        }
        Player.ins.animator.SetBool("NearWall", false);
        return false;

    }
    public void StartClimb()
    {
        if (isStartClimb) return;
        isStartClimb = true;
        Player.ins.animator.applyRootMotion = false;

    }
    public void Climb(CharacterControl characterControl)
    {
        StartClimb();
        float h = characterControl.joystick.Horizontal;
        float v = characterControl.joystick.Vertical;
        canclimb = CanClimb(h, v);
        Player.ins.animator.SetBool("NearWall", canclimb);

        if (canclimb)
        {

            isStart = false;
            Player.ins.animator.SetFloat("Vertical", v);
            Player.ins.animator.SetFloat("Horizontal", h);
            if ((characterControl.isSwing && !Player.ins.playerControl.isSwing) || characterControl.isRope || (Input.GetKey(KeyCode.LeftControl) && !Player.ins.playerControl.isSwing))
            {

                transform.rotation = Quaternion.Euler(new Vector3(0, Quaternion.LookRotation(-rot).eulerAngles.y, 0));
                Player.ins.animator.SetBool("NearWall", false);
                return;
            }
            if (h + v == 0)
            {
                FreeLookCameraControl.ins.TargetHeading(false);
                return;
            }
            else
            {
                FreeLookCameraControl.ins.TargetHeading(true);
                if (isPlane)
                {
                    Player.ins.characterController.Move(climbDirection * speed * Time.deltaTime);

                }
                if (isCorner)
                {
                    MoveToTarget();
                }
                else
                {
                    cancheck = true;
                }
            }

            Quaternion newRotation = Quaternion.Euler(new Vector3(0, Quaternion.LookRotation(rot).eulerAngles.y, 0));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, speedrotate * speed * Time.deltaTime);

        }
        else
        {
            if (isGround && h + v != 0)
            {
                JumpUptoGround(directionJump, 5);
                float targetAngle = Mathf.Atan2(h, v) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, targetAngle, 0), speed * Time.deltaTime);
            }
            else
            {
                //transform.rotation = Quaternion.LookRotation(-rot);
                JumpUptoGround(transform.forward, 5);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, transform.rotation.y, 0), speed * Time.deltaTime);
            }

        }
    }

    public bool CanClimb(float h, float v)
    {
        if (isCorner) return true;
        Vector3 _moveDirection = transform.right * h + transform.up * v;
        Vector3 origin = wallCheck.position;
        float _distance = raydistance;
        Vector3 direction = _moveDirection;
        RaycastHit hit;
        Debug.DrawRay(origin, direction, Color.yellow);
        if (Physics.Raycast(origin, direction, out hit, raydistance, GameManager.ins.layerData.WallMask))
        {
            rot = -hit.normal;
            isCorner = true;
            isPlane = false;
            isGround = false;
            cancheck = false;
            target = hit.point + hit.normal * rad + Vector3.down * Vector3.Distance(transform.position, wallCheck.position);
            return true;
        }

        origin += _moveDirection * Mathf.Sqrt(Mathf.Pow(dis1 * h, 2) + Mathf.Pow(dis2 * v, 2));
        direction = transform.forward;

        Debug.DrawRay(origin - transform.forward * rad, direction.normalized * (raydistance + rad), Color.yellow);
        if (Physics.Raycast(origin - transform.forward * rad, direction, out hit, raydistance + rad, GameManager.ins.layerData.WallMask))
        {
            climbDirection = _moveDirection;
            isPlane = true;
            isCorner = false;
            isGround = false;
            return true;
        }

        origin += direction * dis2;
        direction = Vector3.down;
        Debug.DrawRay(origin, direction, Color.yellow);
        if (Physics.Raycast(origin, direction, out hit, raydistance, GameManager.ins.layerData.WallMask))
        {
            directionJump = new Vector3(h, 0f, v);
            float targetAngle = Mathf.Atan2(directionJump.normalized.x, directionJump.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            directionJump = new Vector3(0, targetAngle, 0);
            transform.rotation = Quaternion.Euler(directionJump);
            isGround = true;
            isPlane = false;
            isCorner = false;
            return false;

        }
        direction = -_moveDirection;
        Debug.DrawRay(origin, direction, Color.yellow);
        if (Physics.Raycast(origin, direction, out hit, raydistance, GameManager.ins.layerData.WallMask))
        {
            rot = -hit.normal;
            Debug.LogError("Down");
            isCorner = true;
            isPlane = false;
            isGround = false;
            target = hit.point + hit.normal * rad + Vector3.down * Vector3.Distance(transform.position, wallCheck.position);
            cancheck = false;
            return true;
        }

        return false;

    }
    public void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 0.001f)
        {

            isCorner = false;

        }
    }

    public void JumpUptoGround(Vector3 direction, float _speed)
    {
        StartJump();
        _verticalVelocity += Gravity * Time.deltaTime;
        Player.ins.animator.SetFloat("Fall", _verticalVelocity);
        Player.ins.characterController.Move((transform.forward * _speed + new Vector3(0, _verticalVelocity, 0)) * Time.deltaTime);


    }
    public void StartJump()
    {

        if (isStart) return;
        isStart = true;
        FreeLookCameraControl.ins.TargetHeading(true);
        _verticalVelocity = Mathf.Sqrt(Mathf.Abs(jumpHeight * Gravity));



    }
}
