﻿using System.Collections;
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
	
	// Update is called once per frame
	void Update () {
		if (gameObject.activeInHierarchy)
		{
			_myAnimator.SetTrigger("Impact");
		}
	}
}
