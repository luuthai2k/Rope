using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingManager : MonoBehaviour
{

    public Slider slider;
    private RectTransform handleRect;
    private float handleWidth;


    private void Start()
    {

        handleRect = slider.handleRect;
        handleWidth = handleRect.rect.width;
    }



    public void UpdateSpeedSensitivity()
    {
        FreeLookCameraControl.ins._touchSpeedSensitivity = slider.value;
    }


}
