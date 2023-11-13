using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerRigControl : MonoBehaviour
{
    public RigBuilder rigBuilder;
    public HeadLookAt headLookAt;
    public MultiAimConstraint multiAimConstraint_Spine;
   

    public void HeadLookAt()
    {
        if (!rigBuilder.layers[0].active)
        {
            rigBuilder.layers[0].active = true;
        }
        if (Time.timeScale != 0)
        {
            headLookAt.HeadLookAtToCenter();
        }
        
    }
    public void ReturnHeadLookAt()
    {
        if (!rigBuilder.layers[0].active) return;

        rigBuilder.layers[0].active = false;

    }
    //public void ShootLaser()
    //{
    //    multiAimConstraint_Spine.weight = 1;
    //}
    //public void ReturnShootLaser()
    //{
    //    multiAimConstraint_Spine.weight = 0;
    //}
}
