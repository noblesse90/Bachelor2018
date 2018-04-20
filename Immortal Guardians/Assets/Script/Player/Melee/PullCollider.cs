using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullCollider : MonoBehaviour
{
	private List<GameObject> _enemies = new List<GameObject>();

	public List<GameObject> Enemies
	{
		get { return _enemies; }
		set { _enemies = value; }
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			_enemies.Add(other.gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			_enemies.Remove(other.gameObject);
		}
	}
}
