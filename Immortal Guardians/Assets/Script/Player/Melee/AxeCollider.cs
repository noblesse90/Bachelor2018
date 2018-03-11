using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeCollider : MonoBehaviour {
	
	private float _speed = 0, _damage = 0;

	private int _offset;

	private Vector2 _playerPos, _directionPos, _normalizeDirection;
	
	private List<GameObject> _enemies = new List<GameObject>();
	
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
	}
	
	// Update is called once per frame
	void Update () {
		if (_enemies.Count > 3)
		{
			Release();
		}
		
		transform.Rotate(0,0,-500f * Time.deltaTime);
		
		if (Vector2.Distance(_playerPos, transform.position) > 20)
		{
			Release();
			GameObject explo = ObjectPool.Instance.GetObject("ArrowExplosion");
			explo.transform.position = transform.position;
		}

		
		transform.position += (Vector3)_normalizeDirection * _speed * Time.deltaTime;
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
			_enemies.Add(other.gameObject);
			other.GetComponent<EnemyController>().SetSpeed(other.GetComponent<EnemyController>().DefaultSpeed * 0.5f);
			other.GetComponent<EnemyController>().TakeDamage(50);
			GameObject explo = ObjectPool.Instance.GetObject("ArrowExplosion");
			explo.transform.position = other.transform.position;
		}
		
	}

	private void Release()
	{
		gameObject.SetActive(false);
		_enemies.Clear();
		transform.rotation = Quaternion.identity;

		
	}
}
