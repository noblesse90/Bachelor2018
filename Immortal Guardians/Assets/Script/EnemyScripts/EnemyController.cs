﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    private float startHP = 400;
    private float hp;

    [SerializeField] private Image healthBar;


    public float Hp
    {
        get
        {
            return hp;
        }

        set
        {
            hp = value;
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
            Destroy(gameObject);
        }
        
	}


    public void takeDamage(float dmg)
    {
        Hp -= dmg;
        healthBar.fillAmount = hp / startHP;
    }
}
