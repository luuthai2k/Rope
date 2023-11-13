using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCSensor : MonoBehaviour
{
    public NPCControl npcControl;
    private NavMeshObstacle obstacle;
    public GameObject dsdd;
    public GameObject NPC;
    private void OnTriggerEnter(Collider other)
    {
        if (npcControl.obstacleMask == (npcControl.obstacleMask | (1 << other.gameObject.layer))&&!npcControl.npcAttack.isAttack)
        {
            dsdd = other.gameObject;
            npcControl.npcMovement.oncollision = true;
            Ray ray = new Ray(transform.position + Vector3.up, transform.forward);
            if (Physics.Raycast(ray, 3f, npcControl.obstacleMask) && npcControl.npcMovement.ismove)
            {

                npcControl.navMeshAgent.speed=0;
                npcControl.animator.SetFloat("Forward", 0);
                if (other.gameObject.GetComponent<NavMeshObstacle>() != null)
                {
                     obstacle = other.gameObject.GetComponent<NavMeshObstacle>();
                    obstacle.enabled = true;
                }
                else
                {
                    obstacle = other.gameObject.AddComponent<NavMeshObstacle>();
                    obstacle.carving = true;
                    obstacle.shape = NavMeshObstacleShape.Capsule;
                }

                StartCoroutine(CouroutineTurnOffObstacle());

            }

        }
    }
    IEnumerator CouroutineTurnOffObstacle()
    {
        yield return new WaitForSeconds(3f);
        obstacle.enabled = false;

    }

  
}
