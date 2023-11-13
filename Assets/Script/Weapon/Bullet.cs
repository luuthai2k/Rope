using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private LayerMask HumanLayer;
    public ParticleSystem _particleSystem;
    private void Start()
    {
        HumanLayer = GameManager.ins.layerData.HumanLayer;
      
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Building")
        {
            //FxShotPooling.ins.ReturnPool(gameObject, 0f);
        }

    }
}
