using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Camera gameCamera, pauseCamera;

    [SerializeField]
    private GameObject gameUi, pauseUi,minimap,mainPanel;

    [SerializeField]
    private Canvas uiCanvas;

    [SerializeField]
    private MinimapManager minimapManager;

    [SerializeField]
    private ShopManager shopUI;

    [SerializeField]
    private GameObject setting;

    //[SerializeField]
    //private 

    public void InGame()
    {
        gameCamera.gameObject.SetActive(true);
        pauseCamera.gameObject.SetActive(false);
        gameUi.SetActive(true);
        pauseUi.SetActive(false);
        minimapManager.OnMinimapUi();
        uiCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        gameCamera.gameObject.SetActive(false);
        pauseCamera.gameObject.SetActive(true);
        gameUi.SetActive(false);
        pauseUi.SetActive(true);
        minimapManager.OnBgMiniMap();
        uiCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        
        Time.timeScale = 0;

        //uiCanvas.
    }

    public void OnShop()
    {
       
        shopUI.OnShop();
    }

    public void BackShop()
    {
     
        shopUI.BackShop();
    }

    public void OnSetting()
    {
        setting.SetActive(true);
        minimap.SetActive(false);
      
    }

    public void OnMinimap()
    {
        minimap.SetActive(true);
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InGame();
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            PauseGame();
        }

    }


    public void ButtonAddArmor()
    {
        if(ItemManager.ins.itemData.armor > 0)
        {
            Player.ins.playerHP.PlusArmor(10);
            ItemManager.ins.AddArmor(-1);
        }
    }


    public void buttonAddHp()
    {
        if (ItemManager.ins.itemData.hpkit > 0)
        {
            Player.ins.playerHP.PlusHp(10);
            ItemManager.ins.AddHPKit(-1);
        }
    }
}

