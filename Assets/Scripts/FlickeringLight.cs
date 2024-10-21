using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light lightSource;
    public GameObject linkedGameObject;

    public float minTime = 0.1f;
    public float maxTime = 0.5f;

    private bool isFlickering = false;

    void Start()
    {
        StartCoroutine(FlickerLight());
    }

    IEnumerator FlickerLight()
    {
        while (true)
        {
            bool isLightOn = !lightSource.enabled;
            lightSource.enabled = isLightOn;
            linkedGameObject.SetActive(isLightOn);
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
