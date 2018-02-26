using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : Singleton<WaveManager> {

    private bool _spawnMode = false;
    
    private GameObject[] _spawnLocations;

    

    private int _enemiesPerWave = 10;
    private int _enemyCount = 0;
    private int _enemySpawned = 0;

    private string[] _enemyTypes = {"Enemy01", "Enemy02"};
    

    private int _waveIndex = 0;
    
    public GameObject[] SpawnLocations
    {
        get { return _spawnLocations; }
        set { _spawnLocations = value; }
    }

    private int WaveIndex
    {
        get
        {
            return _waveIndex;
        }

        set
        {
            _waveIndex = value;
        }
    }

    public int EnemyCount
    {
        get
        {
            return _enemyCount;
        }

        set
        {
            _enemyCount = value;
        }
    }

    public int EnemySpawned
    {
        get
        {
            return _enemySpawned;
        }

        set
        {
            _enemySpawned = value;
        }
    }

    public int EnemiesPerWave
    {
        get
        {
            return _enemiesPerWave;
        }

        set
        {
            _enemiesPerWave = value;
        }
    }

    public bool SpawnMode
    {
        get
        {
            return _spawnMode;
        }

        set
        {
            _spawnMode = value;
        }
    }

    private void Awake()
    {
        _spawnLocations = GameObject.FindGameObjectsWithTag("EnemySpawn");
    }

    // Update is called once per frame
    private void Update () {
        UIManager.Instance.Wave = WaveIndex;
	}

    public void NextWave()
    {
        Debug.Log("NEXT WAVE");
        UIManager.Instance.Wave = _waveIndex++;
        UIManager.Instance.NextWaveBtn.transform.gameObject.SetActive(false);
        GManager.Instance.BuildMode = false;
        SpawnMode = true;

    }
}
