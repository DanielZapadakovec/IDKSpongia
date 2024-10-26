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

        // VypoËÌtaj vzdialenosù medzi telom a gravit·torom
        float distance = Vector3.Distance(body.position, transform.position);

        // Dynamick· gravit·cia, ktor· sa zmenöuje so vzdialenosùou
        float dynamicGravity = Mathf.Lerp(0, gravity, 1 - (distance / maxGravityDistance));

        // ZÌskaj norm·lu terÈnu na pozÌcii objektu
        RaycastHit hit;
        if (Physics.Raycast(body.position, -body.up, out hit))
        {
            // Norm·la terÈnu
            Vector3 groundNormal = hit.normal;

            // Aplikuj silu gravit·cie v smere norm·ly terÈnu
            rb.AddForce(groundNormal * dynamicGravity);

            // ZabezpeËiù hladk˙ rot·ciu objektu smerom k norm·le terÈnu
            Quaternion targetRotation = Quaternion.FromToRotation(body.up, groundNormal) * body.rotation;

            // Pouûi jemnejöie prispÙsobenie rot·cie
            body.rotation = Quaternion.Slerp(body.rotation, targetRotation, rotationSmoothness * Time.deltaTime);
        }
    }
}