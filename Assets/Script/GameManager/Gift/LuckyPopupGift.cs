using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LuckyPopupGift : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI indexGift;

    [SerializeField]
    private LuckyWheel luckyWheel;

    [SerializeField]
    private GameObject uiLucky;
    public void Init(string index)
    {
        indexGift.text = index;

    }

    public void Claim()
    {
        gameObject.SetActive(false);
        luckyWheel.Init();
        //Time.timeScale = 1;
    }

    public void ClaimX2()
    {
       
       

        luckyWheel.ClaimX2();
        luckyWheel.Init();
        gameObject.SetActive(false);
        //Time.timeScale = 1;
    }
}
