using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeProjectile : MonoBehaviour {

	private float _speed = 0, _damage = 0, _x = 0.1f, _y = 0.1f;


	private Vector2 _playerPos, _directionPos, _normalizeDirection;
	
	
	public void InstantiateProjectile(float damage, float speed, Vector2 playerPos, Vector2 direction)
	{
		_directionPos = direction;
		_damage = damage;
		_speed = speed;
		_playerPos = playerPos;
		_playerPos.y += 1; 
		
		transform.position = _playerPos;
		
		// direction the arrow is moving towards
		_normalizeDirection = (_directionPos - (Vector2) transform.position);
		_normalizeDirection = _normalizeDirection.normalized;
		
		// angles the cone correctly
		TransformRotation();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Vector2.Distance(_playerPos, transform.position) > 5)
		{
			Release();
			GameObject explo = ObjectPool.Instance.GetObject("ArrowExplosion");
			explo.transform.position = transform.position;
		}

		_x += 4f * Time.deltaTime;
		_y += 4f * Time.deltaTime;
		transform.localScale = new Vector2(_x, _y);
		
		transform.position += (Vector3)_normalizeDirection * _speed * Time.deltaTime;
	}
	
	private void TransformRotation()
	{
		float angle = Mathf.Atan2(_normalizeDirection.y, _normalizeDirection.x) * Mathf.Rad2Deg;
		
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Obstacle"))
		{
			Release();
			GameObject explo = ObjectPool.Instance.GetObject("ArrowExplosion");
			explo.transform.position = transform.position;
		}
		if (other.CompareTag("Enemy"))
		{
			other.GetComponent<EnemyController>().TakeDamage(_damage);
			GameObject explo = ObjectPool.Instance.GetObject("ArrowExplosion");
			explo.transform.position = other.transform.position;
		}
		
	}

	private void Release()
	{
		gameObject.SetActive(false);
		_x = 0.1f;
		_y = 0.1f;
	}
}
