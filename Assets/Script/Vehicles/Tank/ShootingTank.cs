using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTank : MonoBehaviour
{
    public Transform barrelOut;
    public float speed;
    public float currentfireRaterocket;
    public float fireRateRocket;


    public void Shooting(bool isshooting)
    {


        if (currentfireRaterocket > 0)
        {
            currentfireRaterocket -= Time.deltaTime;
            return;
        }
        if (currentfireRaterocket <= 0 && isshooting)
        {
            ParticleSystem flash = FxPooling.ins.GetmuzzleFlashPool_2(barrelOut.position);
            currentfireRaterocket = fireRateRocket;
            GameObject rocket = BulletPooling.ins.GetRocketPool(barrelOut.position);
            rocket.transform.rotation = Quaternion.LookRotation(barrelOut.transform.forward);
            rocket.GetComponent<Rigidbody>().velocity = (barrelOut.transform.forward * speed);

        }
    }

}
