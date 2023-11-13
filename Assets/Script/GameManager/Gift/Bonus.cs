using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField]
    private List<GiftBonus> gifts;

    [SerializeField]
    private int indexBonus;

    public void Claim()
    {
        if (gifts.Count != 0)
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
        else
        {
            BonusStamina();
        }
    }

    public void BonusStamina()
    {
        if(indexBonus == 0)
        {
            Player.ins.playerHP.PlusStamina(10, true);
        }
        else
        {
            Player.ins.playerHP.InfiniteStamina();
        }
    }
}
