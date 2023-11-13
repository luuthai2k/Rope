//using Autodesk.Fbx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public SelectWeapon meleeWeapons;
    public LayerMask HumanLayer;
    //private ParticleSystem hitEffect;
    [SerializeField]
    private Collider cod;
    [SerializeField]
    private NPCControl npc;
    [SerializeField]
    private int dame;
    private void Start()
    {
        HumanLayer = GameManager.ins.layerData.HumanLayer;

    }

    public void OnController(NPCControl _npc)
    {
        cod.enabled = true;
        npc = _npc;
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (HumanLayer == (HumanLayer | (1 << other.gameObject.layer)))
        //{
        //    hitEffect = FxPooling.ins.GetbloodEffectPool(other.transform.position);         
        //    hitEffect.gameObject.transform.parent = other.gameObject.transform;
        //}
       

        if (other.gameObject.tag == "Player")
        {
  
            Player.ins.playerHP.OnHit(HitDameState.Weapon, false, npc.chacractorData.dame,Vector3.zero);
  
            cod.enabled = false;
        }
    }
}
