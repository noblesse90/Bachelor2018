using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollider : Singleton<PlayerAttackCollider> {
	
	private List<GameObject> _targets = new List<GameObject>();

	public List<GameObject> Targets
	{
		get { return _targets; }
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.gameObject.CompareTag("Enemy"))
		{
			_targets.Add(other.gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			_targets.Remove(other.gameObject);
		}
	}
}
