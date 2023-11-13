using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterAnimation : MonoBehaviour
{
    [SerializeField]
    private PlayerDriverHelicopter driverHelicopter;

    public void FinishOnHelicopter()
    {
        driverHelicopter.FinishGetIn();
    }

    
}
