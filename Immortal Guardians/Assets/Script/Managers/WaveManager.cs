using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Random = System.Random;

public class WaveManager : Singleton<WaveManager> {

    private bool _spawnMode = false;
    
    private List<GameObject> _spawnLocations = new List<GameObject>();
    private int _enemiesPerWave = 30;
    private int _enemyDied = 0;
    private int _enemySpawned = 0;
    
    private bool _canSpawn = true;
    private float _spawnTimer;
    private float _cooldownTimer = 1;

    private string[] _enemyTypes = {"Enemy01", "Enemy02", "Enemy03", "Enemy04", "Enemy05", "Enemy06", "Enemy07", "Enemy08", "Enemy09", "Enemy10"};
    private Random _rnd = new Random();

    private int _waveIndex = 0;
    
    public List<GameObject> SpawnLocations
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
        GameObject spawnLocation = GameObject.FindGameObjectWithTag("EnemySpawn");
        foreach (Transform child in spawnLocation.transform)
        {
            _spawnLocations.Add(child.gameObject);
        }
        
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
            // TODO CHECK WHICH MAP IS ACTIVE
            GameObject enemy;
            var spawn1 = _spawnLocations[1];
            enemy = ObjectPool.Instance.GetObject(_enemyTypes[_waveIndex-1]);
            enemy.transform.position = spawn1.transform.position;
            _enemySpawned++;

            if (_waveIndex > 3)
            {
                var spawn2 = _spawnLocations[0];
                enemy = ObjectPool.Instance.GetObject(_enemyTypes[_waveIndex-1]);
                enemy.transform.position = spawn2.transform.position;
                _enemySpawned++;
            }

            if (_waveIndex > 7)
            {
                var spawn3 = _spawnLocations[2];
                enemy = ObjectPool.Instance.GetObject(_enemyTypes[_waveIndex-1]);
                enemy.transform.position = spawn3.transform.position;
                _enemySpawned++;
            }
            
            _canSpawn = false;
            
        }
    }
}
