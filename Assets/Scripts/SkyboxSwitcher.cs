using UnityEngine;

public class SkyboxSwitcher : MonoBehaviour
{
    public Material skybox1;
    public Material skybox2;

    private bool isSkybox1Active = true;

    public void ToggleSkybox()
    {
        // Prepne na druhý skybox
        if (isSkybox1Active)
        {
            RenderSettings.skybox = skybox2;
        }
        else
        {
            RenderSettings.skybox = skybox1;
        }

        // Preklopí stav skyboxu
        isSkybox1Active = !isSkybox1Active;
    }
}