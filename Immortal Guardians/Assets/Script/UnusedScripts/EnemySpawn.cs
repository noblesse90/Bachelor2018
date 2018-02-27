/*

using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    private bool _canSpawn = true;
    private float _spawnTimer;
    [SerializeField] private float _cooldownTimer;
    
    private void LateUpdate()
    {
        if (WaveManager.Instance.SpawnMode)
        {
            Spawn();
        }

        if (WaveManager.Instance.EnemySpawned != WaveManager.Instance.EnemiesPerWave) return;
        if (WaveManager.Instance.EnemySpawned != WaveManager.Instance.EnemyDied) return;
        UIManager.Instance.NextWaveBtn.transform.gameObject.SetActive(true);
        WaveManager.Instance.SpawnMode = false;

    }


    private void Spawn()
    {
        if (!_canSpawn)
        {
            _spawnTimer += Time.deltaTime;

            if(_spawnTimer >= _cooldownTimer)
            {
                _canSpawn = true;
                _spawnTimer = 0;
            }
        }


        if (_canSpawn || WaveManager.Instance.EnemySpawned < WaveManager.Instance.EnemiesPerWave)
        {
            GameObject enemy= ObjectPool.Instance.GetObject("Enemy");
            enemy.transform.position = transform.position;
            WaveManager.Instance.EnemySpawned++;
            _canSpawn = false;
        }

    }

}

*/
