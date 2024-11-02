using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject SettingsPanel;

    
    void Update()
    {
        
    }
    public void SettingButton()
    {
        MainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }
    public void HomeButton()
    {
        MainMenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }
    public void StartgameButton()
    {
        SceneManager.LoadScene("StartingScene");
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
