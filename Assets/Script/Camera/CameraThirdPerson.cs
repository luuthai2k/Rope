using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;

public class CameraThirdPerson : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;  
    public CinemachinePOV aimPOV;
    public Joystick joystick;
    private void Start()
    {
        //freeLookCamera.m_Heading.m_Bias = -20f;
    }
    private void Update()
    {
        
        //if (Input.GetMouseButton(0))
        //{
        //    CamSetUp(-20);
        //    return;
        //}
        //else
        //{
        //    CamSetUp(0);
        //}
    }
    public void CamSetUp(float targetBias )
    {
        DOTween.To(() => freeLookCamera.m_Heading.m_Bias, x => freeLookCamera.m_Heading.m_Bias = x, targetBias, 0.5f);
    }

}
