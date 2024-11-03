using UnityEngine;

public class AudioTurn : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject audioParticle;
    public ObjectiveManager objectiveManager;
     bool hasCompletedObjective;
    public bool isTV;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ToggleRadio()
    {
        if (!hasCompletedObjective && !isTV)
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