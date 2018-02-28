using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {
	
	private float _speed = 0;

	private int _damage = 0;

	private Vector2 _playerPos;

	private Vector2 _mousePos;

	private Vector2 _normalizeDirection;

	public void InstantiateProjectile(int damage, float speed, Vector2 playerPos )
	{
		_damage = damage;
		_speed = speed;
		_playerPos = playerPos;
		_playerPos.y += 1;
	}

	private void Start()
	{
		_mousePos = GManager.Instance.GetMousePos();
		transform.position = _playerPos;
		_normalizeDirection = (_mousePos - (Vector2) transform.position).normalized;
		GetDirection();
	}

	// Update is called once per frame
	private void Update () {
		Move();
	}

	private void Move()
	{
		if (Vector2.Distance(_playerPos, transform.position) > 20)
		{
			ObjectPool.Instance.ReleaseObject(gameObject);
		}
		transform.position += (Vector3)_normalizeDirection * _speed * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy") || other.CompareTag("Obstacle"))
		{
			
			GameObject aniPrefab = ObjectPool.Instance.GetObject("ArrowExplosion");
			aniPrefab.transform.position = new Vector2(transform.position.x, transform.position.y);
			ObjectPool.Instance.ReleaseObject(gameObject);
			if (other.CompareTag("Obstacle")) return;
			other.GetComponent<EnemyController>().TakeDamage(_damage);
		}
	}

	private void GetDirection()
	{
		Vector2 dir = _mousePos - (Vector2)transform.position;

		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

}
