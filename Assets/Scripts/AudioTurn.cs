using UnityEngine;

public class AudioTurn : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject audioParticle;
    public ObjectiveManager objectiveManager;
     bool hasCompletedObjective;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ToggleRadio()
    {
        if (!hasCompletedObjective) 
        {
            objectiveManager.CompleteObjective(); 
            hasCompletedObjective = true;   
        }
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