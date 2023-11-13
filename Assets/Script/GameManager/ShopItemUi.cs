using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUi : MonoBehaviour
{
    [SerializeField]
    private Image bg;

    public void OnItem()
    {
        bg.color = new Color32(255,255,255,255);
 
    }
    public void EndItem()
    {
        bg.color = new Color32(255, 255, 255, 0);
    
    }
}
