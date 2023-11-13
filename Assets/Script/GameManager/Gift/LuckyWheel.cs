using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LuckyWheel : MonoBehaviour
{
    [SerializeField]
    private List<LuckyGift> luckyGifts;

    [SerializeField]
    private GameObject spin;

    [SerializeField]
    private GameObject spinads, spinfree,spinLock;

    [SerializeField]
    private float speed,rotationIndex;

    [SerializeField]
    private GameObject uiLucky;

    [SerializeField]
    private bool istime,isSpintime;

    [SerializeField]
    private DateTime timeSpin; 

    private bool isSpin;

    [SerializeField]
    private Text textTime;

    [SerializeField]
    private GameObject timeUi;

    public void Awake()
    {
        uiLucky.SetActive(false);

    }

    public void Start()
    {
        isSpin = false;
        spinLock.SetActive(false);
        timeUi.SetActive(false);
        if (istime)
        {
            Invoke("OnLuckyWheel", 150f);
        }
       
    }

    public void Init()
    {
        //rotationIndex = 10;
        spin.transform.rotation = Quaternion.EulerAngles(0, 0, 0);
    }


    public void Update()
    {
        if(isSpin)
        {

            if (rotationIndex > 0)
            {
                speed += Time.unscaledDeltaTime * rotationIndex;
                spin.transform.rotation = Quaternion.EulerAngles(0, 0, speed);

                rotationIndex -= Time.unscaledDeltaTime * 2;

            }
            else
            {

                Claim();
                spinLock.SetActive(false);
                isSpin = false;
            }
        }
        if (isSpintime )
        {
            var time = timeSpin - DateTime.Now;
           
            textTime.text = string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds);
            if (CheckTime())
            {
                isSpintime = false;
                spinfree.SetActive(true);
                spinads.SetActive(false);
                timeUi.SetActive(false);
            }
        }

    }
    public void LuckyWheelSpin()
    {
        isSpin = true;
        spinLock.SetActive(true);
    }

    public bool CheckTime()
    {

        if(timeSpin > DateTime.Now)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void Spin()
    {
        rotationIndex = UnityEngine.Random.Range(10f,20f);

        timeSpin = DateTime.Now.AddMinutes(1);

        timeUi.SetActive(true);

        isSpintime = true;

        CheckTime();
        LuckyWheelSpin();

    }

    public void SpinAds()
    {
        rotationIndex = UnityEngine.Random.Range(10f, 20f);

        LuckyWheelSpin();
    }

    public void ClaimX2()
    {
        double x = spin.transform.rotation.eulerAngles.z / 45;
        int index = (int)System.Math.Truncate(x);

        for (int i = 0; i < luckyGifts.Count; i++)
        {
            if (i == index)
            {
                luckyGifts[i].Claim(); 
            }
        }

    }

    public void Claim()
    {  
        double x = spin.transform.rotation.eulerAngles.z / 45;
        int index = (int)System.Math.Truncate(x);
        
        for(int i = 0; i < luckyGifts.Count; i++)
        {
            if(i == index)
            {
                luckyGifts[i].Claim();
            }
        }
        spinfree.SetActive(false);
        spinads.SetActive(true);
        if (istime)
        {
            Invoke("OnLuckyWheel", 300f);
        }
        
    }

  
    public void OnLuckyWheel()
    {
        uiLucky.SetActive(true);
        spinfree.SetActive(true);
        spinads.SetActive(false);
        Time.timeScale = 0;
        
    }

    public void EndLuckyWheel()
    {
        uiLucky.SetActive(false);
        
        Time.timeScale = 1;

        if (istime)
        {
            Invoke("OnLuckyWheel", 300f);
        }
    }
}
