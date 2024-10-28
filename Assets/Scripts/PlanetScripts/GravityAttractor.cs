using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    public float gravity = -10f;  // Z·kladn· sila gravit·cie
    public float maxGravityDistance = 100f; // Maxim·lna vzdialenosù, kde gravit·cia pÙsobÌ
    public float rotationSmoothness = 2f;   // Faktor rot·cie

    public void Attract(Transform body)
    {
        Rigidbody rb = body.GetComponent<Rigidbody>();
        if (rb == null) return;

        // Zistenie vzdialenosti medzi telom a gravit·torom
        float distance = Vector3.Distance(body.position, transform.position);

        // Dynamick· gravit·cia, ktor· sa zmenöuje so vzdialenosùou
        float dynamicGravity = Mathf.Lerp(0, gravity, 1 - (distance / maxGravityDistance));

        // ZÌskanie norm·ly povrchu na pozÌcii objektu
        RaycastHit hit;
        if (Physics.Raycast(body.position, (transform.position - body.position).normalized, out hit, maxGravityDistance))
        {
            Vector3 groundNormal = hit.normal;

            // Aplikovanie sily gravit·cie v smere norm·ly povrchu
            rb.AddForce(groundNormal * dynamicGravity, ForceMode.Acceleration);

            // Hladk· rot·cia objektu v smere norm·ly
            Quaternion targetRotation = Quaternion.FromToRotation(body.up, groundNormal) * body.rotation;
            body.rotation = Quaternion.Slerp(body.rotation, targetRotation, rotationSmoothness * Time.deltaTime);
        }
    }
}