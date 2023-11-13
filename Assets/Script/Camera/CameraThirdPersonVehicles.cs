using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraThirdPersonVehicles : MonoBehaviour
{
    public CinemachineFreeLook freeLookCam;
    public float verticalAxisValue;
    public float horizontalAxisValue;
    

    private void OnEnable()
    {
        freeLookCam.m_XAxis.Value = horizontalAxisValue;
        freeLookCam.m_YAxis.Value = verticalAxisValue;


    }
   public void TargetCam(Transform target)
    {
        freeLookCam.Follow= target;
        freeLookCam.LookAt = target;

    }
}
