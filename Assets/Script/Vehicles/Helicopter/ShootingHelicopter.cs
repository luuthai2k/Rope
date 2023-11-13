using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingHelicopter : MonoBehaviour
{
    public float power;
    [Header("Shooting Bullet")]
    public GameObject bulleteffect;
    public Transform launcherLeft;
    public Transform launcherRight;
    public ParticleSystem flashL;
    public ParticleSystem flashR;
    public float fireRateBullet;
    private float fireRatebullet = 0;
    [Header("Shooting Rocket")]
    public GameObject rocket;
    public Transform launcherMid;
    public float fireRateRocket;

    private float currentfireRaterocket = 0;
    void Start()
    {

    }

    public void ShootingRocket(bool isshooting)
    {
        if (currentfireRaterocket > 0)
        {
            currentfireRaterocket -= Time.deltaTime;
            return;
        }
        if (currentfireRaterocket <= 0 && isshooting)
        {
            currentfireRaterocket = fireRateRocket;
            Vector3 direction = PointCenterSceenToWorld.ins.targetTransform.position - launcherRight.position;
            GameObject rocket = BulletPooling.ins.GetRocketPool(launcherMid.position);
            rocket.transform.rotation = Quaternion.LookRotation(direction);
            rocket.GetComponent<Rigidbody>().velocity = (direction.normalized * power);

        }
    }
    public void ShootingBullet(bool isshooting)
    {
        if (fireRatebullet > 0)
        {
            fireRatebullet -= Time.deltaTime;
            return;
        }
        if (fireRatebullet <= 0 && isshooting)
        {
            fireRatebullet = fireRateBullet;
            flashL.Emit(1);
            flashR.Emit(1);
            var bulletLeft = Instantiate(bulleteffect, launcherLeft.position, Quaternion.identity);
            var bulletRight = Instantiate(bulleteffect, launcherRight.position, Quaternion.identity);
            Vector3 directionLeft = PointCenterSceenToWorld.ins.targetTransform.position - launcherLeft.position;
            bulletLeft.GetComponent<Rigidbody>().AddForce(directionLeft * power);
            Vector3 directionRight = PointCenterSceenToWorld.ins.targetTransform.position - launcherRight.position;
            bulletRight.GetComponent<Rigidbody>().AddForce(directionRight * power);
            Debug.Log(fireRatebullet);
        }


    }
}
