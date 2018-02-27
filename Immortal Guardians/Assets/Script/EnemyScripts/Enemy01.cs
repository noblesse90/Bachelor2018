using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class Enemy01 : EnemyController {

    private float _startHp = 100;

    private float _hp;

    [SerializeField] private Image _healthBar;

    public float StartHp
    {
        get { return _startHp; }
        set { _startHp = value; }
    }

    // Use this for initialization
    private void Start () {
        _hp = _startHp;
	}


    // Update is called once per frame
    private void Update () {
		if(_hp <= 0)
        {
            Release();
            UIManager.Instance.Currency += 1;
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
