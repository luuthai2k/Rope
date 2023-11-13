using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    public static ControlsManager ins;
    public List<GameObject> Control;
  

    private GameObject currentControl;
    private void Awake()
    {
        ins = this;
    }
    void Start()
    {
        Control[0].SetActive(true);
        currentControl = Control[0];
     
    }

    public void ChangeControl(int i)
    {
        currentControl.SetActive(false);
        Control[i].SetActive(true);
        currentControl = Control[i];
    }
}
