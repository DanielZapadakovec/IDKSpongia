using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterSeconds : MonoBehaviour
{
    public float delayInSeconds = 3f;
    public GameObject thisObject;
    public GameObject otherObject;

    void Start()
    {
        thisObject = this.gameObject;
        Invoke("DisableGameObject", delayInSeconds);
    }

    void DisableGameObject()
    {
        thisObject.SetActive(false);
        otherObject.SetActive(true);
    }
}
