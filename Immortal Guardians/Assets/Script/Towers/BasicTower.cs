using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class BasicTower : MonoBehaviour {

    // Price, range, attackCooldown, damange, level, Projectilespeed
    readonly float[,] _stats = new float[4, 6]
    {
        { 10,7,1,10,1,10},
        { 25,9,0.9f,20,2,15},
        { 25,10,0.7f,25,3,20},
        { 50,13,0.4f,50, 4,30}
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
