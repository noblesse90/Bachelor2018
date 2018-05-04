using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cannon Tower that controls all the stats for the tower

public class CanonTower : MonoBehaviour {
    // Price, range, attackCooldown, damange, level, Projectilespeed
    float[,] _stats = new float[4, 6]
    {
        /* 			 Price  Range	CD		Damage	Level	Speed	 */
        /*Level 1*/{ 20,    10,     2,      15,     1,      15},
        /*Level 2*/{ 50,    12,     1.5f,   30,     2,      12.20f},
        /*Level 3*/{ 50,    14,     1.25f,  40,     3,      20},
        /*Level 4*/{ 75,    30,     0.8f,   70,     4,      25}
    };


    // Use this for initialization
    private void Awake()
    {
        InitialiserStats();
    }

    public void InitialiserStats()
    {
        TowerController tc = GetComponent<TowerController>();
        tc.TowerType = "Canon Tower";
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

        if(!(level + 1 > 3))
        {
            tc.UpgradePrice = (int)_stats[level + 1, 0];
        }
        else
        {
            tc.UpgradePrice = 0;
        }

        tc.TotalPrice += (int)_stats[level, 0];

        UIManager.Instance.SetTowerStats(tc);
    }

}
