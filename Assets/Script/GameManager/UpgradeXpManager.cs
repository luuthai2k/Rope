using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeXpManager : MonoBehaviour
{
    public static UpgradeXpManager ins;

    [SerializeField]
    private float _Hp, _dame, _stamina, _rechargeStamina, _speed, _laserDame, _laserRange, _rocketDame, _rocketRange, _backHole;

    //[SerializeField]
    //private float _Hp,_dame,_stamina, _rechargeStamina, _speed, _laserDame, _laserRange, _rocketDame, _rocketRange, _backHole;

    [SerializeField]
    private CharacterParameter characterParameter;

    public DataGame dataGame;

    [SerializeField]
    private string dataname = "datagame";

    public void Awake()
    {
        ins = this;
        dataGame = new DataGame();
        if (PlayerPrefs.HasKey(dataname))
        {
            string data = PlayerPrefs.GetString(dataname);
            dataGame = JsonConvert.DeserializeObject<DataGame>(data);

        }
        else
        {
            SaveData();
        }
    }

    public void UpgradeIndex(int _index)
    {
        if(_index == 1)
        {
            UpgradeHp();
        }
        else if(_index == 2) 
        {
            UpgradeStamina();
        }
        else if(_index == 3) 
        {
            UpgradeMeleeDame();                     
        }
        else if(_index == 5)
        {
            UpgradeSprintSpeed();
        }
        else if(_index == 4)
        {
            UpgradeStaminaRecharge();
        }
        else if(_index == 6)
        {
            UpgradeLaserDamage();
        }
        else if(_index == 7)
        {
            UpgradeLaserRange();
        }
        else if(_index == 8)
        {
            UpgradeRocketDame();
        }
        else if(_index == 9)
        {
            UpgradeRocketRange();
        }
        else if(_index == 10)
        {
            UpgradeBackHole();
        }
    }


    public void UpgradeHp()
    {
        dataGame.maxHp += _Hp;
        dataGame.levelHp += 1;
        SaveData();
    }

    public void UpgradeStamina()
    {
        dataGame.maxStamina += _stamina;
        dataGame.levelstamina += 1;
        SaveData();
    }

    public void UpgradeMeleeDame()
    {
        dataGame.maxMeleeDame += _dame;
        dataGame.leveldame += 1;
        SaveData();
    }

    public void UpgradeStaminaRecharge()
    {
        dataGame.maxStaminaRecharge += _rechargeStamina;
        dataGame.levelrechargeStamina += 1;
        SaveData();
    }

    public void UpgradeSprintSpeed()
    {
        dataGame.maxSprintSpeed += _speed;
        dataGame.levelspeed += 1;
        SaveData();
    }

    public void UpgradeLaserDamage()
    {
        dataGame.maxLaserDamage += _laserDame;
        dataGame.levellaserDame += 1;
        SaveData();
    }
    public void UpgradeLaserRange()
    {
        dataGame.maxLaserRange += _laserRange;
        dataGame.levellaserRange += 1;
        SaveData();
    }
    public void UpgradeRocketDame()
    {
        dataGame.maxRocketDame += _rocketDame;
        dataGame.levelrocketDame += 1;
        SaveData();
    }
    public void UpgradeRocketRange()
    {
        dataGame.maxRocketRange += _rocketRange;
        dataGame.levelrocketRange += 1;
        SaveData();
    }
    public void UpgradeBackHole()
    {
        dataGame.maxBackHole += _backHole;
        dataGame.levelbackHole += 1;
        SaveData();
    }


    public void SaveData()
    {
        PlayerPrefs.SetString(dataname, JsonConvert.SerializeObject(dataGame));
    }
}

public class DataGame
{
    [SerializeField]
    private float _Hp, _dame, _stamina, _rechargeStamina, _speed, _laserDame, _laserRange, _rocketDame, _rocketRange, _backHole;

    public int  levelHp, leveldame, levelstamina, levelrechargeStamina, levelspeed, levellaserDame, levellaserRange, levelrocketDame, levelrocketRange, levelbackHole;


    public float maxHp { get => _Hp; set => _Hp = value; }
    public float maxMeleeDame { get => _dame; set => _dame = value; }
    public float maxStamina { get => _stamina; set => _stamina = value; }
    public float maxStaminaRecharge { get => _rechargeStamina; set => _rechargeStamina = value; }
    public float maxSprintSpeed { get => _speed; set => _speed = value; }
    public float maxLaserDamage { get => _laserDame; set => _laserDame = value; }
    public float maxLaserRange { get => _laserRange; set => _laserRange = value; }
    public float maxRocketDame { get => _rocketDame; set => _rocketDame = value; }
    public float maxRocketRange { get => _rocketRange; set => _rocketRange = value; }
    public float maxBackHole { get => _backHole; set => _backHole = value; }


    public DataGame()
    {
        maxHp = 500;
        maxMeleeDame = 20;
        maxStamina = 100;
        maxStaminaRecharge = 2.22f;
        maxSprintSpeed = 0.5f;
        maxLaserDamage = 30;
        maxLaserRange = 50;
        maxRocketDame = 150;
        maxRocketRange = 10;
        maxBackHole = 25;

    }
}

