using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenuManager : MonoBehaviour
{
    public ItemData itemData;

    [SerializeField]
    private string itemDataName = "Itemdata";

    [SerializeField]
    private TextMeshProUGUI moneytext;

    private void Awake()
    {
        itemData = new ItemData();
        if (PlayerPrefs.HasKey(itemDataName))
        {
            string data = PlayerPrefs.GetString(itemDataName);
            itemData = JsonConvert.DeserializeObject<ItemData>(data);
        }
        SaveData();

    }

    public void Init()
    {
        moneytext.text = itemData.money + "";
    }

    public void SaveData()
    {
        PlayerPrefs.SetString(itemDataName, JsonConvert.SerializeObject(itemData));
        Init();
    }
}
