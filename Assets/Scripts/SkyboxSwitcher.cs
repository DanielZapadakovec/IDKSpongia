using UnityEngine;

public class SkyboxSwitcher : MonoBehaviour
{
    public Material skybox1;
    public Material skybox2;

    private bool isSkybox1Active = true;

    public void ToggleSkybox()
    {
        // Prepne na druh� skybox
        if (isSkybox1Active)
        {
            RenderSettings.skybox = skybox2;
        }
        else
        {
            RenderSettings.skybox = skybox1;
        }

        // Preklop� stav skyboxu
        isSkybox1Active = !isSkybox1Active;
    }
}