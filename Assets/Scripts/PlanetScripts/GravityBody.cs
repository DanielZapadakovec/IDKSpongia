using System.Collections;
using UnityEngine;

public class GravityBody : MonoBehaviour
{
    public GravityAttractor attractor;
    private bool gravityEnabled = true; // Povolenie gravitácie

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
            attractor.Attract(transform); // Atrakcia len ak je gravitácia povolená
        }
    }

    public void DisableGravity(float duration)
    {
        StartCoroutine(DisableGravityTemporarily(duration));
    }

    private IEnumerator DisableGravityTemporarily(float duration)
    {
        gravityEnabled = false;  // Doèasne vypni gravitáciu
        yield return new WaitForSeconds(duration);
        gravityEnabled = true;   // Obnov gravitáciu
    }
}