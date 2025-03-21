using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{



    public void NewGame()
    {
        SceneManager.LoadScene("MainShip");
    }
    public void MainGameScene()
    {
        SceneManager.LoadScene("SecondVideoScene");
    }
    public void EndVideoScene()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void Settings()
    {
        
    }
    public void GameQuit()
    {
        Application.Quit();
    }
    public void LoadGame()
    {

    }
}
