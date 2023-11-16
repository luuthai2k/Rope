using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerRope : MonoBehaviour
{
    [SerializeField] private LayerMask pullColliderMask;
    [SerializeField] private LayerMask rushColliderMask;
    [SerializeField] Transform ropeTarget;
    private Vector3 currentRopeTarget;
    private Transform player;
    public Vector3 currentThreadEnd;
    public LineRenderer lineRenderer;
    public Transform startPoint;
    public float shootsilk;
    public GameObject collisionObj;
    private Rigidbody rb;
    public bool isshootsilk;
    public bool ispull;
    public bool canpull;
    public bool canrush;
    public float speed = 10f;
    public float currentspeed;
    public float height;
    public float dis;
    private float distance;
    public float damping;
    public float speedrotate;
    public float timeStartOnGround;
    Quaternion rot;
    public void Start()
    {
        player = Player.ins.transform;
    }
    public void CheckCanRope(float dis, RaycastHit hit)
    {

        if (pullColliderMask == (pullColliderMask | (1 << hit.collider.gameObject.layer)))
        {
            if (dis >= 1) 
            {
                ispull = true;
                currentRopeTarget = hit.point;
                Player.ins.playerControl.characterControl.rope.SetActive(true);
                return;
            }
            Player.ins.playerControl.characterControl.rope.SetActive(false);

        }
        else if (rushColliderMask == (rushColliderMask | (1 << hit.collider.gameObject.layer)))
        {
            if (dis >= 5)
            {
                ispull = false;
                currentRopeTarget = hit.point + hit.normal * 0.3f;
                Player.ins.playerControl.characterControl.rope.SetActive(true);
                return;
            }
            
                Player.ins.playerControl.characterControl.rope.SetActive(false);
            

           

        }


    }
    public void Rope(CharacterControl _characterControl)
    {
        //UpdateLineRender();
        player.rotation = Quaternion.RotateTowards(player.rotation, rot, speedrotate * Time.deltaTime);
        if (!isshootsilk) return;
        if (Vector3.Distance(currentThreadEnd, ropeTarget.position) >= 0.1f) return;
        if (ispull)
        {

            Pull();


        }
        else
        {
            Rush();

        }
    }
    IEnumerator CouroutineUpdateLineRender()
    {
        while (isshootsilk)
        {
            UpdateLineRender();
            yield return null;
        }
        yield break;
    }
    public void SetTargetPoint()
    {
        collisionObj = PointCenterSceenToWorld.ins.CollisionObj;
        ropeTarget.transform.parent = collisionObj.transform;
        Vector3 direction = PointCenterSceenToWorld.ins.targetTransform.position - startPoint.position;
        if (ispull) return;
        Player.ins.animator.SetBool("IsRope", true);

    }
    public void CheckCanRope(RaycastHit hit,float distance)
    {
        if (pullColliderMask == (pullColliderMask | (1 << collisionObj.layer)))
        {
            ispull = true;
            ropeTarget.position = hit.point;

            return;
        }
        else
        {

            Player.ins.animator.SetBool("IsRope", true);
            ispull = false;
            ropeTarget.position = hit.point + hit.normal * 0.3f;
            return;

        }
    }
    public void PrepareShoot()
    {

        Player.ins.animator.SetBool("IsRope", true);
        rot = Quaternion.Euler(new Vector3(0f, Camera.main.transform.eulerAngles.y, 0f));
        if (Player.ins.playerControl.CheckOnGround())
        {
            Invoke("StartShootSilk", timeStartOnGround);
        }
        else
        {
            StartShootSilk();
        }
        currentThreadEnd = startPoint.position;
    }
    public void StartShootSilk()
    {

        if (isshootsilk) return;
        currentThreadEnd = startPoint.position;
        lineRenderer.enabled = true;
        isshootsilk = true;
        StartCoroutine(CouroutineUpdateLineRender());
        ropeTarget.position = currentRopeTarget;
        this.distance = Vector3.Distance(ropeTarget.position, transform.position);
        if (!ispull)
        {
            Player.ins.animator.SetTrigger("IsJump");

        }
    }

    public void UpdateLineRender()
    {
        lineRenderer.positionCount = 2;
        currentThreadEnd = Vector3.MoveTowards(currentThreadEnd, ropeTarget.position, shootsilk * distance * Time.deltaTime);
        lineRenderer.SetPositions(new Vector3[] { startPoint.position, currentThreadEnd });

    }
    public void Pull()
    {
        Vector3 _target = player.position + (collisionObj.transform.position - player.position).normalized * dis;
        Vector3 direction = _target - collisionObj.transform.position;
        float distanceToTarget = direction.magnitude;
        float angle = Vector3.Angle(player.position - ropeTarget.position, collisionObj.transform.forward);
        Debug.Log(angle);
        float initialVelocityY = Mathf.Sqrt(2 * height * Mathf.Abs(Physics.gravity.y));
        float initialVelocityXZ = distanceToTarget / (Mathf.Sqrt(2 * height / Mathf.Abs(Physics.gravity.y)) + Mathf.Sqrt(2 * Mathf.Abs(distanceToTarget - height) / Mathf.Abs(Physics.gravity.y)));
        Vector3 throwVelocity = direction.normalized * initialVelocityXZ;
        throwVelocity.y = initialVelocityY;
        Vector3 randomTorque = new Vector3(angle - 90, 0, angle);
        if (collisionObj.GetComponent<Rigidbody>() != null)
        {
            rb = collisionObj.GetComponent<Rigidbody>();
        }
        else if (collisionObj.transform.parent.gameObject.GetComponent<Rigidbody>() != null)
        {
            rb = collisionObj.transform.parent.gameObject.GetComponent<Rigidbody>();
        }
        else
        {
            Invoke("FinishRope", 0.1f);

        }
        rb.isKinematic = false;
        rb.AddTorque(randomTorque * 10, ForceMode.VelocityChange);
        rb.velocity = throwVelocity;
        Invoke("FinishRope", 0.5f);
    }
    public void Rush()
    {
        Vector3 direction = ropeTarget.position - transform.position;
        CollisionCheck(direction);
        if (Player.ins.playerControl.characterControl.isSwing)
        {
            FinishRope();
        }
        if (direction.magnitude > 0)
        {

            Player.ins.animator.applyRootMotion = false;
            currentspeed = Mathf.MoveTowards(currentspeed, speed, damping * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, ropeTarget.position, currentspeed * Time.deltaTime);
        }
        else
        {
            FinishRope();
        }

    }
    public void FinishRope()
    {

        ispull = false;
        Player.ins.playerControl.characterControl.isRope = false;
        isshootsilk = false;
        currentspeed = 0;
        StartCoroutine(ReturnSilk());
        Player.ins.animator.SetBool("IsJump", false);
        Player.ins.animator.SetBool("IsRope", false);
    }
    IEnumerator ReturnSilk()
    {
        while (Vector3.Distance(startPoint.position, currentThreadEnd) > 0.1f)
        {
            currentThreadEnd = Vector3.MoveTowards(currentThreadEnd, startPoint.position, 5 * distance * Time.deltaTime);
            lineRenderer.SetPositions(new Vector3[] { startPoint.position, currentThreadEnd });
            yield return null;
        }
        lineRenderer.enabled = false;

        yield break;
    }
    public void RandomRopeType()
    {
        int value = Random.Range(1, 4);
        Player.ins.animator.SetInteger("RopeType", value);
    }
    public void CollisionCheck(Vector3 direction)
    {
        RaycastHit hit;
        Debug.DrawRay(startPoint.position, (direction.normalized + Vector3.left) * 5f, Color.green);
        Debug.DrawRay(startPoint.position, (direction.normalized - Vector3.left) * 5f, Color.green);
        float maxDis = Mathf.Clamp(direction.magnitude, 0.5f, 10f);
        if (Physics.Raycast(startPoint.position, direction, out hit, maxDis))
        {
            if (GameManager.ins.layerData.WallMask == (GameManager.ins.layerData.WallMask | (1 << hit.collider.gameObject.layer)))
            {
                Player.ins.animator.SetBool("NearWall", true);
                Quaternion newRotation = Quaternion.Euler(new Vector3(0, Quaternion.LookRotation(-hit.normal).eulerAngles.y, 0));
                rot = Quaternion.RotateTowards(transform.rotation, newRotation, 500 * Time.deltaTime);

            }
            else if (!hit.collider.CompareTag("Player"))
            {
                rot = Quaternion.LookRotation(direction);
                FinishRope();
                Debug.Log("fnish");
            }
            return;
        }
        if (Player.ins.playerControl.playerClimb.CheckNearWall())
        {
            FinishRope();
        }

        else
        {
            rot = Quaternion.LookRotation(direction);
        }





    }
}
