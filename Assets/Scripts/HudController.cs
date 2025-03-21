using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    public static HudController instance;
    [SerializeField] Text interactionText;

    private void Awake()
    {
        instance = this; 
    }

    public void EnableInteractionText(string text)
    {
        interactionText.text = text;
        interactionText.gameObject.SetActive(true);
    }
    public void DisableInteractionText()
    {
        interactionText?.gameObject.SetActive(false);
    }
}
