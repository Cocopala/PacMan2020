using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
public class SoundMgr : MonoBehaviour
{

    public Sound[] sounds;
    public AudioSource audioSource;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        Debug.Log("play music:" + s.name);
        if (s != null && s.source != null)
            s.source.Play();
    }

    public void PlayOnce(string soundName)
    {

        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        Debug.Log("play music once:" + s.name);
        audioSource.PlayOneShot(s.source.clip);
    }

    void Start()
    {
        //string theme = "Normal";
        //Play(theme);
    }

    public void StopPlay(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        Debug.Log("stop play music:" + s.name);
        if (s != null && s.source != null)
            s.source.Stop();

    }
}
