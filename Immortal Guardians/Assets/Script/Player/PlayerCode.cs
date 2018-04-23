using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCode : Singleton<PlayerCode> {

	public bool IsEmpty(Vector2 mousePos)
	{
		var mousePosition = GManager.Instance.GetMousePos();
		bool empty = true;
		RaycastHit2D[] rayCenter = Physics2D.RaycastAll(mousePosition, new Vector2(0, 0), 0F);

		foreach (RaycastHit2D r in rayCenter)
		{
			if (!r.collider.CompareTag("Obstacle")) continue;
			Debug.Log("Bottom left");
			empty = false;
		}

		return empty;
	}
}
