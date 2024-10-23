using System.Collections;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material planetSkybox;  // Skybox pre planÈtu
    public Material spaceSkybox;   // Skybox pre vesmÌr
    public bool inSpace = true;

    public float transitionDuration = 2f; // DÂûka prechodu v sekund·ch

    private bool isTransitioning = false;

    // Vstup do triggeru - Zmena skyboxu na vesmÌr
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTransitioning) // Ak objekt je hr·Ë a neprech·dzame
        {
            if (inSpace) // Ak sme vo vesmÌre
            {
                StartCoroutine(ChangeSkyboxSmoothly(planetSkybox, spaceSkybox, false));
                inSpace = false;
            }
        }
    }

    // V˝stup z triggeru - Zmena skyboxu sp‰ù na planÈtu
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isTransitioning) // Ak objekt je hr·Ë a neprech·dzame
        {
            if (!inSpace) // Ak sme na planÈte
            {
                StartCoroutine(ChangeSkyboxSmoothly(spaceSkybox, planetSkybox, true));
                inSpace = true;
            }
        }
    }

    // Funkcia na hladk˝ prechod medzi skyboxmi
    private IEnumerator ChangeSkyboxSmoothly(Material newSkybox, Material oldSkybox, bool toSpace)
    {
        isTransitioning = true;
        float elapsedTime = 0f;

        // NastavÌme nov˝ skybox
        RenderSettings.skybox = newSkybox;

        // ZÌskame poËiatoËn˙ a cieæov˙ expozÌciu
        float startExposure = toSpace ? 1f : 0f;
        float endExposure = toSpace ? 0f : 1f;

        // Postupn· interpol·cia hodnoty Exposure
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / transitionDuration);
            float currentExposure = Mathf.Lerp(startExposure, endExposure, t);

            // Nastavenie expozÌcie pre hladk˝ prechod
            RenderSettings.skybox.SetFloat("_Exposure", currentExposure);

            yield return null; // PoËkame na ÔalöÌ frame
        }

        isTransitioning = false;
        DynamicGI.UpdateEnvironment(); // Aktualiz·cia glob·lneho osvetlenia po zmene
    }
}