using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrafficLightManager : MonoBehaviour
{
    
   public List<Direction> direction;
    public float timeRed;
    public float timeGreen;
    public float timeYellow;
    void Start()
    {
        
    }


    
}
[Serializable]
public struct Direction 
{
   public List<TrafficLight> trafficLight;
}

