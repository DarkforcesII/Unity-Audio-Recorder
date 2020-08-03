﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Static instance;
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    instance = new GameObject("Spawn AudioManager", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }

            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    internal void PlayMusic(string v, int v1)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Fields
    private AudioSource musicSource;
    private AudioSource musicSource2;
    // two AudioSources created so that smooth transition between music can occur
    private AudioSource sfxSource;

    private bool firstMusicSourceIsPlaying;
    #endregion

    private void Awake()
    {
        // Make sure we don't destroy this instance
        DontDestroyOnLoad(this.gameObject);

        // Create audio sources and save them as references
        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource2 = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();

       
        // Loop music tracks
        musicSource.loop = true;
        musicSource2.loop = true;

    }

    public void PlayMusic(AudioClip musicClip)
    {
        // Determine which source is active
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;

        activeSource.clip = musicClip;
        activeSource.volume = 1;
        activeSource.Play();
    }
    public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1.0f)
    {
        // Determine which audio source is active
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;
        StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));
    }
    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1.0f)
    {
        // Determine which audio source is active
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;
        AudioSource newSource = (firstMusicSourceIsPlaying) ? musicSource2 : musicSource;

        // Swap the source
        firstMusicSourceIsPlaying = !firstMusicSourceIsPlaying;

        // Set the fields of the audio source, then start the coroutine to crossfade
        newSource.clip = musicClip;
        newSource.Play();
        StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));
    }

    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        // Make sure the source is active and playing 
        if (!activeSource.isActiveAndEnabled)
            activeSource.Play();

        float t = 0.0f;

        // Fade out
        for (t = 0;t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = ((t / transitionTime));
            yield return null;
        }

        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();
    }
    private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transitionTime)
    {
        float t = 0.0f;

        // Fade out
        for (t = 0; t <= transitionTime; t += Time.deltaTime)
        {
            original.volume = (1 - (t / transitionTime));
            newSource.volume = ((t / transitionTime));
            yield return null;
        }

        original.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void PlaySFX(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);

    }

    public void SetMusicVolume(float Volume)
    {
        musicSource.volume = Volume;
        musicSource2.volume = Volume;
    }
    public void SetSFXVolume(float Volume)
    {
        sfxSource.volume = Volume;
    }
}

