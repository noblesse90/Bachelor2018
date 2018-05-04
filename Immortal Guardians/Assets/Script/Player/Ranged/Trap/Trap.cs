using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the behaviour of trap (ranged #3 ability)

public class Trap : MonoBehaviour
{

	[Header("TrapObjects")] 
	[SerializeField] private GameObject _trapOpen;
	[SerializeField] private GameObject _trapClose;
	
	[Header("Collider")] 
	[SerializeField] private GameObject _collider;
	
	// TIMERS
	private float _timer = 0, _duration = 10;


	public GameObject TrapClose
	{
		get { return _trapClose; }
		set { _trapClose = value; }
	}

	public GameObject Collider
	{
		get { return _collider; }
		set { _collider = value; }
	}

	private void Update()
	{
		
		if (_collider.activeInHierarchy)
		{
			var v = _collider.transform.localScale;
			if (v.x <= 5)
			{
				var newScale = new Vector3(v.x += Time.deltaTime * 2, v.y += Time.deltaTime * 2, 0);
				_collider.transform.localScale = newScale;
			}
			
			_timer += Time.deltaTime;

			if (_timer >= _duration)
			{
				Destroy(gameObject);
			}
		}
	}

}
