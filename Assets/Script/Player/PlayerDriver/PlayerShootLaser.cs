using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootLaser : MonoBehaviour
{
    public Transform muzzleTransform;
    public bool canshoot;
    public float startTime;
    public float currentStartTime;
    public float fireRate;
    private GameObject laser;
    private float speedcam;
    public bool isStart;
    public float speedrotate;


    public void StartShootLaser()
    {
        if (isStart) return;
        isStart = true;
        StartCoroutine(CouroutineCanShoot());
        Player.ins.animator.SetBool("IsLaser", true);
    }

    IEnumerator CouroutineCanShoot()
    {
        yield return new WaitForSeconds(startTime);
        canshoot = true;
    }
    public void ShootLaser()
    {
        Quaternion rot = Quaternion.Euler(new Vector3(0f, Camera.main.transform.eulerAngles.y, 0f));
        Player.ins.transform.rotation = Quaternion.RotateTowards(Player.ins.transform.rotation, rot, speedrotate * Time.deltaTime);
        StartShootLaser();
        if (!canshoot) return;
        if(Player.ins.playerHP.stamina <= 0)
        {
            FinishShootLaser();

        }
        else
        {
            if (!Player.ins.playerHP.infiniteStamina)
                Player.ins.playerHP.LoseStamina(0.05f);

            if (laser == null)
            {
                laser = BulletPooling.ins.GetLaserPool(muzzleTransform.position);
                laser.transform.parent = muzzleTransform;
                laser.transform.localRotation = Quaternion.identity;
            }
            else
            {
                laser.SetActive(true);
            }

        }

    }
    public void FinishShootLaser()
    {

        Player.ins.playerControl.characterControl.isLaser = false;
        if (laser != null)
        {
            laser.SetActive(false);
        }
        canshoot = false;
        isStart = false;
        Player.ins.animator.SetBool("IsLaser", false);
    }



}




