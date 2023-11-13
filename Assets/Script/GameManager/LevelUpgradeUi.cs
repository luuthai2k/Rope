using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpgradeUi : MonoBehaviour
{
    [SerializeField]
    private Sprite lastLv, nowLv, futureLv;

    [SerializeField]
    private Image bg;

    [SerializeField]
    private Image lockUi,icon,levelIcon;

    [SerializeField]
    private UpgradeUi upgradeUi;

    [SerializeField]
    private Lv lv;

    [SerializeField]
    private int money;

    [SerializeField]
    private string describe, level, upgradeText;

    [SerializeField]
    private int index;

    [SerializeField]
    private LevelUpgradeUi futureLevel;


    public void Add(Image _icon,Image _levelIcon)
    {
        icon = _icon;
        levelIcon = _levelIcon;
    }

    public void Init(Lv _lv)
    {

        lv = _lv;

        if (_lv == Lv.last)
        {
            bg.sprite = lastLv;
            lockUi.enabled = false;
        }
        else if(_lv == Lv.now)
        {
            bg.sprite = nowLv;
            lockUi.enabled = false;
        }
        else
        {
            bg.sprite = futureLv;
            lockUi.enabled = true;
        }
        
    }

    public void ClickButton()
    {
        if (lv == Lv.last)
        {
            upgradeUi.SetMoney(money, true);
        }
        else if (lv == Lv.now)
        {
            upgradeUi.SetMoney(money, false);
        }
        else
        {
            upgradeUi.SetMoney(money, false);
        }
        upgradeUi.Initdescribe(describe, level, upgradeText, icon.sprite, levelIcon.sprite, index, futureLevel);
    }
}

public enum Lv
{
    last,
    future,
    now
}
