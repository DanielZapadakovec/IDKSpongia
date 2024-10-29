using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [Header("Lights Configuration")]
    public List<Light> lights; // Zoznam svietidiel, ktor� chceme upravi�
    public Color targetColor = Color.red; // Cie�ov� farba svetiel
    public float targetIntensity = 0.5f; // Cie�ov� intenzita svetiel
    public float transitionDuration = 2f; // Doba prechodu v sekund�ch

    // Met�da na spustenie prechodu intenzity a farby svetiel
    public void AdjustLights()
    {
        StartCoroutine(AdjustLightsCoroutine());
    }

    private IEnumerator AdjustLightsCoroutine()
    {
        float elapsedTime = 0f;

        // Ulo�enie p�vodnej farby a intenzity pre ka�d� sveteln� zdroj
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
                // Lerp (line�rne prechod) medzi p�vodnou a cie�ovou farbou a intenzitou
                lights[i].color = Color.Lerp(initialColors[i], targetColor, t);
                lights[i].intensity = Mathf.Lerp(initialIntensities[i], targetIntensity, t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Uist�me sa, �e sme nastavili presne cie�ov� farbu a intenzitu po skon�en� prechodu
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].color = targetColor;
            lights[i].intensity = targetIntensity;
        }
    }
}