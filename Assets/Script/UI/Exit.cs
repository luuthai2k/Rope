using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameObject PopUPExit;

    [SerializeField]
    private UiManager uiManager;
    public void TurnOffPopUp()
    {
        PopUPExit.SetActive(false);
        uiManager.InGame();
    }
}
