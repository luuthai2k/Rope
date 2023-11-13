using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUi : MonoBehaviour
{
    [SerializeField]
    private UpgradeXpManager upgrade;

    [SerializeField]
    private List<LevelUpgradeUi> Hp, dame, stamina, rechargeStamina, speed, laserDame, laserRange, rocketDame, rocketRange, backHole;

    [SerializeField]
    private GameObject upgraded, canUpgrade, noUpgrade;

    [SerializeField]
    private Text textCanUpgrade, textNoUpgrade,text;

    [SerializeField]
    private TextMeshProUGUI describe, level, upgradeText;

    [SerializeField]
    private Image iconUpgrade,iconLevel,bgicon;

    [SerializeField]
    private List<Image> Connect;

    [SerializeField]
    private GameObject popup;

    [SerializeField]
    private int upgradeMoney,index;

    [SerializeField]
    private LevelUpgradeUi futureButton;

    public void Start()
    {
        InitUpgrade();
        Initdescribe("", "", "", null, null,0,null);
        InitMoney();
    }
    public void OnUpgradeUi()
    {
        popup.SetActive(true);
        InitUpgrade();
        Initdescribe("", "", "", null, null,0,null);
        InitMoney();
    }
    public void BackUpgradeUi()
    {
        popup.SetActive(false);
        InitUpgrade();
        Initdescribe("", "", "", null, null,0,null);
        InitMoney();
    }


    public void InitMoney()
    {
        text.text = ItemManager.ins.itemData.money + "";
    }


    public void Initdescribe(string _describe,string _level,string _upgrade, Sprite _iconUpgrade,Sprite _iconLevel,int _index,LevelUpgradeUi _furure)
    {

        describe.text = _describe;
        level.text = _level;
        upgradeText.text = _upgrade;
        index = _index;

        if(_furure != null)
        {
            futureButton = _furure;
        }
        

        if(_iconUpgrade != null)
        {
            iconUpgrade.sprite = _iconUpgrade;
            iconUpgrade.enabled = true;
            bgicon.enabled = true;
            iconUpgrade.SetNativeSize();
        }
        else
        {
            iconUpgrade.enabled = false;
            bgicon.enabled = false;
        }

        if (_iconLevel != null)
        {
            iconLevel.sprite = _iconLevel;
            iconLevel.enabled = true;
            iconLevel.SetNativeSize();

        }
        else
        {
            iconLevel.enabled = false;
        }

    }

    public void SetMoney(int _indexMoney, bool isUpgrade)
    {
        textCanUpgrade.text = _indexMoney.ToString();
        textNoUpgrade.text = _indexMoney.ToString();
        upgradeMoney = _indexMoney;
        if (isUpgrade)
        {
            upgraded.SetActive(true);
            canUpgrade.SetActive(false);
            noUpgrade.SetActive(false);
        }
        else
        {
            upgraded.SetActive(false);

            if (ItemManager.ins.itemData.money >= _indexMoney)
            {
                canUpgrade.SetActive(true);
                noUpgrade.SetActive(false);
            }
            else
            {
                canUpgrade.SetActive(false);
                noUpgrade.SetActive(true);
            }
        }
        InitMoney();
    }

    public void InitUpgrade()
    {
        for (int i = 0; i < Hp.Count; i++) 
        {
            if(i < upgrade.dataGame.levelHp)
            {
                Hp[i].Init(Lv.last);
            }
            else if(i == upgrade.dataGame.levelHp)
            {
                Hp[i].Init(Lv.now);
            }
            else
            {
                Hp[i].Init(Lv.future);
            }

            //Debug.LogError(upgrade.dataGame.levelHp);
            Connect[0].fillAmount = (upgrade.dataGame.levelHp ) / 5 + 0.1f;
        }
        for (int i = 0; i < dame.Count; i++)
        {
            if (i < upgrade.dataGame.leveldame)
            {
                dame[i].Init(Lv.last);
            }
            else if (i == upgrade.dataGame.leveldame)
            {
                dame[i].Init(Lv.now);
            }
            else
            {
                dame[i].Init(Lv.future);
            }
            Connect[1].fillAmount = (upgrade.dataGame.leveldame ) / 5 + 0.1f;
        }
        for (int i = 0; i < stamina.Count; i++)
        {
            if (i < upgrade.dataGame.levelstamina)
            {
                stamina[i].Init(Lv.last);
            }
            else if (i == upgrade.dataGame.levelstamina)
            {
                stamina[i].Init(Lv.now);
            }
            else
            {
                stamina[i].Init(Lv.future);
            }
            Connect[2].fillAmount = (upgrade.dataGame.levelstamina ) / 5 + 0.1f;
        }
        for (int i = 0; i < rechargeStamina.Count; i++)
        {
            if (i < upgrade.dataGame.levelrechargeStamina)
            {
                rechargeStamina[i].Init(Lv.last);
            }
            else if (i == upgrade.dataGame.levelrechargeStamina)
            {
                rechargeStamina[i].Init(Lv.now);
            }
            else
            {
                rechargeStamina[i].Init(Lv.future);
            }
            Connect[3].fillAmount = (upgrade.dataGame.levelrechargeStamina ) / 5 + 0.1f;
        }
        for (int i = 0; i < speed.Count; i++)
        {
            if (i < upgrade.dataGame.levelspeed)
            {
                speed[i].Init(Lv.last);
            }
            else if (i == upgrade.dataGame.levelspeed)
            {
                speed[i].Init(Lv.now);
            }
            else
            {
                speed[i].Init(Lv.future);
            }
            Connect[4].fillAmount = (upgrade.dataGame.levelspeed ) / 5 + 0.1f;
        }
        for (int i = 0; i < laserDame.Count; i++)
        {
            if (i < upgrade.dataGame.levellaserDame)
            {
                laserDame[i].Init(Lv.last);
            }
            else if (i == upgrade.dataGame.levellaserDame)
            {
                laserDame[i].Init(Lv.now);
            }
            else
            {
                laserDame[i].Init(Lv.future);
            }
            Connect[5].fillAmount = (upgrade.dataGame.levellaserDame ) / 5 + 0.1f;
        }
        for (int i = 0; i < laserRange.Count; i++)
        {
            if (i < upgrade.dataGame.levellaserRange)
            {
                laserRange[i].Init(Lv.last);
            }
            else if (i == upgrade.dataGame.levellaserRange)
            {
                laserRange[i].Init(Lv.now);
            }
            else
            {
                laserRange[i].Init(Lv.future);
            }
            Connect[6].fillAmount = (upgrade.dataGame.levellaserRange ) / 5 + 0.1f;
        }
        for (int i = 0; i < rocketDame.Count; i++)
        {
            if (i < upgrade.dataGame.levelrocketDame)
            {
                rocketDame[i].Init(Lv.last);
            }
            else if (i == upgrade.dataGame.levelrocketDame)
            {
                rocketDame[i].Init(Lv.now);
            }
            else
            {
                rocketDame[i].Init(Lv.future);
            }
            Connect[7].fillAmount = (upgrade.dataGame.levelrocketDame) / 5 + 0.1f;
        }
        for (int i = 0; i < rocketRange.Count; i++)
        {
            if (i < upgrade.dataGame.levelrocketRange)
            {
                rocketRange[i].Init(Lv.last);
            }
            else if (i == upgrade.dataGame.levelrocketRange)
            {
                rocketRange[i].Init(Lv.now);
            }
            else
            {
                rocketRange[i].Init(Lv.future);
            }
            Connect[8].fillAmount = (upgrade.dataGame.levelrocketRange) / 5 + 0.1f;
        }
        for (int i = 0; i < backHole.Count; i++)
        {
            if (i < upgrade.dataGame.levelbackHole)
            {
                backHole[i].Init(Lv.last);
            }
            else if (i == upgrade.dataGame.levelbackHole)
            {
                backHole[i].Init(Lv.now);
            }
            else
            {
                backHole[i].Init(Lv.future);
            }
            Connect[9].fillAmount = (upgrade.dataGame.levelHp ) / 5 + 0.1f;
        }
    }

    public void Upgrade()
    {
        ItemManager.ins.AddMoney(-upgradeMoney);
        upgrade.UpgradeIndex(index);
        InitUpgrade();
        futureButton.ClickButton();
        InitMoney();
    }
}
