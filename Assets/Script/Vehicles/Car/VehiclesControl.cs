using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclesControl : MonoBehaviour
{
    public float Horizontal;
    public float Vertical;
    private bool isHorizontalChange;
    private bool isVerticalChange;
    public void Start()
    {
        
    }
    public void HorizontalUp()
    {
        isHorizontalChange = true;
        StartCoroutine(HorizontalCouroutine(1));    
    }
    public void HorizontalDown()
    {
        isHorizontalChange = true;
        StartCoroutine(HorizontalCouroutine(-1));
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
    IEnumerator HorizontalCouroutine(int i)
    {
        while (isHorizontalChange && i*Horizontal < 1)
        {
            Horizontal += 0.2f * i;
            yield return new WaitForSeconds(0.1f);


        }
    }
    IEnumerator VerticalCouroutine(int i)
    {
        while (isVerticalChange && i*Vertical < 1)
        {
            Vertical += 0.2f * i;
            yield return new WaitForSeconds(0.1f);


        }
    }

    public void HorizontalButtonUp()
    {
        isHorizontalChange = false;
        Horizontal = 0;
    }
    public void VerticalButtonUp()
    {
        isVerticalChange = false;
        Vertical = 0;
    }
}
