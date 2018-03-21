using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTProjectile : MonoBehaviour {

	private GameObject _target;

	private float _speed = 0;

	private int _damage = 0;

	private Vector3 _targetPos;
	private bool _shot = false;

	public int Damage
	{
		get { return _damage; }
		set { _damage = value; }
	}

	// Update is called once per frame
	private void Update () {
		MoveToTarget();
	}

	private void MoveToTarget()
	{
		if(_target != null && _target.activeInHierarchy)
		{
			_targetPos = _target.transform.position;
			transform.position = Vector3.MoveTowards(transform.position, _targetPos, Time.deltaTime * _speed);
			
			
		}
		else
		{
			transform.position = Vector3.MoveTowards(transform.position, _targetPos, Time.deltaTime * _speed);
			_target = null;
		}
		
		Explode();
		
	}

	private void Explode()
	{
		if (!(_shot) && Vector2.Distance(transform.position,_targetPos) < 1)
		{
			SplashDamage(transform.position, 2.40f);
			
			GameObject aniPrefab = ObjectPool.Instance.GetObject("CannonExplosion");
			aniPrefab.transform.position = new Vector2(transform.position.x, transform.position.y + 1);

			_shot = true;
			Release();
			
		}
	}

	public void SetTargetAndSpeed(GameObject target, float speed)
	{
		this._target = target;
		this._speed = speed;
	}

	private void SplashDamage(Vector2 center, float radius)
	{
		Collider2D[] hit = Physics2D.OverlapCircleAll(center, radius);

		foreach (var enemy in hit)
		{
			if (!enemy.CompareTag("Enemy")) continue;
			enemy.GetComponent<EnemyController>().TakeDamage(Damage);

		}
	}

	public void Release()
	{
		gameObject.SetActive(false);
		_shot = false;
	}
}
