using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTest : MonoBehaviour
{

	[SerializeField] Transform _center;
	private Vector3 _axis;
	private float _radius;
	private float _radiusSpeed;
	private float _rotationSpeed;
 
	private void Start() {
		_axis = new Vector3(0,0,1);
		_radius = 2.0f;
		_radiusSpeed = 0.5f;
		_rotationSpeed = -400.0f;
		transform.position = (transform.position - _center.position).normalized * _radius + _center.position;
	}
 
	private void Update() {
		transform.RotateAround (_center.position, _axis, _rotationSpeed * Time.deltaTime);
		var desiredPosition = (transform.position - _center.position).normalized * _radius + _center.position;
		transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * _radiusSpeed);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			other.GetComponent<EnemyController>().TakeDamage(10);
		}
	}
}
