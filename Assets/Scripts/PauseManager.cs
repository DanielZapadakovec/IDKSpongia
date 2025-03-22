using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject SettingsPanel;
    public GameObject ControlsPanel;
    public GameObject ButtonsPanel;
    private bool isPaused;
    private bool isSettings;
    private bool isControls;
    public PlayerController playerController;
    public Terminal terminal;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isSettings && !isControls)
        {
            Pause();
        }
        else if ( Input.GetKeyDown(KeyCode.Escape) && isSettings && !isControls) 
        {
            Settings();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !isSettings && isControls)
        {
            Controls();
        }
    }

    public void Pause()
    {
        if (!isPaused)
        {
            if (playerController != null && terminal != null && !terminal.isTerminal)
            {
                playerController.enabled = false;
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            isPaused = true;
            PausePanel.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (playerController != null && !terminal.isTerminal)
            {
                playerController.enabled = true;
            }
            Time.timeScale = 1;
            isPaused = false;
            PausePanel.SetActive(false);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Settings()
    {
        if (!isSettings)
        {
            ButtonsPanel.SetActive(false);
            isSettings = true;
            SettingsPanel.SetActive(true);
        }
        else
        {
            ButtonsPanel.SetActive(true);
            isSettings = false;
            SettingsPanel.SetActive(false);
        }

    }
    public void Controls()
    {
        if (!isControls)
        {
            ButtonsPanel.SetActive(false);
            isControls = true;
            ControlsPanel.SetActive(true);
        }
        else
        {
            ButtonsPanel.SetActive(true);
            isControls = false;
            ControlsPanel.SetActive(false);
        }
    }

}