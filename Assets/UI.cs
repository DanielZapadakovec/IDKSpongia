using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }
    public void BackInGameMenuButton()
    {
        InGameMenuPanel.SetActive(false);
    }
}
