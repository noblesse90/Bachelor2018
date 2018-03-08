using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using NUnit.Framework.Internal;
using TMPro;
using Random = System.Random;

public class WaveManager : Singleton<WaveManager> {

    private bool _spawnMode = false;
    
    private GameObject[] _spawnLocations;

    private int _enemiesPerWave = 30;
    private int _enemyDied = 0;
    private int _enemySpawned = 0;
    
    private bool _canSpawn = true;
    private float _spawnTimer;
    [SerializeField] private float _cooldownTimer;

    private string[] _enemyTypes = {"Enemy01", "Enemy02", "Enemy03", "Enemy04", "Enemy05", "Enemy06", "Enemy07", "Enemy08", "Enemy09", "Enemy10"};
    private Random _rnd = new Random();

    private int _waveIndex = 0;
    
    public GameObject[] SpawnLocations
    {
        get { return _spawnLocations; }
        set { _spawnLocations = value; }
    }

    public int EnemyDied
    {
        get { return _enemyDied; }
        set { _enemyDied = value; }
    }

    private void Start()
    {
        _spawnLocations = GameObject.FindGameObjectsWithTag("EnemySpawn");
    }

    
    private void Update () {
        if (_spawnMode && _enemySpawned < _enemiesPerWave)
        {
            Spawn();
        }
        else if (_enemyDied == _enemiesPerWave)
        {
            _spawnMode = false;
            _enemySpawned = 0;
            _enemyDied = 0;
            _canSpawn = true;
            if (_waveIndex == _enemyTypes.Length) return;
            UIManager.Instance.NextWaveBtn.transform.gameObject.SetActive(true);
            if (UIManager.Instance.Wave == 9)
            {
                UIManager.Instance.NextWaveBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Final Wave";
            }
        }
	}

    public void NextWave()
    {
        Debug.Log("NEXT WAVE");
        _waveIndex++;
        UIManager.Instance.Wave = _waveIndex;
        UIManager.Instance.NextWaveBtn.transform.gameObject.SetActive(false);
        GManager.Instance.BuildMode = false;
        _spawnMode = true;

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
        else
        {
            foreach (var spawnLocation in _spawnLocations)
            {
                GameObject enemy;
                if (_waveIndex == 10)
                {
                    enemy = ObjectPool.Instance.GetObject(_enemyTypes[_rnd.Next(10)]);
                }
                else
                {
                    enemy = ObjectPool.Instance.GetObject(_enemyTypes[_waveIndex-1]);
                }
                enemy.transform.position = spawnLocation.transform.position;
                _enemySpawned++;
                _canSpawn = false;
            }
        }
    }
}
