using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the behaviour of enemies that enters the trap area (ranged #3 ability)

public class AreaCollider : MonoBehaviour {

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			other.GetComponent<EnemyController>().SetSpeed(2.5f);
		}
	}
}
