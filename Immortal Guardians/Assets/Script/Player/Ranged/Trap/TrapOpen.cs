using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapOpen : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			transform.parent.GetComponent<Trap>().TrapClose.SetActive(true);
			transform.parent.GetComponent<Trap>().Collider.SetActive(true);
			gameObject.SetActive(false);
		}
	}
}
