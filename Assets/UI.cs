using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject inGameMenuPanel;
    public PlayerController playerController;
    public GameObject TerminalPanel;
    public GameObject TitulkyPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inGameMenuPanel.SetActive(true);
        }

        if (inGameMenuPanel.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;

            playerController.enabled = false;

            Time.timeScale = 0;
        }
        else if (TerminalPanel.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if (TitulkyPanel.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
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
        inGameMenuPanel.SetActive(false);
    }

    public void BackToMenuIngameButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}