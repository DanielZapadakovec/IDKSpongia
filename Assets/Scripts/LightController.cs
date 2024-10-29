using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [Header("Lights Configuration")]
    public List<Light> lights; // Zoznam svietidiel, ktorÈ chceme upraviù
    public Color targetColor = Color.red; // Cieæov· farba svetiel
    public float targetIntensity = 0.5f; // Cieæov· intenzita svetiel
    public float transitionDuration = 2f; // Doba prechodu v sekund·ch

    // MetÛda na spustenie prechodu intenzity a farby svetiel
    public void AdjustLights()
    {
        StartCoroutine(AdjustLightsCoroutine());
    }

    private IEnumerator AdjustLightsCoroutine()
    {
        float elapsedTime = 0f;

        // Uloûenie pÙvodnej farby a intenzity pre kaûd˝ sveteln˝ zdroj
        List<Color> initialColors = new List<Color>();
        List<float> initialIntensities = new List<float>();
        foreach (Light light in lights)
        {
            initialColors.Add(light.color);
            initialIntensities.Add(light.intensity);
        }

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            for (int i = 0; i < lights.Count; i++)
            {
                // Lerp (line·rne prechod) medzi pÙvodnou a cieæovou farbou a intenzitou
                lights[i].color = Color.Lerp(initialColors[i], targetColor, t);
                lights[i].intensity = Mathf.Lerp(initialIntensities[i], targetIntensity, t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // UistÌme sa, ûe sme nastavili presne cieæov˙ farbu a intenzitu po skonËenÌ prechodu
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].color = targetColor;
            lights[i].intensity = targetIntensity;
        }
    }
}