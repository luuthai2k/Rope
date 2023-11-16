using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toollight : MonoBehaviour
{
    public TrafficLight trafficLight;
    public GameObject traffic;
    public GameObject colider;
   


    private void Reset()
    {
        
        trafficLight = gameObject.GetComponent<TrafficLight>();
            traffic= transform.Find("trafficLight").gameObject;
            trafficLight.redLight= traffic.transform.Find("RedLightTfL").gameObject;
        trafficLight.greenLight = traffic.transform.Find("GreenLightTfL").gameObject;
        GameObject yellow = Instantiate(trafficLight.greenLight, traffic.transform);
            trafficLight.yellowLight = yellow;
       
    }
   
}
