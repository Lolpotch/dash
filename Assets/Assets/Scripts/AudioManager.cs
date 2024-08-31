using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume * PlayerPrefs.GetFloat("Audio Volume", 1f);
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public Sound GetClip(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if(s == null)
        {
            Debug.LogError("The name doesn't match with any clip!: " + name);
        }
        return s;
    }

    public void SetSoundVolume(float multiplier) 
    {
        multiplier = Mathf.Clamp(multiplier, 0f, 1f);
        PlayerPrefs.SetFloat("Audio Volume", multiplier);

        foreach(Sound s in sounds)
        {
            s.source.volume = s.volume * multiplier;
        }
    }

    public void StartMusic()
    {
        GetClip("Music").Play();
    }

    public void PlayClip(string name)
    {
        GetClip(name).Play();
    }
}
