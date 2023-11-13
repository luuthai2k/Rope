using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardManager : MonoBehaviour
{
    [SerializeField]
    private List<DailyReward> dailyRewards;

    [SerializeField]
    private GameObject buttonClaim, buttonClaimX2, buttonText;

    [SerializeField]
    private DateTime timeDaily;

    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private int dailyDay;

    [SerializeField]
    private GameObject dailyPopup;

    public void Start()
    {
        Time.timeScale = 0;
        dailyDay = PlayerPrefs.GetInt("DailyReward");
        //InitDaily();
        if (PlayerPrefs.HasKey("TimeDaily"))
        {
            timeDaily = DateTime.Parse(PlayerPrefs.GetString("TimeDaily"));
            //timeDaily.AddDays(-1);
            InitDaily(false);
        }
        else
        {
            timeDaily = DateTime.Now;
            timeDaily = timeDaily.AddDays(-1);
            PlayerPrefs.SetString("TimeDaily", timeDaily.ToString());
            InitDaily(true);

        }
        //dailyPopup.SetActive(true);

       
 
    }

    //public void Update()
    //{
    //    TimeClaim();
    //}


    public void InitDaily(bool _isdayOne)
    {
       
        for (int i = 0; i < dailyRewards.Count; i++)
        {
            if (i < dailyDay)
            {
                dailyRewards[i].LastDaily();
            }
            else if (i == dailyDay)
            {
                dailyRewards[i].NowDaily();
            }
            else
            {
                dailyRewards[i].FutureDaily();
            }
        }
        ButtonClaim(_isdayOne);
    }

    public void Claim()
    {
        
        dailyRewards[dailyDay].Claim();

        PlayerPrefs.SetInt("DailyReward", PlayerPrefs.GetInt("DailyReward") +1);
        dailyDay++;
        if(dailyDay >= 6)
        {
            dailyDay = 0;
            PlayerPrefs.SetInt("DailyReward", 0);
        }

        timeDaily = DateTime.Now.AddDays(1);
        PlayerPrefs.SetString("TimeDaily", timeDaily.ToString());
        InitDaily(false);
        EndDailyReward();

        
    }

    public void ClaimX2()
    {
        dailyRewards[dailyDay].Claim();
        dailyRewards[dailyDay].Claim();

        PlayerPrefs.SetInt("DailyReward", PlayerPrefs.GetInt("DailyReward") + 1);

        timeDaily = DateTime.Now.AddDays(1);
        PlayerPrefs.SetString("TimeDaily", timeDaily.ToString());
        EndDailyReward();


    }

    public void ButtonClaim(bool isdayone)
    {
        if (isdayone)
        {
            buttonClaim.SetActive(true);
            buttonClaimX2.SetActive(true);
            buttonText.SetActive(false);
            timeText.enabled = false;
            dailyPopup.SetActive(true);
            
        }
        else
        {

            if(timeDaily.Month == DateTime.Now.Month)
            {
                if (timeDaily.Day <= DateTime.Now.Day)
                {
                    buttonClaim.SetActive(true);
                    buttonClaimX2.SetActive(true);
                    buttonText.SetActive(false);
                    timeText.enabled = false;
                    dailyPopup.SetActive(true);
                    
                }
                else
                {
                    buttonClaim.SetActive(false);
                    buttonClaimX2.SetActive(false);
                    buttonText.SetActive(true);
                    timeText.enabled = true;
                    dailyPopup.SetActive(false);
                    
                    Time.timeScale = 1;
                    //TimeClaim();
                }
            }
            else
            {
                buttonClaim.SetActive(true);
                buttonClaimX2.SetActive(true);
                buttonText.SetActive(false);
                timeText.enabled = false;
                dailyPopup.SetActive(true);
                
                //TimeClaim();
            }

        }
        
       
    }


    public void AdsSkipTime()
    {
        timeDaily = DateTime.Now;
    }


    public void EndDailyReward()
    {
        dailyPopup.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnDaily()
    {
        InitDaily(false);
        dailyPopup.SetActive(true);
    }

}
