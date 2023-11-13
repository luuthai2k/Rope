using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public delegate void JoystickDelegate();
public class FixedJoystick : Joystick
{
    public static event JoystickDelegate enter;
    public static event JoystickDelegate exit;
    public float forward;
    protected override void Start()
    {
        base.Start();
       
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);        
        

    }

    public override void OnPointerUp(PointerEventData eventData)
    {
       
        base.OnPointerUp(eventData);
       
    }
}