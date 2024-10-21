using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceStation : MonoBehaviour
{

    public GameObject RedToGreenDoors;
    public GameObject RedWall;

    public GameObject ReactorGreen;
    public GameObject ReactorRed;

    public bool ReactorGreen1;
    public void Doors()
    {
        if (ReactorGreen1 == true)
        {
            RedToGreenDoors.SetActive(true);
            RedWall.SetActive(false);
            StarshipOut();
        }
    }
    public void Reactor()
    {
        ReactorGreen.SetActive(true);
        ReactorRed.SetActive(false);
        ReactorGreen1 = true;
    }
    public void StarshipOut()
    {
        SceneManager.LoadScene("Space");
    }
}
