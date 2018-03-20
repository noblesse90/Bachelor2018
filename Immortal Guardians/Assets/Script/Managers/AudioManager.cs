using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : Singleton<AudioManager> {

	public Sound[] _sounds;

    private void Awake()
    {
        foreach(Sound s in _sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.clip;

            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;

            s.Source.outputAudioMixerGroup = s.Output;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(_sounds, Sound => Sound.SoundName == name);
        s.Source.Play();
    }

}
