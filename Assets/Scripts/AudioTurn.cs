using UnityEngine;

public class AudioTurn : MonoBehaviour
{
    private AudioSource audioSource;
    public GameObject audioParticle;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ToggleRadio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            audioParticle.SetActive(false);
        }
        else
        {
            audioSource.Play();
            audioParticle.SetActive(true);
        }
    }
}