using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    public Image healthBar;
    public float healthAmount = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if (healthAmount < 0)
        {

        }
    }

    public void TakeDamage()
    {
        healthAmount -= 25f;
        healthBar.fillAmount = healthAmount / 100f;
        Debug.Log("Odoberá HP od hráèa");
    }

    public void Heal()
    {
        healthAmount += 25;
        healthAmount = Mathf.Clamp(25, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
        Debug.Log("Heali hráèa");
    }
}
