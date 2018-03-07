using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {


	public void Resume()
	{
		Time.timeScale = 1;
		CameraZoom.Instance.Zoom = true;
		gameObject.SetActive(false);
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Time.timeScale = 1;
	}

	public void Quit()
	{
		Application.Quit();
	}
}
