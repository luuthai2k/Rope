using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Dissable : MonoBehaviour
{
    public Image dissableimg;
    public float time;
   public void OnEnable()
    {
        dissableimg.fillAmount = 1;
        DOTween.To(() => dissableimg.fillAmount, x => dissableimg.fillAmount = x, 0, time)
            .OnComplete(CanClick);

    }
    public void CanClick()
    {
        gameObject.SetActive(false);
    }
}
