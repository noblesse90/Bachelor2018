using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// rotates an object (this case, the circles that indicates a toggable ability)

public class ToggleUIRotation : MonoBehaviour {
	
	void Update () {
		transform.Rotate(new Vector3(0,0,-90) * Time.deltaTime);
	}
}
