using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTower : MonoBehaviour {
    // Price, range, attackCooldown, damange, level, Projectilespeed
    float[,] _stats = new float[4, 6]
    {
        { 20,10,2,10,1,10},
        { 50,12,1.5f,20,2,12.5f},
        { 50,14,1.25f,25,3,15},
        { 75,16,1f,50, 4,20}
    };


    // Use this for initialization
    void Awake()
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
