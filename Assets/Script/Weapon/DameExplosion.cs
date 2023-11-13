using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DameExplosion : MonoBehaviour
{
    [SerializeField]
    private float dameExplosion;
    [SerializeField]
    private float powerRagDoll;
    private SphereCollider _collider;
    public float range = 6;
    private void OnEnable()
    {
        _collider = gameObject.GetComponent<SphereCollider>();
        _collider.enabled = true;
        _collider.radius = 0;
        DOTween.To(() => _collider.radius, x => _collider.radius = x, range, 1f)
             .OnComplete(() => {
                 _collider.radius = 0;
                 _collider.enabled = false;

             });

    }
    public void OnTriggerEnter(Collider other)
    {
        Vector3 direction = gameObject.transform.position - other.gameObject.transform.position;

        if (other.gameObject.layer == 9)
        {

            //Player.ins.playerHP.OnHit(HitDameState.Weapon, true, dameExplosion,pos,powerRagDoll);

        }
        if (other.gameObject.layer == 8)
        {
            other.gameObject.GetComponent<VehiclesHp>().DameVehicles(dameExplosion);
        }

        if (other.gameObject.layer == 10)
        {
            other.gameObject.GetComponent<NPCHP>().HitDame(dameExplosion, direction, powerRagDoll, true);

        }
    }
}
