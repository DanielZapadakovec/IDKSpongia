using UnityEngine;

public class AudioSwitcher : MonoBehaviour
{
    public GameObject targetObject;
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;

    void Update()
    {
        if (targetObject.activeInHierarchy)
        {
            if (audioSource1.isPlaying)
            {
                audioSource1.Stop();
                audioSource3.Stop();
            }

            
            if (!audioSource2.isPlaying)
            {
                audioSource2.Play();
            }
        }
    }
}