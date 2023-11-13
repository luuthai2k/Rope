using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager ins;
    public LayerData layerData;

    [SerializeField]
    private UiManager uiManager;
    void Awake()
    {
        ins = this;
    }

    public void Start()
    {
        
    }
}
