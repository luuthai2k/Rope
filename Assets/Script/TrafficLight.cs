using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public GameObject redLight;
    public GameObject greenLight;
    public GameObject yellowLight;
    public GameObject Collider;
   
    public void Red()
    {
        redLight.SetActive(true);
        yellowLight.SetActive(false);
        greenLight.SetActive(false);
        Collider.SetActive(true);
    }
    public void Green()
    {
        greenLight.SetActive(true);
        redLight.SetActive(false);
        yellowLight.SetActive(false);
    }
    public void Yellow()
    {
        yellowLight.SetActive(true);
        greenLight.SetActive(false);
        redLight.SetActive(false);
        Collider.SetActive(false);

    }

}
