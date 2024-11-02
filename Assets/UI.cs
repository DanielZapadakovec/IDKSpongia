using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject InGameMenuPanel;
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
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void BackInGameMenuButton()
    {
        InGameMenuPanel.SetActive(false);
    }
}
