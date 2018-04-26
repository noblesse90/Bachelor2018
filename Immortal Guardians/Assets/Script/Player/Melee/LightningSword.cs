using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSword : MonoBehaviour
{

	private GameObject _player;

	public GameObject Player
	{
		get { return _player; }
		set { _player = value; }
	}

	private bool _takeDamage = true;
	private float _dmgTimer, _dmgCooldown = 1;
	private float _destroyTimer, _destroyCooldown = 10;

	private List<GameObject> _enemies = new List<GameObject>();

	private void Update()
	{
		_destroyTimer += Time.deltaTime;
		if (_destroyTimer >= _destroyCooldown)
		{
			DestroySword();
		}
		
		
		if (!_takeDamage)
		{
			_dmgTimer += Time.deltaTime;
			if (_dmgTimer >= _dmgCooldown)
			{
				_dmgTimer = 0;
				_takeDamage = true;
			}
		}
		else
		{
			foreach (GameObject enemy in _enemies)
			{
				enemy.GetComponent<EnemyController>().TakeDamage(50);
			}

			_takeDamage = false;
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			other.GetComponent<EnemyController>().SetSpeed(3.5f);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			_enemies.Add(other.gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			_enemies.Remove(other.gameObject);
		}
	}

	public void Teleport()
	{
		_player.transform.position = transform.position;
		_player.GetComponent<PlayerController>().LSword = null;
		PlayerController.Instance.LSwordActive = false;
		Destroy(gameObject);
	}

	public void DestroySword()
	{
		_player.GetComponent<PlayerController>().LSword = null;
		PlayerController.Instance.LSwordActive = false;
		Destroy(gameObject);
	}
}
