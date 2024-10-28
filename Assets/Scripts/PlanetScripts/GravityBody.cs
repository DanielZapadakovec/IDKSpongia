using System.Collections;
using UnityEngine;

public class GravityBody : MonoBehaviour
{
    public GravityAttractor attractor;
    private bool gravityEnabled = true; // Povolenie gravit�cie

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        if (gravityEnabled && attractor != null)
        {
            attractor.Attract(transform); // Atrakcia len ak je gravit�cia povolen�
        }
    }

    public void DisableGravity(float duration)
    {
        StartCoroutine(DisableGravityTemporarily(duration));
    }

    private IEnumerator DisableGravityTemporarily(float duration)
    {
        gravityEnabled = false;  // Do�asne vypni gravit�ciu
        yield return new WaitForSeconds(duration);
        gravityEnabled = true;   // Obnov gravit�ciu
    }
}