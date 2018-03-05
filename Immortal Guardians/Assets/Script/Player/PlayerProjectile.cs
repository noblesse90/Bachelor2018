using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class PlayerProjectile : MonoBehaviour
{

	private float _speed = 0, _damage = 0;

	private int _offset;

	private Vector2 _playerPos, _directionPos, _normalizeDirection;

	private bool _scatterShot = false;

	private GameObject _enemy = null;

	public GameObject Enemy
	{
		get { return _enemy; }
		set { _enemy = value; }
	}

	public bool ScatterShot
	{
		get { return _scatterShot; }
		set { _scatterShot = value; }
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
		if (_scatterShot)
		{
			if (Vector2.Distance(_playerPos, transform.position) > 20)
			{
				SShot(null);
				Release();		
			}
		}
		else
		{
			if (Vector2.Distance(_playerPos, transform.position) > 20)
			{
				Release();
			}
		}
		
		transform.position += (Vector3)_normalizeDirection * _speed * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D otherObject)
	{
		if (otherObject.CompareTag("Enemy") || otherObject.CompareTag("Obstacle"))
		{
			if (otherObject.CompareTag("Obstacle"))
			{
				Release();
			}
			else
			{
				if (_scatterShot)
				{
					SShot(otherObject.gameObject);
				}

				if (_enemy == otherObject.gameObject) return;
				//aniPrefab.transform.position = otherObject.transform.position;
				otherObject.GetComponent<EnemyController>().TakeDamage(_damage);
				otherObject.GetComponent<EnemyController>().SetSpeed(otherObject.GetComponent<EnemyController>().DefaultSpeed * 0.5f);
				Release();
			}
		}
	}

	private void TransformRotation()
	{
		float angle = Mathf.Atan2(_normalizeDirection.y, _normalizeDirection.x) * Mathf.Rad2Deg;
		
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
	
	
	private void SShot(GameObject enemy)
	{
		Color c = new Color(0.9f, 0.137f, 0.137f);
		Vector2 pos = transform.GetChild(0).transform.position;
		GameObject projectile = ObjectPool.Instance.GetObject("PlayerArrow");
		projectile.GetComponentInChildren<PlayerProjectile>().InstantiateSProjectile(_damage, 25f, transform.position, pos, 45);
		projectile.GetComponentInChildren<SpriteRenderer>().color = c;
		if (enemy != null)
		{
			projectile.GetComponentInChildren<PlayerProjectile>().Enemy = enemy;
		}
		
		projectile = ObjectPool.Instance.GetObject("PlayerArrow");
		projectile.GetComponentInChildren<PlayerProjectile>().InstantiateSProjectile(_damage, 25f, transform.position, pos, 90);
		projectile.GetComponentInChildren<SpriteRenderer>().color = c;
		if (enemy != null)
		{
			projectile.GetComponentInChildren<PlayerProjectile>().Enemy = enemy;
		}
		
		projectile = ObjectPool.Instance.GetObject("PlayerArrow");
		projectile.GetComponentInChildren<PlayerProjectile>().InstantiateSProjectile(_damage, 25f, transform.position, pos, 135);
		projectile.GetComponentInChildren<SpriteRenderer>().color = c;
		if (enemy != null)
		{
			projectile.GetComponentInChildren<PlayerProjectile>().Enemy = enemy;
		}
		
		projectile = ObjectPool.Instance.GetObject("PlayerArrow");
		projectile.GetComponentInChildren<PlayerProjectile>().InstantiateSProjectile(_damage, 25f, transform.position, pos, 180);
		projectile.GetComponentInChildren<SpriteRenderer>().color = c;
		if (enemy != null)
		{
			projectile.GetComponentInChildren<PlayerProjectile>().Enemy = enemy;
		}
		
		projectile = ObjectPool.Instance.GetObject("PlayerArrow");
		projectile.GetComponentInChildren<PlayerProjectile>().InstantiateSProjectile(_damage, 25f, transform.position, pos, -45);
		projectile.GetComponentInChildren<SpriteRenderer>().color = c;
		if (enemy != null)
		{
			projectile.GetComponentInChildren<PlayerProjectile>().Enemy = enemy;
		}
		
		projectile = ObjectPool.Instance.GetObject("PlayerArrow");
		projectile.GetComponentInChildren<PlayerProjectile>().InstantiateSProjectile(_damage, 25f, transform.position, pos, -90);
		projectile.GetComponentInChildren<SpriteRenderer>().color = c;
		if (enemy != null)
		{
			projectile.GetComponentInChildren<PlayerProjectile>().Enemy = enemy;
		}
		
		projectile = ObjectPool.Instance.GetObject("PlayerArrow");
		projectile.GetComponentInChildren<PlayerProjectile>().InstantiateSProjectile(_damage, 25f, transform.position, pos, -135);
		projectile.GetComponentInChildren<SpriteRenderer>().color = c;
		if (enemy != null)
		{
			projectile.GetComponentInChildren<PlayerProjectile>().Enemy = enemy;
		}
		
		projectile = ObjectPool.Instance.GetObject("PlayerArrow");
		projectile.GetComponentInChildren<PlayerProjectile>().InstantiateSProjectile(_damage, 25f, transform.position, pos, 0);
		projectile.GetComponentInChildren<SpriteRenderer>().color = c;
		if (enemy != null)
		{
			projectile.GetComponentInChildren<PlayerProjectile>().Enemy = enemy;
		}		
	}
	
	
	

	private void Release()
	{
		// resets color of the projectile
		Color color = new Color(0.3f,1f,1f);
		GetComponentInChildren<SpriteRenderer>().color = color;
		
		// adds explosion where it lands
		GameObject aniPrefab = ObjectPool.Instance.GetObject("ArrowExplosion");
		aniPrefab.transform.position = transform.position;
		
		// sets it to false so the projectile doesn't spawn more arrows
		_scatterShot = false;
		
		// sets current parent to false so it can be reused in object pool
		gameObject.transform.parent.gameObject.SetActive(false);
		
		// sets the enemy saved to null so it can be hit
		_enemy = null;
	}

}
