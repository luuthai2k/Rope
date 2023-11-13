using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanGetOut : MonoBehaviour
{
    public GameObject getOut;
    public float waittime;
    private void OnEnable()
    {
        getOut.SetActive(false);
        Invoke("CanGetout", waittime);
    }
    void CanGetout()
    {
        getOut.SetActive(true);
    }
}
