using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]

public class Sound{

    public string SoundName;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float Volume;
    [Range(.1f, 3f)]
    public float Pitch;
	
	[HideInInspector]
    public AudioSource Source;

	public AudioMixerGroup Output;

}
