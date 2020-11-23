using System;
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
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.loop = s.loop;
        s.source.Play();
    }

    public void Play(string name, float pitchVar)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = s.pitch + UnityEngine.Random.Range(-pitchVar, pitchVar);
        s.source.loop = s.loop;
        s.source.Play();
    }

    public void FadeVolume(string soundName, float targetValue)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        StartCoroutine(AdjustVolume(s, s.volume, targetValue, 1.5f));
    }

    public void FadeVolume(string soundName, float startValue, float targetValue)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        StartCoroutine(AdjustVolume(s, startValue, targetValue, 1.5f));
    }

    public void FadeVolume(string soundName, float startValue, float targetValue, float fadeDuration)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        StartCoroutine(AdjustVolume(s, startValue, targetValue, fadeDuration));
    }

    private IEnumerator AdjustVolume(Sound s, float startValue, float targetValue, float fadeDuration)
    {
        float delta = targetValue - startValue;
        float volume = startValue;

        while(Mathf.Abs(volume - targetValue) > 0.02f)
        {
            volume += (0.01f / fadeDuration) * delta;
            s.source.volume = volume;
            
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void PlayPitched(string name, float pitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = pitch;
        s.source.loop = s.loop;
        s.source.Play();
    }

    public void PlayPitched(string name, float pitch, float pitchVar)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = pitch + UnityEngine.Random.Range(-pitchVar, pitchVar);
        s.source.loop = s.loop;
        s.source.Play();
    }

    internal static AudioManager GetInstance()
    {
        return instance;
    }
}