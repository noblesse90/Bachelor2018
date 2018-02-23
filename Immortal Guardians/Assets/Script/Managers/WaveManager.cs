using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : Singleton<WaveManager> {

    private bool _spawnMode = false;

    private int _enemiesPerWave = 10;
    private int _enemyCount = 0;
    private int _enemySpawned = 0;

    private int _waveIndex = 0;

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
        _enemyCount = 0;
        _enemySpawned = 0;

    }
}
