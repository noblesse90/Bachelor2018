using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;



public class SettingsMenu : MonoBehaviour
{
	[Header("Resolution Dropdown Menu")]
	[SerializeField] private TMP_Dropdown _resolutionDropdown;

	[Header("Audio Mixer")] 
	[SerializeField] private AudioMixer _audioMixer;

	[Header("Sliders")] 
	[SerializeField] private Slider _masterSlider;
	[SerializeField] private Slider _musicSlider;
	[SerializeField] private Slider _effectsSlider;

	private Resolution[] _resolutions;

	// Use this for initialization
	private void Start()
	{
		_resolutions = Screen.resolutions;
		
		_resolutionDropdown.ClearOptions();
		
		List<string> options = new List<string>();

		int currentResolutionIndex = 0;
		for (int i = 0; i < _resolutions.Length; i++)
		{
			string option = _resolutions[i].width + "x" + _resolutions[i].height + "-" + _resolutions[i].refreshRate + "hz";
			options.Add(option);

			if (_resolutions[i].width == Screen.currentResolution.width &&
			    _resolutions[i].height == Screen.currentResolution.height &&
			    _resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
			{
				currentResolutionIndex = i;
			}
		}
		
		_resolutionDropdown.AddOptions(options);
		_resolutionDropdown.value = currentResolutionIndex;
		SetFullscreen(true);
	}

	private void LateUpdate()
	{
		Cursor.lockState = CursorLockMode.Confined;
		
		
	}


	public void SetResolution(int resolutionIndex)
	{
		Resolution resolution = _resolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}


	public void SetFullscreen(bool fullscreen)
	{
		Screen.fullScreen = fullscreen;
	}

	public void SetMasterVolume(float volume)
	{
		_audioMixer.SetFloat("volume", volume);
	}

	public void SetMusicVolume(float volume)
	{
		_audioMixer.SetFloat("musicVolume", volume);
	}

	public void SetEffectsVolume(float volume)
	{
		_audioMixer.SetFloat("effectsVolume", volume);
	}

	public void SetSliders()
	{
		float value;
		bool result = _audioMixer.GetFloat("volume", out value);
		if (result)
		{
			_masterSlider.value = value;
		}
		
		result = _audioMixer.GetFloat("musicVolume", out value);
		if (result)
		{
			_musicSlider.value = value;
		}
		
		result = _audioMixer.GetFloat("effectsVolume", out value);
		if (result)
		{
			_effectsSlider.value = value;
		}
	}
}
