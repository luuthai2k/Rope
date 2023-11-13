using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public LayerMask enemylayerMask;
    public PlayerAttack playerAttack;
    public int dame;
    
    private void OnTriggerEnter(Collider other)
    {
        if (enemylayerMask == (enemylayerMask | (1 << other.gameObject.layer)))
        {
            playerAttack.ishit = true;
            if (other.GetComponent<NPCHP>() != null)
            {
                playerAttack.OffHitBox();

               
                other.GetComponent<NPCHP>().HitDame(dame,Vector3.zero);
                
            }
           
        }
    }
}
