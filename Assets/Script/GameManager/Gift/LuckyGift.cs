using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyGift : MonoBehaviour
{
    [SerializeField]
    private List<GiftBonus> gifts;
    [SerializeField]
    private List<LuckyPopupGift> luckyPopupGifts;

    public void Claim()
    {
        for (int i = 0; i < gifts.Count; i++)
        {
            if (gifts[i].gift == Gift.hpKit)
            {
                ItemManager.ins.AddHPKit(gifts[i].indexGift);
                luckyPopupGifts[1].gameObject.SetActive(true);
                luckyPopupGifts[1].Init("X" + gifts[i].indexGift);
            }
            if (gifts[i].gift == Gift.armor)
            {
                ItemManager.ins.AddArmor(gifts[i].indexGift);
                luckyPopupGifts[0].gameObject.SetActive(true);
                luckyPopupGifts[0].Init("X" + gifts[i].indexGift);
            }
            if (gifts[i].gift == Gift.spin)
            {
                ItemManager.ins.AddSpin(gifts[i].indexGift);
            }
            if (gifts[i].gift == Gift.money)
            {
                ItemManager.ins.AddMoney(gifts[i].indexGift);
                luckyPopupGifts[2].gameObject.SetActive(true);
                luckyPopupGifts[2].Init("X" + gifts[i].indexGift);
            }

        }
    }
}
