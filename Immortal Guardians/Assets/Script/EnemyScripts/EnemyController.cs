using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
	
	// if the enemy is a boss or not
	private bool _boss;

	public bool Boss
	{
		get { return _boss; }
	}

	private float _startHp;

	private float _hp;

	private int _money;

	[SerializeField]private Image _healthBar;
	
	
	// SLOW EFFECT
	
	private bool _slowed = false;
	private float _slowTimer, _slowDuration;

	private float _defaultSpeed;

	public float DefaultSpeed
	{
		get { return _defaultSpeed; }
	}
	
	// PULL EFFECT

	private bool _pull = false;
	private GameObject _target;
	private Vector2 _targetPosition;



	// Use this for initialization
	private void Start () {
		_slowDuration = 2;
	}

	public void InitializeStats(Enemy enemy)
	{
		_startHp = enemy.Health;
		_money = enemy.Money;
		_defaultSpeed = enemy.Defaultspeed;
		GetComponent<AIPath>().maxSpeed = _defaultSpeed;
		transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = enemy.Art;
		transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = enemy.Color;

		_boss = enemy.Boss;
		
		_hp = _startHp;
	}


	// Update is called once per frame
	private void Update () {
		OnDeath();
		Slowed();

		if (_pull)
		{
			FollowTarget();
		}
	}

	private void OnDeath()
	{
		if(_hp <= 0)
		{
			Release();
			UIManager.Instance.Currency += _money;
			if (PlayerController.Instance.Mana >= PlayerController.Instance.MaxMana-10)
			{
				PlayerController.Instance.Mana = PlayerController.Instance.MaxMana;
			}
			else
			{
				PlayerController.Instance.Mana += 10;
			}
			
		}   
	}
    
	public void TakeDamage(float dmg)
	{
		_hp -= dmg;
		_healthBar.fillAmount = _hp / _startHp;
	}

	private void Slowed()
	{
		if (_slowed)
		{
			_slowTimer += Time.deltaTime;

			if (_slowTimer >= _slowDuration)
			{
				_slowed = false;
				_slowTimer = 0;
				SetSpeed(_defaultSpeed);
			}
		}
	}

	public void SetSpeed(float speed)
	{
		if (speed < 0) return;

		if (GetComponent<AIPath>().maxSpeed > speed && !_pull)
		{
			_slowed = true;
		}
		
		GetComponent<AIPath>().maxSpeed = speed;

	}

	public void InitializePullTarget(GameObject target)
	{
		_target = target;
		_pull = true;
		_targetPosition = _target.transform.position;
		SetSpeed(0);
		
	}

	private void FollowTarget()
	{
		if (Vector2.Distance(gameObject.transform.position, _target.transform.position) > 1)
		{
			_targetPosition = _target.transform.position;
			gameObject.transform.position =
				Vector3.MoveTowards(gameObject.transform.position, _targetPosition, Time.deltaTime * 100);
		}
		else
		{
			SetSpeed(_defaultSpeed);
			_pull = false;
		}
	}

	public void Release()
	{
		WaveManager.Instance.EnemyDied++;
		Destroy(gameObject);
	}
}
