using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class PlayerProjectile : MonoBehaviour
{

	private float _speed = 0, _damage = 0;

	private int _offset;

	private Vector2 _playerPos, _directionPos, _normalizeDirection;
	

	public void InstantiateProjectile(float damage, float speed, Vector2 playerPos, Vector2 direction, int offset)
	{
		_offset = offset;
		_directionPos = direction;
		_damage = damage;
		_speed = speed;
		_playerPos = playerPos;
		_playerPos.y += 1;
	}

	private void Start()
	{
		transform.position = _playerPos;
		
		// direction the arrow is moving towards (with offset)
		_normalizeDirection = (_directionPos - (Vector2) transform.position);
		_normalizeDirection = Quaternion.Euler(0, _offset, _offset) * _normalizeDirection;
		_normalizeDirection = _normalizeDirection.normalized;
		
		// angle the arrow correctly
		TransformRotation();

	}

	// Update is called once per frame
	private void Update () {
		Shoot();
	}

	private void Shoot()
	{
		if (Vector2.Distance(_playerPos, transform.position) > 20)
		{
			ObjectPool.Instance.ReleaseObject(gameObject);
		}
		transform.position += (Vector3)_normalizeDirection * _speed * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D otherObject)
	{
		if (otherObject.CompareTag("Enemy") || otherObject.CompareTag("Obstacle"))
		{
			GameObject aniPrefab = ObjectPool.Instance.GetObject("ArrowExplosion");
			if (otherObject.CompareTag("Obstacle"))
			{
				aniPrefab.transform.position = transform.position;
			}
			else
			{
				aniPrefab.transform.position = otherObject.transform.position;
				otherObject.GetComponent<EnemyController>().TakeDamage(_damage);
			}
			ObjectPool.Instance.ReleaseObject(gameObject);
			
		}
	}

	private void TransformRotation()
	{
		float angle = Mathf.Atan2(_normalizeDirection.y, _normalizeDirection.x) * Mathf.Rad2Deg;
		
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

}
