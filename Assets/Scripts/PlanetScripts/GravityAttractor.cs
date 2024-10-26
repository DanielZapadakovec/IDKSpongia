using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    public float gravity = -10f; // Z�kladn� sila gravit�cie
    public float maxGravityDistance = 100f; // Maxim�lna vzdialenos�, kde gravit�cia p�sob�
    public float rotationSmoothness = 2f; // Faktor, ktor� ur�uje, ako r�chlo sa rot�cia prisp�sobuje

    public void Attract(Transform body)
    {
        Rigidbody rb = body.GetComponent<Rigidbody>();
        if (rb == null) return;

        // Vypo��taj vzdialenos� medzi telom a gravit�torom
        float distance = Vector3.Distance(body.position, transform.position);

        // Dynamick� gravit�cia, ktor� sa zmen�uje so vzdialenos�ou
        float dynamicGravity = Mathf.Lerp(0, gravity, 1 - (distance / maxGravityDistance));

        // Z�skaj norm�lu ter�nu na poz�cii objektu
        RaycastHit hit;
        if (Physics.Raycast(body.position, -body.up, out hit))
        {
            // Norm�la ter�nu
            Vector3 groundNormal = hit.normal;

            // Aplikuj silu gravit�cie v smere norm�ly ter�nu
            rb.AddForce(groundNormal * dynamicGravity);

            // Zabezpe�i� hladk� rot�ciu objektu smerom k norm�le ter�nu
            Quaternion targetRotation = Quaternion.FromToRotation(body.up, groundNormal) * body.rotation;

            // Pou�i jemnej�ie prisp�sobenie rot�cie
            body.rotation = Quaternion.Slerp(body.rotation, targetRotation, rotationSmoothness * Time.deltaTime);
        }
    }
}