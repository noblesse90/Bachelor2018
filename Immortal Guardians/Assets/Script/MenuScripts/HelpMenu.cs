using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour
{

	[SerializeField] private GameObject _meleeWindow;
	[SerializeField] private GameObject _rangedWindow;

	public void ClassHelp()
	{
		if (PlayerController.Instance.GetClass == PlayerController.Class.Melee)
		{
			_meleeWindow.SetActive(true);
		}
		else if (PlayerController.Instance.GetClass == PlayerController.Class.Ranged)
		{
			_rangedWindow.SetActive(true);
		}
	}
}
