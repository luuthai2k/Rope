using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using DG.Tweening;
using Cinemachine;

public class PlayerSwing : MonoBehaviour
{
    private Tween myTween;
    float turnCalmVelocity;
    [SerializeField] float turnCalmTime = 1f;
    [SerializeField] private PlayerControl playerControl;
    [SerializeField] private float heightMax;
    [SerializeField] private LineRenderer lineRenderer;
    SpringJoint joint;
    [Header("Player Jump")]
    public float speed;
    public float Gravity;
    public float jumpHeight;
    private float _verticalVelocity;
    [Header("Player Swing")]
    [SerializeField] Transform swingStartPoint;
    [SerializeField] Vector3 swingPoint;
    [SerializeField] Transform swingPivot;
    public bool startSwing;
    public float force;
    public float foward;
    float currenforce;
    float currenfoward;
    public float shootsilk;
    public float damping;
    public float maxAngle;
    private bool isSwing;
    private bool isfall;
    private bool canswing = true;
    public float swing;
    private float currentdamping;
    public float fall;
    public void Jump()
    {
        _verticalVelocity = Mathf.Sqrt(Mathf.Abs(jumpHeight * Gravity));
    }
    public void StartSwing(CharacterControl characterControl)
    {

        if (Input.GetKey(KeyCode.LeftControl) && canswing || characterControl.isSwing && canswing)
        {

            Player.ins.animator.SetBool("IsSwing", true);
            if (startSwing) return;
            if (playerControl.onSurface)
            {
                isSwing = false;
                if (Player.ins.animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
                {
                    fall = 1;
                    Jump();
                }
                else
                {
                    return;

                }
            }
            else
            {
                if (transform.position.y > heightMax) return;
            }
            swing = 0;
            isfall = false;
            if (myTween != null)
            {
                myTween.Kill();

            }
            swingPoint = swingPivot.position;
            currentdamping = 0f;
            startSwing = true;
        }
        else
        {
            if (startSwing)
            {
                startSwing = false;
                canswing = false;
                FinishSwing(1.5f);
            }
            lineRenderer.enabled = false;
            Player.ins.animator.SetBool("IsSwing", false);
        }
    }
    public void CanSwing()
    {
        isSwing = true;
        if (GetComponent<SpringJoint>() == null)
        {
            joint = gameObject.AddComponent<SpringJoint>();
        }
        else
        {
            joint.enableCollision = true;
        }
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = swingPoint;
        float dis = Vector3.Distance(swingPoint, swingStartPoint.position);
        joint.maxDistance = dis * 0.8f;
        joint.minDistance = dis * 0.5f; ;
        joint.damper = 5;
        joint.massScale = 10;
        joint.spring = 7.5f;
        myTween = DOTween.To(() => currentdamping, x => currentdamping = x, 2, damping)
        .SetEase(Ease.InQuad);
        lineRenderer.enabled = true;

    }
    public void Swing(CharacterControl characterControl)
    {
        StartSwing(characterControl);
        float currenspeed = 0;
        if (isSwing)
        {
            //FreeLookCameraControl.ins.TargetHeading(true, 2, 2f);
            Player.ins.animator.applyRootMotion = false;
            IsSwing();
            Vector3 direction = new Vector3(characterControl.joystick.Horizontal, 0f, characterControl.joystick.Vertical);
            Vector3 directionMove = Vector3.zero;
            Player.ins.animator.SetFloat("Forward", Mathf.Clamp01(direction.magnitude));
            Player.ins.animator.SetFloat("Turn", characterControl.joystick.Horizontal);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = 0;

            if (isfall)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
                fall += Time.deltaTime * 0.1f;
                directionMove = transform.up * currenforce + transform.forward * currenfoward + new Vector3(0.0f, _verticalVelocity, 0.0f);
                angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, 2f);

            }
            else
            {

                swing += Time.deltaTime;
                currenforce = force * Mathf.Clamp(swing, 0.5f, 1f);
                currenfoward = foward * Mathf.Clamp(direction.magnitude * swing, Mathf.Clamp(swing, 0.5f, 1f), 1.25f);
                if (CheckForAngle(swingStartPoint.position, swingPoint))
                {
                    startSwing = false;
                    canswing = false;
                    FinishSwing(1.5f);

                }

                _verticalVelocity = 0;
                directionMove = transform.forward * foward * Mathf.Clamp(fall, 1f, 1.5f) + transform.up * force * currentdamping;
                angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);

            }
            Player.ins.animator.SetFloat("Fall", _verticalVelocity);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Player.ins.characterController.Move(directionMove * Time.deltaTime);
            return;
        }
        else
        {

            Vector3 direction = new Vector3(characterControl.joystick.Horizontal, 0f, characterControl.joystick.Vertical);
            currenspeed = direction.magnitude * speed;
            _verticalVelocity += Gravity * Time.deltaTime;
            if (characterControl.isSprint)
            {
                currenspeed = speed * 1.5f;
            }
            Player.ins.characterController.Move((transform.forward * currenspeed + new Vector3(0.0f, _verticalVelocity, 0.0f)) * Time.deltaTime);
        }

    }


    public void IsSwing()
    {
        if (!isSwing) return;
        StartCoroutine(UpdateLineRender());
        Player.ins.animator.SetFloat("Swing", swing);


    }

    IEnumerator UpdateLineRender()
    {

        while (isSwing)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(1, swingStartPoint.position);
            lineRenderer.SetPosition(0, swingPoint);
            yield return null;
        }

    }
    public void FinishSwing(float time)
    {

        lineRenderer.enabled = false;
        isfall = true;
        fall = 1.5f;
        StartCoroutine(CouroutineDelaySwing(time));
    }
    IEnumerator CouroutineDelaySwing(float time)
    {
        yield return new WaitForSeconds(time);
        canswing = true;
        startSwing = false;
    }

    private bool CheckForAngle(Vector3 pos, Vector3 source)
    {
        Vector3 direction = pos - source;
        Vector3 planeNormal = Vector3.up;
        float Angle = Vector3.SignedAngle(direction, planeNormal, Vector3.up);
        if (Angle < maxAngle)
            return true;
        else
            return false;
    }
}