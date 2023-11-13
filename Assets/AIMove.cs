using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AIMove : MonoBehaviour
{
    public bool onSurface;
    public float speedRotate;
    [SerializeField] Transform surfaceCheck;
    [SerializeField] float surfaceDistance ;
    public float speed;
    public Transform pointtarget;
    public LayerMask surfaceMask; 
    public bool oncollision;
    public bool ismove;
    public AIController aiController;

    public void Move(float value)
    {
        speed = value;
        aiController.animator.SetFloat("Forward", speed);
        StartCoroutine(MoveCourotine());
    }
    public void Idle(float value)
    {

        speed = value;
        aiController.animator.SetFloat("Forward", speed);
        StartCoroutine(IdleCourotine());
    }
    public void CheckOnGround()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);          
    }
    IEnumerator IdleCourotine()
    {
        yield return new WaitForSeconds(5f);
        NextState();
    }
    IEnumerator MoveCourotine()
    {
      
        while (ismove)
        {
           
            Vector3 directionToTarget = pointtarget.position - transform.position;
            directionToTarget.y = 0f; 
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
            aiController.animator.SetFloat("Forward", speed);
            if (oncollision)
            {
               
                oncollision = true;
                Vector3 avoidDirection = FindAvoidDirection(transform.forward);
                
                targetRotation = Quaternion.LookRotation(avoidDirection, Vector3.up);

                Debug.LogError(avoidDirection);
            }
            transform.DORotate(targetRotation.eulerAngles, speedRotate);


            if (Vector3.Distance(transform.position, pointtarget.position) <2f)
            {
                NextState();
            }
            yield return new WaitForSeconds(aiController.timedelay);
        }
                       
    } 
  
    public void NextState()
    {
       
        if (pointtarget.gameObject.GetComponent<PointAIMove>() != null)
        {
            int i = Random.Range(0, 101);
            Debug.Log(i);
            if (i >= 0 && i <= 80)
            {
                pointtarget = pointtarget.gameObject.GetComponent<PointAIMove>().RandomNextPoint();
                Move( 0.5f);
            }
            else
            {

                AIController.ins.aiState.ChangeState(SelectState.Idle);
            }
        }
    }
    
    private Vector3 FindAvoidDirection(Vector3 direction)
    {
        Vector3 avoidDirection = direction+Vector3.right;
        float minAngleDifference = float.MaxValue; // Lưu góc nhỏ nhất giữa hai hướng
        int raysCount = 8;
        float angleStep = 360f / raysCount;

        for (int i = 0; i < raysCount; i++)
        {
            Vector3 rayDirection = Quaternion.Euler(0f, angleStep * i, 0f) * direction;
            Ray ray = new Ray(transform.position + Vector3.up, rayDirection);        
            if (!Physics.Raycast(ray, 10f, aiController.obstacleMask))
            {
                Debug.DrawRay(ray.origin, ray.direction * 1f, Color.red);
                float angleDifference = Vector3.Angle(direction, rayDirection);

                Debug.Log(rayDirection);
                if (angleDifference < minAngleDifference)
                {
                    minAngleDifference = angleDifference;
                    avoidDirection = rayDirection;
                }
            }
        }

        return avoidDirection;
    }
   

}

