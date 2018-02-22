using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTower : MonoBehaviour {
    // Price, range, attackCooldown, damange, level, Projectilespeed
    float[,] _stats = new float[4, 6]
    {
        { 10,10,1,10,1,10},
        { 20,12,0.75f,20,2,20},
        { 30,14,0.5f,30,3,30},
        { 40,16,0.4f,50, 4,40}
    };


    // Use this for initialization
    void Awake()
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

        if (!(level + 1 > 3))
        {
            tc.UpgradePrice = (int)_stats[level + 1, 0];
        }
        else
        {
            tc.UpgradePrice = 0;
        }

        tc.TotalPrice += (int)_stats[level, 0];

        UIManager.Instance.setTowerStats(tc);
    }
}
