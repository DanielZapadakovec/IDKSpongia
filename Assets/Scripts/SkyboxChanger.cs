using System.Collections;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material planetSkybox;  // Skybox pre plan�tu
    public Material spaceSkybox;   // Skybox pre vesm�r
    public bool inSpace = true;

    public float transitionDuration = 2f; // D�ka prechodu v sekund�ch

    private bool isTransitioning = false;

    // Vstup do triggeru - Zmena skyboxu na vesm�r
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTransitioning) // Ak objekt je hr�� a neprech�dzame
        {
            if (inSpace) // Ak sme vo vesm�re
            {
                StartCoroutine(ChangeSkyboxSmoothly(planetSkybox, spaceSkybox, false));
                inSpace = false;
            }
        }
    }

    // V�stup z triggeru - Zmena skyboxu sp� na plan�tu
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isTransitioning) // Ak objekt je hr�� a neprech�dzame
        {
            if (!inSpace) // Ak sme na plan�te
            {
                StartCoroutine(ChangeSkyboxSmoothly(spaceSkybox, planetSkybox, true));
                inSpace = true;
            }
        }
    }

    // Funkcia na hladk� prechod medzi skyboxmi
    private IEnumerator ChangeSkyboxSmoothly(Material newSkybox, Material oldSkybox, bool toSpace)
    {
        isTransitioning = true;
        float elapsedTime = 0f;

        // Nastav�me nov� skybox
        RenderSettings.skybox = newSkybox;

        // Z�skame po�iato�n� a cie�ov� expoz�ciu
        float startExposure = toSpace ? 1f : 0f;
        float endExposure = toSpace ? 0f : 1f;

        // Postupn� interpol�cia hodnoty Exposure
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / transitionDuration);
            float currentExposure = Mathf.Lerp(startExposure, endExposure, t);

            // Nastavenie expoz�cie pre hladk� prechod
            RenderSettings.skybox.SetFloat("_Exposure", currentExposure);

            yield return null; // Po�kame na �al�� frame
        }

        isTransitioning = false;
        DynamicGI.UpdateEnvironment(); // Aktualiz�cia glob�lneho osvetlenia po zmene
    }
}