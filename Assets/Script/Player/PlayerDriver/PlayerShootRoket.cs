using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootRoket : MonoBehaviour
{

    public Transform muzzleTransform;
    public float fireRate;
    public float startTime;
    public float speed;
    public bool canshoot;
    public bool isShoot;
    public float speedrotate = 100;
    void Start()
    {

        canshoot = true;
    }





    IEnumerator CouroutineRotate()
    {
        Player.ins.animator.SetTrigger("IsShootRocket");
        StartCoroutine(StartShootCouroutine());
        while (Player.ins.transform.rotation != Quaternion.Euler(new Vector3(0f, Camera.main.transform.eulerAngles.y, 0f)))
        {
            Player.ins.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0f, Camera.main.transform.eulerAngles.y, 0f)), speedrotate * Time.deltaTime);
            yield return null;
        }
        yield break;
    }
    public void ShootRoket()
    {

        if (canshoot)
        {

            if (!Player.ins.playerHP.infiniteStamina)
            {
                Player.ins.playerHP.LoseStamina(15f);
            }

            StartCoroutine(CouroutineRotate());
            canshoot = false;
            isShoot = true;
            //characterControl.isRocket = false;
        }
    }
    IEnumerator StartShootCouroutine()
    {
        yield return new WaitForSeconds(startTime);
        Shoot();
        StartCoroutine(DelayShoot());

    }
    public void Shoot()
    {

        Vector3 pos = muzzleTransform.position;
        Vector3 direction = PointCenterSceenToWorld.ins.targetTransform.position - pos;
        ParticleSystem flash = FxPooling.ins.GetmuzzleFlashPool(muzzleTransform.position);
        flash.gameObject.transform.parent = muzzleTransform;
        flash.gameObject.transform.localScale = Vector3.zero;
        flash.transform.rotation = Quaternion.identity;
        GameObject rocket = BulletPooling.ins.GetRocketPool(muzzleTransform.position);
        rocket.transform.rotation = Quaternion.LookRotation(direction);
        rocket.GetComponent<Rigidbody>().velocity = (direction.normalized * speed);
        StartCoroutine(FinishShootRocket());
    }
    IEnumerator DelayShoot()
    {
        yield return new WaitForSeconds(fireRate);
        canshoot = true;
    }
    IEnumerator FinishShootRocket()
    {
        yield return new WaitForSeconds(0.5f);
        isShoot = false;
    }

}
