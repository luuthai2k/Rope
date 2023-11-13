using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Loading : MonoBehaviour
{

    public void Start()
    {
        
        Invoke("LoadGameMenu", 3f);
    }



    public void LoadGameMenu()
    {      

        SceneManager.LoadScene(1);
        
    }

}
