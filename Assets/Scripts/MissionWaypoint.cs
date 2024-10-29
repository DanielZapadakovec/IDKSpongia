using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MissionWaypoint : MonoBehaviour
{
    public GameObject waypointPrefab; // Prefab waypointu (ikona, text)
    public Canvas canvas; // Canvas, kde sa waypoint zobrazí
    public Transform playerTransform; // Referencia na hráèa
    public Vector3 offset; // Offset waypointu
    public Camera mainCamera;
    [Header("Properties")]
    public string objectName;
    public bool isTimed;

    private GameObject currentWaypointInstance;
    private Image img;
    private Text meter;
    private Text nameText;

    private bool isSpawned;
    private float fadeDuration = 1.5f; // Dåžka fade-out efektu v sekundách

    private void Update()
    {
        // Inštanciovanie waypointu, ak ešte neexistuje
        if (!isSpawned)
        {
            currentWaypointInstance = Instantiate(waypointPrefab, canvas.transform);
            img = currentWaypointInstance.GetComponentInChildren<Image>();
            meter = currentWaypointInstance.transform.Find("Meter").GetComponent<Text>();
            nameText = currentWaypointInstance.transform.Find("Name").GetComponent<Text>();
            nameText.text = objectName;
            isSpawned = true;
            if (isTimed)
            {
                StartCoroutine(FadeOutAndDestroyAfterDelay(11f));
            }
            else { return; }
        }

        if (currentWaypointInstance)
        {
            UpdateWaypointPositionAndDistance();
        }
    }

    private void UpdateWaypointPositionAndDistance()
    {
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = mainCamera.WorldToScreenPoint(transform.position + offset);

        if (Vector3.Dot((transform.position - playerTransform.position), mainCamera.transform.forward) < 0)
        {
            pos.x = pos.x < Screen.width / 2 ? maxX : minX;
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        meter.text = $"{Mathf.RoundToInt(distance)} m";
    }

    private IEnumerator FadeOutAndDestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay - fadeDuration); // Èas, kedy zaène fade-out

        // Fade-out efekt
        float elapsedTime = 0;
        Color initialColor = img.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);

            SetAlphaForWaypoint(alpha);
            yield return null;
        }

        Destroy(currentWaypointInstance);
        isSpawned = false;
        this.enabled = false;
    }

    // Metóda na nastavenie alfa pre všetky komponenty waypointu
    private void SetAlphaForWaypoint(float alpha)
    {
        img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
        meter.color = new Color(meter.color.r, meter.color.g, meter.color.b, alpha);
        nameText.color = new Color(nameText.color.r, nameText.color.g, nameText.color.b, alpha);
    }
}