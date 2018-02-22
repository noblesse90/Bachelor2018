using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class EnemyController : MonoBehaviour {

    private float startHP = 200;
    private float hp;

    [SerializeField] private Image healthBar;

    public float StartHP
    {
        get
        {
            return startHP;
        }

        set
        {
            startHP = value;
        }
    }


    // Use this for initialization
    void Start () {
        hp = startHP;
	}


    // Update is called once per frame
    void Update () {
		if(hp <= 0)
        {
            Release();
        }   
	}


    public void takeDamage(float dmg)
    {
        hp -= dmg;
        healthBar.fillAmount = hp / startHP;
    }

    public void Release()
    {
        gameObject.SetActive(false);
        WaveManager.Instance.EnemyCount++;
        hp = startHP;
        healthBar.fillAmount = hp / startHP;
    }
}
