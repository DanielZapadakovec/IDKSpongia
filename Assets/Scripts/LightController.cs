using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [Header("Lights Configuration")]
    public List<Light> lights;
    public Color targetColor = Color.red;
    public float targetIntensity = 0.5f;
    public float transitionDuration = 2f;

    public void AdjustLights()
    {
        StartCoroutine(AdjustLightsCoroutine());
    }

    private IEnumerator AdjustLightsCoroutine()
    {
        float elapsedTime = 0f;

        // Uloženie pôvodnej farby a intenzity pre každý svetelný zdroj
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
                // Lerp (lineárne prechod) medzi pôvodnou a cie¾ovou farbou a intenzitou
                lights[i].color = Color.Lerp(initialColors[i], targetColor, t);
                lights[i].intensity = Mathf.Lerp(initialIntensities[i], targetIntensity, t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Uistíme sa, že sme nastavili presne cie¾ovú farbu a intenzitu po skonèení prechodu
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].color = targetColor;
            lights[i].intensity = targetIntensity;
        }
    }
}