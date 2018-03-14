using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

	[SerializeField] private TMP_Dropdown _resolutionDropdown;

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


	public void SetResolution(int resolutionIndex)
	{
		Resolution resolution = _resolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}

	public void SetFullscreen(bool fullscreen)
	{
		Screen.fullScreen = fullscreen;
	}
}
