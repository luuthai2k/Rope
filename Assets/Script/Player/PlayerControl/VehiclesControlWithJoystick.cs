using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclesControlWithJoystick : MonoBehaviour
{
  
    public float Vertical;
    private bool isVerticalChange;
    public Joystick joystick;
    public float speed=0.1f;
    public void Start()
    {

    }
    

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
}
