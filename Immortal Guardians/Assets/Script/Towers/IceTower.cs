using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ice Tower that controls all the stats for the tower

public class IceTower : MonoBehaviour {
	// Price, range, attackCooldown, damange, level, Projectilespeed, Slow%
	float[,] _stats = new float[4, 7]
	{
		/* 			 Price  Range	CD		Damage	Level	Speed	Slow% */
		/*Level 1*/{ 30,	7,		3,		10,		1,		20, 	0.7f},
		/*Level 2*/{ 50,	9,		2,		20,		2,		25, 	0.6f},
		/*Level 3*/{ 50,	10,		1.5f,	30,		3,		30, 	0.5f},
		/*Level 4*/{ 75,	13,		1f,		40,		4,		40, 	0.4f}
	};


	// Use this for initialization
	private void Awake()
	{
		InitialiserStats();
	}

	public void InitialiserStats()
	{
		TowerController tc = GetComponent<TowerController>();
		tc.TowerType = "Ice Tower";
		tc.Price = (int)_stats[0, 0];
		tc.Range = _stats[0, 1];
		tc.AttackCooldown = _stats[0, 2];
		tc.Damage = _stats[0, 3];
		tc.Level = (int)_stats[0, 4];
		tc.ProjectileSpeed = _stats[0, 5];
		tc.Slow = _stats[0, 6];
		
		tc.UpgradePrice = (int)_stats[1, 0];

		tc.TotalPrice = (int)_stats[0, 0];
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
		tc.Slow = _stats[level, 6];

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
