using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject setting;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void TurnOnSetting()
    {
        Debug.LogError("check");
        setting.SetActive(true);
    }
}
