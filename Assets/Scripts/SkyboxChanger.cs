using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    // PriraÔ materi·ly skyboxov v editore
    public Material planetSkybox;  // Skybox pre planÈtu
    public Material spaceSkybox;   // Skybox pre vesmÌr

    // Tento flag n·m povie, Ëi sme vo vesmÌre alebo na planÈte
    public bool inSpace = true;

    // Vstup do triggeru - Zmena skyboxu na vesmÌr
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ak objekt je hr·Ë
        {
            Debug.Log("Zatagom");
            if (inSpace) // Ak nie sme vo vesmÌre
            {
                ChangeSkybox(planetSkybox);
                inSpace = false;
                Debug.Log("Za zmenou");
            }
        }
    }

    // V˝stup z triggeru - Zmena skyboxu sp‰ù na planÈtu
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Ak objekt je hr·Ë
        {
            if (!inSpace) // Ak sme vo vesmÌre
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
        DynamicGI.UpdateEnvironment(); // Aktualiz·cia glob·lneho osvetlenia
    }
}