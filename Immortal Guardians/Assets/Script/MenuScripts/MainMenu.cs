using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

	[SerializeField] private GameObject _ranged;
	[SerializeField] private GameObject _melee;
	private bool _rangedBool;
	private bool _meleeBool;

	public void Next()
	{
		if (_rangedBool)
		{
			_ranged.SetActive(true);
			_ranged.GetComponent<PlayerController>().GetClass = PlayerController.Class.Ranged;
		}
		else if (_meleeBool)
		{
			_melee.SetActive(true);
			_melee.GetComponent<PlayerController>().GetClass = PlayerController.Class.Melee;
		}

		GManager.Instance.GameStarted = true;
		CameraZoom.Instance.Zoom = true;
	}

	public void ChooseRanged()
	{
		_rangedBool = true;
		_meleeBool = false;
	}

	public void ChooseMelee()
	{
		_rangedBool = false;
		_meleeBool = true;
	}
	
	public void Quit()
	{
		Application.Quit();
	}
}
