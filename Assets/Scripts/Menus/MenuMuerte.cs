using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuMuerte : MonoBehaviour
{
    public void RetryButton()
    {
        SceneManager.LoadScene("Unkonw_World"); 
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MenuInicial"); 
    }
}
