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

        // Vypo��taj vektor gravit�cie (normovan� smer k stredu gravit�tora)
        Vector3 gravityUp = (body.position - transform.position).normalized;

        // Vzdialenos� medzi telom a gravit�torom
        float distance = Vector3.Distance(body.position, transform.position);

        // Dynamick� gravit�cia, ktor� sa zmen�uje so vzdialenos�ou
        float dynamicGravity = Mathf.Lerp(0, gravity, 1 - (distance / maxGravityDistance));

        // Aplikuj silu gravit�cie
        rb.AddForce(gravityUp * dynamicGravity);

        // Zabezpe�i� hladk� rot�ciu objektu smerom k stredu gravit�cie
        Vector3 bodyUp = body.up;
        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;

        // Pou�i jemnej�ie prisp�sobenie rot�cie
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, rotationSmoothness * Time.deltaTime);
    }
}