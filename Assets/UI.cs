using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject inGameMenuPanel;
    public PlayerController playerController;
    public bool terminalInGame;
    private bool paused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        paused = !paused;

        if (paused)
        {
            inGameMenuPanel.SetActive(true);
            Time.timeScale = 0;

            if (!terminalInGame)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                playerController.enabled = false;
            }
        }
        else
        {
            inGameMenuPanel.SetActive(false);
            Time.timeScale = 1;

            if (!terminalInGame)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                playerController.enabled = true;
            }
        }
    }

    public void BackInGameMenuButton()
    {
        inGameMenuPanel.SetActive(false);
        paused = false;
        Time.timeScale = 1;

        if (!terminalInGame)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerController.enabled = true;
        }
    }

    public void BackToMenuIngameButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}