using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class PlayerProjectile : MonoBehaviour
{

	private float _speed = 0, _damage = 0;

	private int _offset;

	private Vector2 _playerPos, _directionPos, _normalizeDirection;

	private bool _piercing = false;

	private GameObject _enemy = null;

	public GameObject Enemy
	{
		get { return _enemy; }
		set { _enemy = value; }
	}

	public bool Piercing
	{
		get { return _piercing; }
		set { _piercing = value; }
	}

	public void InstantiateProjectile(float damage, float speed, Vector2 playerPos, Vector2 direction, int offset)
	{
		_offset = offset;
		_directionPos = direction;
		_damage = damage;
		_speed = speed;
		_playerPos = playerPos;
		_playerPos.y += 1; 
		
		transform.position = _playerPos;
		
		// direction the arrow is moving towards (with offset)
		_normalizeDirection = (_directionPos - (Vector2) transform.position);
		_normalizeDirection = Quaternion.Euler(0, _offset, _offset) * _normalizeDirection;
		_normalizeDirection = _normalizeDirection.normalized;
		
		// angle the arrow correctly
		TransformRotation();
	}

	public void InstantiateSProjectile(float damage, float speed, Vector2 playerPos, Vector2 direction, int offset)
	{
		_offset = offset;
		_directionPos = direction;
		_damage = damage;
		_speed = speed;
		_playerPos = playerPos;
		
		transform.position = _playerPos;
		
		// direction the arrow is moving towards (with offset)
		_normalizeDirection = (_directionPos - (Vector2) transform.position);
		_normalizeDirection = Quaternion.Euler(0, 0, _offset) * _normalizeDirection;
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
			Release();
		}

		
		transform.position += (Vector3)_normalizeDirection * _speed * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D otherObject)
	{
		if (otherObject.CompareTag("Obstacle"))
		{
			Release();
		}
		else if (otherObject.CompareTag("Enemy"))
		{
			otherObject.GetComponent<EnemyController>().TakeDamage(_damage);
			if (_piercing) return;
			Release();
		}
	}

	private void TransformRotation()
	{
		float angle = Mathf.Atan2(_normalizeDirection.y, _normalizeDirection.x) * Mathf.Rad2Deg;
		
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
	
	private void Release()
	{
		// resets color of the projectile
		Color color = new Color(0.3f,1f,1f);
		GetComponentInChildren<SpriteRenderer>().color = color;
		
		// adds explosion where it lands
		GameObject aniPrefab = ObjectPool.Instance.GetObject("ArrowExplosion");
		aniPrefab.transform.position = transform.position;
		
		// Plays a hit sound
		AudioManager.Instance.Play("Arrow_Hit");
		
		// Sets piercing to false
		_piercing = false;
		
		// sets current parent to false so it can be reused in object pool
		gameObject.transform.parent.gameObject.SetActive(false);
		
		// sets the enemy saved to null so it can be hit
		_enemy = null;
	}

}
