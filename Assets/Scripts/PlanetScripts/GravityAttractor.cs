using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    public float gravity = -10f; // Z·kladn· sila gravit·cie
    public float maxGravityDistance = 100f; // Maxim·lna vzdialenosù, kde gravit·cia pÙsobÌ
    public float rotationSmoothness = 2f; // Faktor, ktor˝ urËuje, ako r˝chlo sa rot·cia prispÙsobuje

    public void Attract(Transform body)
    {
        Rigidbody rb = body.GetComponent<Rigidbody>();
        if (rb == null) return;

        // VypoËÌtaj vektor gravit·cie (normovan˝ smer k stredu gravit·tora)
        Vector3 gravityUp = (body.position - transform.position).normalized;

        // Vzdialenosù medzi telom a gravit·torom
        float distance = Vector3.Distance(body.position, transform.position);

        // Dynamick· gravit·cia, ktor· sa zmenöuje so vzdialenosùou
        float dynamicGravity = Mathf.Lerp(0, gravity, 1 - (distance / maxGravityDistance));

        // Aplikuj silu gravit·cie
        rb.AddForce(gravityUp * dynamicGravity);

        // ZabezpeËiù hladk˙ rot·ciu objektu smerom k stredu gravit·cie
        Vector3 bodyUp = body.up;
        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;

        // Pouûi jemnejöie prispÙsobenie rot·cie
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, rotationSmoothness * Time.deltaTime);
    }
}