using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    // Prira� materi�ly skyboxov v editore
    public Material planetSkybox;  // Skybox pre plan�tu
    public Material spaceSkybox;   // Skybox pre vesm�r

    // Tento flag n�m povie, �i sme vo vesm�re alebo na plan�te
    public bool inSpace = true;

    // Vstup do triggeru - Zmena skyboxu na vesm�r
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ak objekt je hr��
        {
            Debug.Log("Zatagom");
            if (inSpace) // Ak nie sme vo vesm�re
            {
                ChangeSkybox(planetSkybox);
                inSpace = false;
                Debug.Log("Za zmenou");
            }
        }
    }

    // V�stup z triggeru - Zmena skyboxu sp� na plan�tu
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Ak objekt je hr��
        {
            if (!inSpace) // Ak sme vo vesm�re
            {
                ChangeSkybox(spaceSkybox);
                inSpace = true;
            }
        }
    }

    // Funkcia na zmenu skyboxu
    private void ChangeSkybox(Material newSkybox)
    {
        RenderSettings.skybox = newSkybox;
        DynamicGI.UpdateEnvironment(); // Aktualiz�cia glob�lneho osvetlenia
    }
}