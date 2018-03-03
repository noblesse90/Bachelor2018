using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class PlayerProjectile : MonoBehaviour
{

	private float _speed = 0, _damage = 0;

	private int _offset;

	private Vector2 _playerPos, _directionPos, _normalizeDirection;

	private bool _fractualShot = false;

	public bool FractualShot
	{
		get { return _fractualShot; }
		set { _fractualShot = value; }
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

	public void InstantiateFProjectile(float damage, float speed, Vector2 playerPos, Vector2 direction, int offset)
	{
		_offset = offset;
		_directionPos = direction;
		_damage = damage;
		_speed = speed;
		_playerPos = playerPos;
		
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
		if (_fractualShot)
		{
			// TODO SOMETHING
			if (Vector2.Distance(_playerPos, transform.position) > 4)
			{
				if (PlayerController.Instance.Fshot > 0)
				{
					PlayerController.Instance.Fshot--;
					FShoot();
					
				}
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


	private void FShoot()
	{
		Vector2 pos = transform.GetChild(0).transform.position;
		GameObject projectile = ObjectPool.Instance.GetObject("PlayerArrow");
		projectile.GetComponentInChildren<PlayerProjectile>().FractualShot = true;
		projectile.GetComponentInChildren<PlayerProjectile>().InstantiateFProjectile(_damage, 25f, transform.position, pos, 20);
		
		projectile = ObjectPool.Instance.GetObject("PlayerArrow");
		projectile.GetComponentInChildren<PlayerProjectile>().FractualShot = true;
		projectile.GetComponentInChildren<PlayerProjectile>().InstantiateFProjectile(_damage, 25f, transform.position, pos, -20);
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
		GameObject aniPrefab = ObjectPool.Instance.GetObject("ArrowExplosion");
		aniPrefab.transform.position = transform.position;
		_fractualShot = false;
		gameObject.transform.parent.gameObject.SetActive(false);
	}

}
