﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

    private Queue<GameObject> targets;

    private GameObject currentTarget;

    [SerializeField]private GameObject projectile;

    private bool canAttack = true;

    private float attackTimer;

    [SerializeField] private float attackCooldown;

    [SerializeField] private float projectileSpeed;

    private GameObject rangeSprite;


    public Queue<GameObject> Targets
    {
        get
        {
            return targets;
        }
    }

    public GameObject CurrentTarget
    {
        get
        {
            return currentTarget;
        }

        set
        {
            currentTarget = value;
        }
    }

    public float ProjectileSpeed
    {
        get
        {
            return projectileSpeed;
        }
    }


    // Use this for initialization
    void Start () {
        targets = new Queue<GameObject>();
        rangeSprite = transform.Find("RangeSprite").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        Attack();

        if(GManager.Instance.CurrentTower == gameObject)
        {
            Selected();
        }
        else
        {
            Deselected();
        }
	}

    private void Attack()
    {
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;

            if(attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }

        if(currentTarget == null && targets.Count > 0)
        {
           currentTarget = targets.Dequeue();
        }

        if(currentTarget != null && currentTarget.activeInHierarchy)
        {
            if (canAttack)
            {
                shoot();
                canAttack = false;
            }
        }
        

    }

    private void shoot()
    {
        Vector3 currentPos = transform.position;
        currentPos.y += 1;
        GameObject projectile = ObjectPool.Instance.GetObject("ProjectileTest");
        projectile.transform.position = currentPos;
        projectile.transform.rotation = Quaternion.identity;
        projectile.transform.parent = this.transform;
        projectile.GetComponent<ProjectileController>().target = currentTarget;
        //Instantiate(projectile, currentPos, Quaternion.identity, this.transform);
    }

    private void Selected()
    {
        rangeSprite.SetActive(true);
    }

    private void Deselected()
    {
        rangeSprite.SetActive(false);
    }
}
