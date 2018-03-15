using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIconPulsate : MonoBehaviour {
	
	private bool _pulse = true, _pulseUp = true;
	private float _pulseTimer;
	private const float Cooldown = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Timer();
		
		if (_pulseUp)
		{
			transform.localScale += (new Vector3(0.5f,0.5f,0) * Time.deltaTime);
		}
		else
		{
			transform.localScale -= (new Vector3(0.5f,0.5f,0) * Time.deltaTime);
		}
	}
	
	private void Timer()
	{
		if (!_pulse)
		{
			_pulseTimer += Time.deltaTime;

			if (_pulseTimer >= Cooldown)
			{
				_pulse = true;
				_pulseTimer = 0;
				if (_pulseUp)
				{
					_pulseUp = false;
				}
				else
				{
					_pulseUp = true;
				}
			}
		}
		else
		{
			_pulse = false; 
		}
	}
}
