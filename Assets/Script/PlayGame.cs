using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private Image loading;
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(2);
        button.enabled = false;
        loading.gameObject.SetActive(true);
    }
}
