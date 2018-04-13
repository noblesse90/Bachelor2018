using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCollider : MonoBehaviour {

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			other.GetComponent<EnemyController>().SetSpeed(2.5f);
		}
	}
}
