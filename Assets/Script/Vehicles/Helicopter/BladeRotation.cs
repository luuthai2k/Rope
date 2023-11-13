using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeRotation : MonoBehaviour
{
    public GameObject mainBlade;
    public GameObject tailBlade;
    private Vector3 mainrotation;
    private Vector3 tailrotation;
    public float bladeSpeed=1000;
    public float BladeSpeed
    {
        get { return bladeSpeed; }
        set { bladeSpeed = Mathf.Clamp(value, 0, 1000); }
    }
    private float rotateDegree;
    void Start()
    {
        mainrotation = mainBlade.transform.localEulerAngles;
        tailrotation = tailBlade.transform.localEulerAngles;
    }

    
    public void BladeRotate()
    {
        rotateDegree += BladeSpeed * Time.deltaTime;
        mainBlade.transform.localRotation = Quaternion.Euler(mainrotation.x, rotateDegree, mainrotation.z);
        tailBlade.transform.localRotation = Quaternion.Euler(rotateDegree, tailrotation.y, tailrotation.z);
    }
}
