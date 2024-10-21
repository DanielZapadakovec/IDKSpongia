using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    public AudioSource In, Out;
    public AudioClip audioClipIn;

    public float defaultVolume = 0.3f;
    float transitionTime = 0.1f;

    private bool isInTrigger = false;


     void Start()
    {
        audioClipIn.LoadAudioData();
        In.clip = audioClipIn;
    }
    // Method to change the music clip
    public void ChangeClip()
    {
        AudioSource nowPlaying = Out.isPlaying ? Out : In;
        AudioSource target = nowPlaying == Out ? In : Out;

        StartCoroutine(MixSources(nowPlaying, target));
    }

    IEnumerator MixSources(AudioSource nowPlaying, AudioSource target)
    {
        float percentage = 0;
        while (nowPlaying.volume > 0)
        {
            nowPlaying.volume = Mathf.Lerp(defaultVolume, 0, percentage);
            percentage += Time.deltaTime / transitionTime;
            yield return null;
        }

        nowPlaying.Pause();

        if (!target.isPlaying)
        {
            target.Play();
        }

        target.UnPause();

        percentage = 0;
        while (target.volume < defaultVolume)
        {
            target.volume = Mathf.Lerp(0, defaultVolume, percentage);
            percentage += Time.deltaTime / transitionTime;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isInTrigger)
        {
            isInTrigger = true;
            ChangeClip();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isInTrigger)
        {
            isInTrigger = false;
            ChangeClip();
        }
    }
}