using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class BasicTower : MonoBehaviour {

    // Price, range, attackCooldown, damange, level, Projectilespeed
    readonly float[,] _stats = new float[4, 6]
    {
        /*Start stats*/{ 10,7,1,7,1,20},
        /*Level 2*/{ 25,9,0.9f,10,2,25},
        /*Level 3*/{ 25,10,0.7f,15,3,30},
        /*Level 4*/{ 50,13,0.4f,25, 4,40}
    };


    // Use this for initialization
    private void Awake ()
    {
        InitialiserStats();
    }

    public void InitialiserStats()
    {
        TowerController tc = GetComponent<TowerController>();
        tc.TowerType = "Basic Tower";
        tc.Price = (int)_stats[0, 0];
        tc.Range = _stats[0, 1];
        tc.AttackCooldown = _stats[0, 2];
        tc.Damage = _stats[0, 3];
        tc.Level = (int)_stats[0, 4];
        tc.ProjectileSpeed = _stats[0, 5];

        tc.UpgradePrice = (int)_stats[1, 0];

        tc.TotalPrice = (int)_stats[0, 0];

        tc.Slow = 0;
        tc.Poison = 0;
    }

    public void Upgrade(int level)
    {
        TowerController tc = GetComponent<TowerController>();
        tc.Price = (int)_stats[level, 0];
        tc.Range = _stats[level, 1];
        tc.AttackCooldown = _stats[level, 2];
        tc.Damage = _stats[level, 3];
        tc.Level = (int)_stats[level, 4];
        tc.ProjectileSpeed = _stats[level, 5];

        if(!(level+1 > 3))
        {
            tc.UpgradePrice = (int)_stats[level+1, 0];
        }
        else
        {
            tc.UpgradePrice = 0;
        }

        tc.TotalPrice += (int)_stats[level, 0];

        UIManager.Instance.SetTowerStats(tc);
    }
}
