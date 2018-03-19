using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
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
		
		_hp = _startHp;
	}


	// Update is called once per frame
	private void Update () {
		OnDeath();
		Slowed();
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

		if (GetComponent<AIPath>().maxSpeed > speed)
		{
			_slowed = true;
		}
		
		GetComponent<AIPath>().maxSpeed = speed;

	}

	public void Release()
	{
		gameObject.SetActive(false);
		WaveManager.Instance.EnemyDied++;
		_hp = _startHp;
		_healthBar.fillAmount = _hp / _startHp;
	}
}
