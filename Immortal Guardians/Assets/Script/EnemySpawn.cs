﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    [SerializeField]private GameObject _enemyPrefab;

	
	// Update is called once per frame
	void Start () {
        float start = 1;
        float interval = 2;

        InvokeRepeating("spawn", start, interval);
	}

    private void spawn()
    {
        Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
    }
}
