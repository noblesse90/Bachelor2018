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
		InitializeStats();
		_slowDuration = 2;
	}

	private void InitializeStats()
	{
		switch (gameObject.name)
		{
			case "Enemy01":
				_startHp = 200;
				_money = 5;
				_defaultSpeed = 5;
				GetComponent<AIPath>().maxSpeed = _defaultSpeed;
				break;
			
			case "Enemy02":
				_startHp = 400;
				_money = 5;
				_defaultSpeed = 5;
				GetComponent<AIPath>().maxSpeed = _defaultSpeed;
				break;
			
			case "Enemy03":
				_startHp = 600;
				_money = 5;
				_defaultSpeed = 5;
				GetComponent<AIPath>().maxSpeed = _defaultSpeed;
				break;
			
			case "Enemy04":
				_startHp = 900;
				_money = 10;
				_defaultSpeed = 7;
				GetComponent<AIPath>().maxSpeed = _defaultSpeed;
				break;
			
			case "Enemy05":
				_startHp = 1200;
				_money = 10;
				_defaultSpeed = 7;
				GetComponent<AIPath>().maxSpeed = _defaultSpeed;
				break;
			
			case "Enemy06":
				_startHp = 1600;
				_money = 20;
				_defaultSpeed = 7;
				GetComponent<AIPath>().maxSpeed = _defaultSpeed;
				break;
			
			case "Enemy07":
				_startHp = 2000;
				_money = 20;
				_defaultSpeed = 7;
				GetComponent<AIPath>().maxSpeed = _defaultSpeed;
				break;
			
			case "Enemy08":
				_startHp = 2400;
				_money = 20;
				_defaultSpeed = 10;
				GetComponent<AIPath>().maxSpeed = _defaultSpeed;
				break;
			
			case "Enemy09":
				_startHp = 3000;
				_money = 20;
				_defaultSpeed = 10;
				GetComponent<AIPath>().maxSpeed = _defaultSpeed;
				break;
			
			case "Enemy10":
				_startHp = 4000;
				_money = 20;
				_defaultSpeed = 10;
				GetComponent<AIPath>().maxSpeed = _defaultSpeed;
				break;
			
			default: break;
		}
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
