using System.Collections;
using UnityEngine;

public class GravityBody : MonoBehaviour
{
    public GravityAttractor attractor;
    private Transform myTransform;
    private bool gravityEnabled = true; // Flag pre zapnutie/vypnutie gravit�cie

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false;
        myTransform = transform;
    }

    void Update()
    {
        if (gravityEnabled)
        {
            attractor.Attract(myTransform); // Atrakcia len ak je gravit�cia povolen�
        }
    }

    public void DisableGravity(float duration)
    {
        StartCoroutine(DisableGravityTemporarily(duration));
    }

    private IEnumerator DisableGravityTemporarily(float duration)
    {
        gravityEnabled = false; // Vypni gravit�ciu
        yield return new WaitForSeconds(duration); // Po�kaj definovan� �as
        gravityEnabled = true; // Znova zapni gravit�ciu
    }
}