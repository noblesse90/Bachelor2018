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
				_startHp = 1000;
				_money = 100;
				break;
		}
		_hp = _startHp;
	}


	// Update is called once per frame
	private void Update () {
		if(_hp <= 0)
		{
			Release();
			UIManager.Instance.Currency += _money;
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
