﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingSwordScript : MonoBehaviour
{

	Transform _center;
	private Vector3 _axis, _t;
	private float _radius;
	private float _radiusSpeed;
	private float _rotationSpeed;
	private SpriteRenderer _sr;
	private Color _color;
	private float _alpha = 0;
	private float _damage;


	private void Start()
	{
		_center = transform.parent.transform;
		_axis = new Vector3(0,0,1);
		_radius = 0.1f;
		_radiusSpeed = 1.5f;
		_rotationSpeed = -400.0f;
		transform.position = (_t - _center.position).normalized * _radius + _center.position;
		_sr = GetComponent<SpriteRenderer>();
		_sr.color = new Color(255, 255, 255, 0);
	}

	public void TransformRotationDamage(Vector3 t, Vector3 r, float dmg)
	{
		_t = t;
		transform.Rotate(r);
		_damage = dmg;
	}

	private void Update()
	{
		if (_alpha <= 1 && PlayerController.Instance.OrbitingSwordBool)
		{
			_alpha += 0.5f * Time.deltaTime;
			_color = new Color(1,1,1, _alpha);
		}

		if (!PlayerController.Instance.OrbitingSwordBool)
		{
			DestroySword();
		}

		_sr.color = _color;
		_radius = 3.0f;
		transform.RotateAround (_center.position, _axis, _rotationSpeed * Time.deltaTime);
		var desiredPosition = (transform.position - _center.position).normalized * _radius + _center.position;
		transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * _radiusSpeed);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			other.GetComponent<EnemyController>().TakeDamage(_damage);
		}
	}

	private void DestroySword()
	{
		if (_alpha > 0)
		{
			_alpha -= 1f * Time.deltaTime*2.1f;
			_color = new Color(1,1,1, _alpha);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
