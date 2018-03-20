/*
using UnityEngine;
using Cinemachine;

public class FollowVcam : MonoBehaviour
{

	[SerializeField] private CinemachineVirtualCamera _vcam;
	private GameObject _ranged;
	private GameObject _melee;

	// Use this for initialization
	private void Start ()
	{
		_ranged = transform.GetChild(0).gameObject;
		_melee = transform.GetChild(1).gameObject;
	}
	
	// Update is called once per frame
	private void LateUpdate ()
	{
		if (_vcam.Follow != null) return;
		if (_ranged.activeInHierarchy)
		{
			_vcam.Follow = _ranged.transform;
		} 
		else if (_melee.activeInHierarchy)
		{
			_vcam.Follow = _melee.transform;
		}
	}
}
*/