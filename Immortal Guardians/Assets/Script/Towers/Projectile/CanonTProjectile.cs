using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTProjectile : MonoBehaviour {

	private GameObject _target;

	private float _speed = 0;

	private int _damage = 0;

	private Animator myAnimator;

	private Vector3 _targetPos;
	private bool _shot = false;

	public int Damage
	{
		get { return _damage; }
		set { _damage = value; }
	}

	private void Start()
	{
		myAnimator = GetComponent<Animator>();
	}

	// Update is called once per frame
	private void Update () {
		MoveToTarget();
	}

	private void MoveToTarget()
	{
		if (!(_shot) && transform.position == _targetPos)
		{
			SplashDamage(transform.position, 5f);
			myAnimator.SetTrigger("Impact");
			_shot = true;
		}
		if(_target != null && _target.activeInHierarchy)
		{
			transform.position = Vector3.MoveTowards(transform.position, _targetPos, Time.deltaTime * _speed);
		}
	}

	/*private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!_target.activeSelf) return;
		if (!collision.GetComponent<Collider2D>().Equals(_target.GetComponent<Collider2D>())) return;
		_target.GetComponent<EnemyController>().TakeDamage(_damage);
		SplashDamage(transform.position, 5f);
		
		// calling cannonball animation and release gameobject after animation is done playing
		myAnimator.SetTrigger("Impact");
	}*/

	public void SetTargetAndSpeed(GameObject target, float speed)
	{
		this._target = target;
		this._targetPos = target.transform.position;
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
