using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager ins;
    public GameObject camThirdPeson;
    public GameObject camThirdPesonMotorandCar;
    public GameObject camThirdPesonTank;
    public GameObject camThirdPesonHelicopter;
    void Awake()
    {
        ins = this;
    }

    public void ChangeCam(int index,Transform target=null)
    {
        if (index == 0)
        {
            camThirdPeson.SetActive(true);
            camThirdPesonMotorandCar.SetActive(false);
            camThirdPesonTank.SetActive(false);
            camThirdPesonHelicopter.SetActive(false);

        }
        else if (index == 1)
        {
            camThirdPesonMotorandCar.SetActive(true);
            camThirdPeson.SetActive(false);
            camThirdPesonTank.SetActive(false);
            camThirdPesonHelicopter.SetActive(false);
            camThirdPesonMotorandCar.GetComponent<CameraThirdPersonVehicles>().TargetCam(target);
        }
        else if (index == 2)
        {
            camThirdPesonTank.SetActive(true);
            camThirdPesonMotorandCar.SetActive(false);
            camThirdPeson.SetActive(false);
            camThirdPesonHelicopter.SetActive(false);
            camThirdPesonTank.GetComponent<CameraThirdPersonVehicles>().TargetCam(target);
        }
        else if (index == 3)
        {
            camThirdPesonHelicopter.SetActive(true);
            camThirdPesonTank.SetActive(false);
            camThirdPesonMotorandCar.SetActive(false);
            camThirdPeson.SetActive(false);
            camThirdPesonHelicopter.GetComponent<CameraThirdPersonVehicles>().TargetCam(target);
        }
    }
}
