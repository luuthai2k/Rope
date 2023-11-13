using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField]
    private BonusUIPopup bonusUIPopup;
    [SerializeField]
    private List<GiftBonus> giftBonus;
    [SerializeField]
    private GiftBonus gift;

    public void Start()
    {
        Invoke("Init", 180f);
    }

    public void Init()
    {
        bonusUIPopup.gameObject.SetActive(true);
        int _index = Random.Range(0, giftBonus.Count);
        gift = giftBonus[_index];
        bonusUIPopup.Init(_index);
        Time.timeScale = 0;
        Invoke("Init", 180f);
    }

    public void Claim()
    {
        if (gift.gift == Gift.hpKit)
        {
            ItemManager.ins.AddHPKit(gift.indexGift);
        }
        else if(gift.gift == Gift.armor)
        {
            ItemManager.ins.AddArmor(gift.indexGift);
        }
        else if(gift.gift == Gift.money)
        {
            ItemManager.ins.AddMoney(gift.indexGift);
        }
        bonusUIPopup.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitPopup()
    {
        bonusUIPopup.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
