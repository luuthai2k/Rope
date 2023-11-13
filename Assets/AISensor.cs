using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISensor : MonoBehaviour
{
 
   
    public AIController aiController;
    private void OnTriggerEnter(Collider other)
    {
        if (aiController.obstacleMask == (aiController.obstacleMask | (1 << other.gameObject.layer)))
        {

            aiController.aiMove.oncollision = true;
            Ray ray = new Ray(transform.position + Vector3.up, transform.forward);
            if (Physics.Raycast(ray, 3f, aiController.obstacleMask) && aiController.aiMove.ismove)
            {

                aiController.animator.SetFloat("Forward", 0);

            }
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (aiController.obstacleMask == (aiController.obstacleMask | (1 << other.gameObject.layer)))
        {
            aiController.aiMove.oncollision = false;
        }
    }

}
