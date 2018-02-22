using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    private bool canSpawn = true;
    private float spawnTimer;
    [SerializeField] private float cooldownTimer;
    
    private void LateUpdate()
    {
        if (WaveManager.Instance.SpawnMode)
        {
            spawn();
        }


        if(WaveManager.Instance.EnemySpawned == WaveManager.Instance.EnemiesPerWave)
        {
            if (WaveManager.Instance.EnemySpawned == WaveManager.Instance.EnemyCount)
            {
                UIManager.Instance.NextWave.transform.gameObject.SetActive(true);
                WaveManager.Instance.SpawnMode = false;
            }
        }
        
    }


    private void spawn()
    {
        if (!canSpawn)
        {
            spawnTimer += Time.deltaTime;

            if(spawnTimer >= cooldownTimer)
            {
                canSpawn = true;
                spawnTimer = 0;
            }
        }

        if (canSpawn && WaveManager.Instance.EnemySpawned < WaveManager.Instance.EnemiesPerWave)
        {
            GameObject enemy = ObjectPool.Instance.GetObject("Enemy");
            enemy.transform.position = transform.position;
            WaveManager.Instance.EnemySpawned++;
            canSpawn = false;
        }


    }

}
