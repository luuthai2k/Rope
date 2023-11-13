using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreViewManager : MonoBehaviour
{
    public static PreViewManager ins;

    [SerializeField]
    private GameObject preview;
    public void Awake()
    {
        ins = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        OffPreview();
    }

    public void OnPreview()
    {
        preview.SetActive(true);
    }

    public void OffPreview()
    {
        preview.SetActive(false);
    }
}
