using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour
{
    public ParticleSystem hitEffect;
    public WeaponData WeaponData;
    public Transform muzzle;
    private ParticleSystem flash;
    public float fireRate = 0;
    public float startTime;
    public float offset;
    Ray ray;
    [SerializeField]
    private NPCControl npc;

    private void Start()
    {

        startTime = WeaponData._startTime;
    }
    public void StartShooting(NPCControl _npc)
    {
        npc = _npc;

        if (startTime > 0)
        {
            startTime -= Time.deltaTime;
            return;
        }
        else
        {
            //Invoke("Shooting", 0.1f);
            Shooting();
        }


    }
    public void Shooting()
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
            flash.gameObject.transform.localRotation = Quaternion.identity;
            flash.gameObject.transform.localScale = Vector3.one;
            FxPooling.ins.ReturnPool(flash, 0.5f);

            for (int i = 0; i < 5; i++)
            {
                //var bullet = Instantiate(WeaponData._bullet, pos, Quaternion.identity);
                var bullet = FxShotPooling.ins.GetShotGunPool(pos);
                Vector3 randomOffset = new Vector3(Random.Range(-offset, offset), Random.Range(-offset, offset), 0f);
                Vector3 direction = (PointCenterSceenToWorld.ins.targetTransform.position - pos).normalized + randomOffset;
                HitCheck(direction);
               
                bullet.GetComponent<Rigidbody>().AddForce(direction * WeaponData._power);
            }
            Player.ins.animator.SetBool("RangedWeaponShoot", true);

        }
    }
    public void HitCheck(Vector3 direction)
    {
        ray.origin = muzzle.transform.position;
        ray.direction = direction;

        LayerData layerData = GameManager.ins.layerData;

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 200f))
        {
            if (layerData.HumanLayer == (layerData.HumanLayer | (1 << raycastHit.collider.gameObject.layer)))
            {
                hitEffect = FxPooling.ins.GetbloodEffectPool(raycastHit.point);
                hitEffect.gameObject.transform.LookAt(muzzle);
                Player.ins.playerHP.OnHit(HitDameState.Weapon, false, npc.chacractorData.rangeDame / 5, Vector3.zero);
                return;
            }

            else if (layerData.MetalLayer == (layerData.MetalLayer | (1 << raycastHit.collider.gameObject.layer)))
            {
                hitEffect = FxPooling.ins.GetmetallHitEffectPool(raycastHit.point);
                hitEffect.gameObject.transform.LookAt(muzzle);
                return;
            }

            else if (layerData.StoneLayer == (layerData.StoneLayer | (1 << raycastHit.collider.gameObject.layer)))
            {
                hitEffect = FxPooling.ins.GetstoneHitEffectPool(raycastHit.point);
                hitEffect.gameObject.transform.LookAt(muzzle);
                return;
            }

            else if (layerData.WoodLayer == (layerData.WoodLayer | (1 << raycastHit.collider.gameObject.layer)))
            {
                hitEffect = FxPooling.ins.GetwoodHitEffectPool(raycastHit.point);
                hitEffect.gameObject.transform.LookAt(muzzle);
                return;
            }
        }
    }
    public void FinishShooting()
    {
        startTime = WeaponData._startTime;
        fireRate -= Time.deltaTime;
    }
}
