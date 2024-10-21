using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    public ShipController shipController;

    [Header("Particles")]
    private float minValue = 0f;
    private float maxValue = 32f;
    public ParticleSystem motor1;
    public ParticleSystem motor2;
    [Header("Speeds")]
    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    public  float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration;

    public float lookRateSpeed = 90f;
    private Vector2 LookInput, screenCenter, mouseDistance;

    private float rollInput;
    public float rollSpeed = 90f, rollAcceleration = 3.5f;

    public GameObject wingsParticles;

    [Header("Sounds")]
    public AudioSource shipAudioSource;
    public float minPitch = 0.5f;
    public float maxPitch = 2f;

    [Header("Energy")]
    public float maxEnergy = 100.0f;
    public float currentEnergy = 50.0f;
    public Image energyImage;
    public Text energyText;
    private float energyTimer = 0f; 
    public float energySubtractionRate = 1f;
    public float energySubtractionAmount = 1f;
    public bool canMove;

    [Header("Power")]
    public float maxPower = 45.0f;
    public float currentPower = 0.0f;
    public Image powerImage;
    public Text powerText;

    [Header("Objectives")]
    public Text objectiveText;

    [Header("Tablet")]
    public GameObject tablet;
    void Start()
    {
        shipController = GetComponent<ShipController>();
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        energyImage.fillAmount = 0.5f;
        powerImage.fillAmount = 0f;
        UpdateEnergyText();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnergy <= 0f) 
        {
            canMove = false;

        }
        if (currentEnergy >= 1f)
        {
            canMove = true;
        }
        if (currentEnergy >= 60f)
        {
            objectiveText.text = "land on habitable planet";
            tablet.SetActive(true);
            Cursor.visible = true;
            shipController.enabled = false;
        }

        energyTimer += Time.deltaTime;

        if (energyTimer >= energySubtractionRate)
        {
            ConsumeEnergy(energySubtractionAmount);

            energyTimer = 0f;
        }
        energyImage.fillAmount = currentEnergy / maxEnergy;
        UpdateEnergyText();



        float originalValue = activeForwardSpeed;

        float normalizedValue = Mathf.InverseLerp(minValue, maxValue, originalValue);
        currentPower = originalValue;
        powerImage.fillAmount = normalizedValue;
        UpdatePowerText();
        var mainModule = motor1.main;
        mainModule.startLifetime = new ParticleSystem.MinMaxCurve(normalizedValue);
        var mainModule2 = motor2.main;
        mainModule2.startLifetime = new ParticleSystem.MinMaxCurve(normalizedValue);

        float targetPitch = Mathf.Lerp(minPitch, maxPitch, normalizedValue);

        shipAudioSource.pitch = targetPitch;

        LookInput.x = Input.mousePosition.x;
        LookInput.y = Input.mousePosition.y;

        mouseDistance.x = (LookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (LookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);            
        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += (transform.right * activeStrafeSpeed * Time.deltaTime) + (transform.up * activeHoverSpeed * Time.deltaTime);
        if (canMove)
        {

            rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);
            transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);
            activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration * Time.deltaTime);
            activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);

            activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);
        }

        if(Input.GetButtonDown("Boost"))
        {
            forwardSpeed = 32f;
            energySubtractionAmount = 2;
            wingsParticles.SetActive(true);
        }
        else if (Input.GetButtonUp("Boost"))
        {
            forwardSpeed = 25f;
            energySubtractionAmount = 1;
            wingsParticles.SetActive(false);
        }

    }


    void UpdateEnergyText()
    {
        energyText.text = Mathf.RoundToInt(currentEnergy).ToString() + ".0 %";
    }
    void UpdatePowerText()
    {
        powerText.text = Mathf.RoundToInt(currentPower * 3.11f).ToString() + ".0 %";
    }

    void ConsumeEnergy(float amount)
    {
        currentEnergy = Mathf.Clamp(currentEnergy - amount, 0f, maxEnergy);
    }

    void AddEnergy(float amount)
    {
        currentEnergy = Mathf.Clamp(currentEnergy + amount, 0f, maxEnergy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Energy"))
        {
            Destroy(other.gameObject);
            AddEnergy(5f);
        }
    }

    public void TabletTrigger()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
    }
    public void TabletUntrigger()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
    }
}
