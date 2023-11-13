using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    private LayerMask HumanLayer;
    private ParticleSystem hitEffect;
    public WeaponData WeaponData;
    public Transform muzzle;
    public Transform spawnBullet;
    private ParticleSystem flash;
    public float fireRate = 0;
    public float startTime;
    Ray ray;
    [SerializeField]
    private NPCControl npc;

    private void Start()
    {
        HumanLayer = GameManager.ins.layerData.HumanLayer;
        startTime = WeaponData._startTime;
    }

    public void StartShooting(Vector3 direction,NPCControl _npc)
    {
        npc = _npc;



        Shooting(direction);
    }
    public void Shooting(Vector3 direction)
    {

        if (fireRate > 0)
        {
            fireRate -= Time.deltaTime;
            Player.ins.animator.SetBool("RangedWeaponShoot", false);
            return;
        }
        if (fireRate <= 0)
        {
            Vector3 pos = muzzle.position;
            fireRate = 1f / WeaponData._fireRate;
            flash = FxPooling.ins.GetmuzzleFlashPool(muzzle.position);
            flash.gameObject.transform.parent = muzzle;
            flash.gameObject.transform.localScale = Vector3.one;
            flash.gameObject.transform.forward = muzzle.forward;
            FxPooling.ins.ReturnPool(flash, 0.5f);
    
            //var bullet = Instantiate(WeaponData._bullet, pos, Quaternion.identity);
            var bullet = FxShotPooling.ins.GetShotPistolPool(pos);
            
            HitCheck(direction);
            Player.ins.playerHP.OnHit(HitDameState.Weapon, false, 10,Vector3.zero);
   
            bullet.GetComponent<Rigidbody>().AddForce(direction * WeaponData._power);
            Player.ins.animator.SetBool("RangedWeaponShoot", true);

        }
    }
    public void HitCheck(Vector3 direction)
    {
        ray.origin = muzzle.transform.position;
        ray.direction = direction;


        if (Physics.Raycast(ray, out RaycastHit raycastHit, 200f))
        {
            if (HumanLayer == (HumanLayer | (1 << raycastHit.collider.gameObject.layer)))
            {
                hitEffect = FxPooling.ins.GetbloodEffectPool(raycastHit.point);
                hitEffect.gameObject.transform.LookAt(muzzle);
                hitEffect.gameObject.transform.parent = raycastHit.collider.gameObject.transform;

                Player.ins.playerHP.OnHit(HitDameState.Weapon, false, npc.chacractorData.rangeDame, Vector3.zero);
            }


            else
            {
                hitEffect = FxPooling.ins.GetstoneHitEffectPool(raycastHit.point);
                hitEffect.gameObject.transform.LookAt(muzzle);

            }
        }
    }
    public void FinishShooting()
    {
        startTime = WeaponData._startTime;
        fireRate -= Time.deltaTime;
    }
}
