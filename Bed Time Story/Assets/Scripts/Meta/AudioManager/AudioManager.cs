﻿using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;
    public Sound[] sounds;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.initVolume;
            s.source.pitch = s.initPitch;
        }
    }

    public void Play(string soundName)
    {
        Sound s = FindSound(soundName);
        s.source.loop = s.loop;
        s.source.Play();
    }

    public void Play(string soundName, float pitchVar)
    {
        Sound s = FindSound(soundName);
        s.source.pitch = s.initPitch + UnityEngine.Random.Range(-pitchVar, pitchVar);
        s.source.loop = s.loop;
        s.source.Play();
    }

    public void FadeVolume(string soundName, float targetValue)
    {
        Sound s = FindSound(soundName);
        StartCoroutine(AdjustVolume(s, s.initVolume, targetValue, 1.5f));
    }

    public void FadeVolume(string soundName, float startValue, float targetValue)
    {
        Sound s = FindSound(soundName);
        StartCoroutine(AdjustVolume(s, startValue, targetValue, 1.5f));
    }

    public void FadeVolume(string soundName, float startValue, float targetValue, float fadeDuration)
    {
        Sound s = FindSound(soundName);
        StartCoroutine(AdjustVolume(s, startValue, targetValue, fadeDuration));
    }

    private IEnumerator AdjustVolume(Sound s, float startValue, float targetValue, float fadeDuration, Action onCompleteAction = null)
    {
        float delta = targetValue - startValue;
        float volume = startValue;

        while(Mathf.Abs(volume - targetValue) > 0.02f)
        {
            volume += (0.01f / fadeDuration) * delta;
            s.source.volume = volume;
            
            yield return new WaitForSeconds(0.01f);
        }

        onCompleteAction?.Invoke();
    }

    public void Stop(string soundName)
    {
        Sound s = FindSound(soundName);
        s.source.Stop();
    }

    public void PlayPitched(string soundName, float pitch)
    {
        Sound s = FindSound(soundName);
        s.source.pitch = pitch;
        s.source.loop = s.loop;
        s.source.Play();
    }

    public void PlayPitched(string soundName, float pitch, float pitchVar)
    {
        Sound s = FindSound(soundName);
        s.source.pitch = pitch + UnityEngine.Random.Range(-pitchVar, pitchVar);
        s.source.loop = s.loop;
        s.source.Play();
    }

    internal static AudioManager GetInstance()
    {
        return instance;
    }

    private Sound FindSound(string soundName)
    {
        return Array.Find(sounds, sound => sound.name == soundName);
    }

    public void TogglePause(string soundName, bool doPause)
    {
        Sound s = FindSound(soundName);
        s.source.volume = doPause ? 0.1f : s.initVolume;
        // StartCoroutine(doPause
        //     ? AdjustVolume(s, s.source.volume, 0.1f, 0.5f, s.source.Pause)
        //     : AdjustVolume(s, 0.1f, s.initVolume, 0.5f, s.source.UnPause));
    }
}