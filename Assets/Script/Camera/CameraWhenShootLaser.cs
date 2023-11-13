using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraWhenShootLaser : MonoBehaviour
{
    public CinemachineFreeLook freeLookCam;
    public float topheight=1;
    public float midheight=0;
    public float botheight=-1;
    public float toprad=2;
    public float midrad=2;
    public float botrad=2;
    private float topheightbase;
    private float midheightbase;
    private float botheightbase;
    private float topradbase;
    private float midradbase;
    private float botradbase;
    private void Start()
    {
        botheightbase = freeLookCam.m_Orbits[2].m_Height;
        midheightbase = freeLookCam.m_Orbits[1].m_Height;
        topheightbase = freeLookCam.m_Orbits[0].m_Height;
        botradbase = freeLookCam.m_Orbits[2].m_Radius;
        midradbase = freeLookCam.m_Orbits[1].m_Radius;
        topradbase = freeLookCam.m_Orbits[0].m_Radius;
    }
    public void SetCamWhenShootLaser()
    {
        DOTween.To(() => freeLookCam.m_Orbits[2].m_Height, x => freeLookCam.m_Orbits[2].m_Height = x, botheight, 0.5f);
        DOTween.To(() => freeLookCam.m_Orbits[1].m_Height, x => freeLookCam.m_Orbits[1].m_Height = x, midheight, 0.5f);
        DOTween.To(() => freeLookCam.m_Orbits[0].m_Height, x => freeLookCam.m_Orbits[0].m_Height = x, topheight, 0.5f);
        DOTween.To(() => freeLookCam.m_Orbits[2].m_Radius, x => freeLookCam.m_Orbits[2].m_Radius = x, toprad, 0.5f);
        DOTween.To(() => freeLookCam.m_Orbits[1].m_Radius, x => freeLookCam.m_Orbits[1].m_Radius = x, midrad, 0.5f);
        DOTween.To(() => freeLookCam.m_Orbits[0].m_Radius, x => freeLookCam.m_Orbits[0].m_Radius = x, midrad, 0.5f);
      
    }
    public void ReturnCamBase()
    {
       
        DOTween.To(() => freeLookCam.m_Orbits[2].m_Height, x => freeLookCam.m_Orbits[2].m_Height = x, botheightbase, 0.5f);
        DOTween.To(() => freeLookCam.m_Orbits[1].m_Height, x => freeLookCam.m_Orbits[1].m_Height = x, midheightbase, 0.5f);
        DOTween.To(() => freeLookCam.m_Orbits[0].m_Height, x => freeLookCam.m_Orbits[0].m_Height = x, topheightbase, 0.5f);
        DOTween.To(() => freeLookCam.m_Orbits[2].m_Radius, x => freeLookCam.m_Orbits[2].m_Radius = x, topradbase, 0.5f);
        DOTween.To(() => freeLookCam.m_Orbits[1].m_Radius, x => freeLookCam.m_Orbits[1].m_Radius = x, midradbase, 0.5f);
        DOTween.To(() => freeLookCam.m_Orbits[0].m_Radius, x => freeLookCam.m_Orbits[0].m_Radius = x, midradbase, 0.5f);

    }
}
