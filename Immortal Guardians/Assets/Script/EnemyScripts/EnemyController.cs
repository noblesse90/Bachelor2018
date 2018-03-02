using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
	

	private float _startHp;

	private float _hp;

	private int _money;

	[SerializeField]private Image _healthBar;

	public float StartHp
	{
		get { return _startHp; }
		set { _startHp = value; }
	}

	// Use this for initialization
	private void Start () {
		InitializeStats();
	}

	private void InitializeStats()
	{
		switch (gameObject.name)
		{
			case "Enemy01":
				_startHp = 100;
				_money = 1;
				break;
			
			case "Enemy02":
				_startHp = 200;
				_money = 100;
				break;
			
			case "Enemy03":
				_startHp = 300;
				_money = 100;
				break;
			
			case "Enemy04":
				_startHp = 400;
				_money = 100;
				break;
			
			case "Enemy05":
				_startHp = 500;
				_money = 100;
				break;
			
			case "Enemy06":
				_startHp = 600;
				_money = 100;
				break;
			
			case "Enemy07":
				_startHp = 700;
				_money = 100;
				break;
			
			case "Enemy08":
				_startHp = 800;
				_money = 100;
				break;
			
			case "Enemy09":
				_startHp = 900;
				_money = 100;
				break;
			
			case "Enemy10":
				_startHp = 1000;
				_money = 100;
				break;
		}
		_hp = _startHp;
	}


	// Update is called once per frame
	private void Update () {
		OnDeath();
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

	public void Release()
	{
		gameObject.SetActive(false);
		WaveManager.Instance.EnemyDied++;
		_hp = _startHp;
		_healthBar.fillAmount = _hp / _startHp;
	}
}
