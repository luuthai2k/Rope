using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
    [SerializeField]
    private Image bg,lockBg,icon,popup;
    [SerializeField]
    private Sprite nowbg, lastbg, futurebg;

    [SerializeField]
    private Sprite nowpopup, futurepopup,lastpopup;
    [SerializeField]
    private Sprite onicon, officon;

    [SerializeField]
    private List<GiftBonus> gifts;
    public void NowDaily()
    {
        lockBg.enabled = false;
        bg.sprite = nowbg;
        popup.sprite = nowpopup;
        icon.sprite = onicon;
        bg.SetNativeSize();
        popup.SetNativeSize();
    }

    public void LastDaily()
    {
        lockBg.enabled = true;
        bg.sprite = lastbg;
        popup.sprite = lastpopup;
        icon.sprite = officon;
        bg.SetNativeSize();
        popup.SetNativeSize();
    }

    public void FutureDaily()
    {
        lockBg.enabled = false;
        bg.sprite = futurebg;
        popup.sprite = futurepopup;
        icon.sprite = onicon;
        bg.SetNativeSize();
        popup.SetNativeSize();
    }

    public void Claim()
    {
        for (int i = 0; i < gifts.Count; i++)
        {
            if (gifts[i].gift == Gift.hpKit)
            {
                ItemManager.ins.AddHPKit(gifts[i].indexGift);
            }
            if (gifts[i].gift == Gift.armor)
            {
                ItemManager.ins.AddArmor(gifts[i].indexGift);
            }
            if (gifts[i].gift == Gift.spin)
            {
                ItemManager.ins.AddSpin(gifts[i].indexGift);
            }
            if (gifts[i].gift == Gift.money)
            {
                ItemManager.ins.AddMoney(gifts[i].indexGift);
            }
            
        }
        
    }
}
