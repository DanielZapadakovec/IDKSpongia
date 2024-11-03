using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject InGameMenuPanel;
    public PlayerController playerController;
    void Start()
    {
        
    }

 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InGameMenuPanel.SetActive(true);
        }

        if (InGameMenuPanel.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            playerController.enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            playerController.enabled = true;
        }
    }
    public void BackInGameMenuButton()
    {
        InGameMenuPanel.SetActive(false);
    }
    public void BackToMenuIngameButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
