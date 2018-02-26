using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	private Animator _myAnimator;

	// Use this for initialization
	void Start ()
	{
		_myAnimator = GetComponent<Animator>();
	}
	
}
