using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterControl : MonoBehaviour
{
    public Joystick joystick;
    public float Vertical;
    public bool isVerticalChange;
    public float speed;
    public bool shootingBullet;
    public bool shootingRocket;
    public bool getOut;
    public void VerticalUp()
    {
        isVerticalChange = true;
        StartCoroutine(VerticalCouroutine(1));
    }
    public void VerticalDown()
    {
        isVerticalChange = true;
        StartCoroutine(VerticalCouroutine(-1));
    }

    IEnumerator VerticalCouroutine(int i)
    {
        while (isVerticalChange && i * Vertical < 1)
        {
            Vertical += speed * i;
            yield return new WaitForSeconds(0.1f);


        }
    }

    public void VerticalButtonUp()
    {
        isVerticalChange = false;
        Vertical = 0;
    }
    public void ShootingBullet()
    {
        shootingBullet = true;
    }
    public void FinishShootingBullet()
    {
        shootingBullet = false;
    }
    public void ShootingRocket()
    {
        shootingRocket = true;
    }
    public void FinishShootingRocket()
    {
        shootingRocket = false;
    }
    public void GetOut()
    {
        getOut = true;
    }
    //public void FinishShootingRocket()
    //{
    //    shootingRocket = false;
    //}

}
