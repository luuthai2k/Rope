using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class StaminaBonus : MonoBehaviour
{
    public static StaminaBonus ins;
    [SerializeField]
    private GameObject staminaPopup;

    public void Awake()
    {
        ins = this; 
    }

    public void Init()
    {
        staminaPopup.SetActive(true);
        Time.timeScale = 0;
    }

    public void Claim()
    {
        staminaPopup.SetActive(false);
        Time.timeScale = 1;
        Player.ins.playerHP.PlusStamina(10, true);
    }

    public void Exit()
    {
        staminaPopup.SetActive(false);
        Time.timeScale = 1;
    }
}
