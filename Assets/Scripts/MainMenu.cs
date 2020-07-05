using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    private void Start()
    {
        AudioManager.instance.StopSound("LevelBgm");
        AudioManager.instance.PlaySound("MenuBgm");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void Laugh()
    {
        AudioManager.instance.PlaySound("Laugh");
    }
}
