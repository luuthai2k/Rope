using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusUIPopup : MonoBehaviour
{

    [SerializeField]
    private Image armor, money, hpkit;

    [SerializeField]
    private TextMeshProUGUI meshPro;

    [SerializeField]
    private string textArmor, textMoney, textHpkit;
    public void Init(int _index)
    {
        if(_index == 0)
        {
            armor.enabled = true;
            meshPro.text = textArmor;
            money.enabled = false;
            hpkit.enabled = false;
        }
        else if(_index == 1)
        {
            armor.enabled = false;
            money.enabled = true;
            meshPro.text = textMoney;
            hpkit.enabled = false;
        }
        else if(_index == 2)
        {
            armor.enabled = false;
            money.enabled = false;
            hpkit.enabled = true;
            meshPro.text = textHpkit;
        }
    }

    
}
