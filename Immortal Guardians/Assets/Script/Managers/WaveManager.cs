using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : Singleton<WaveManager> {

    private int enemyCount = 0;
    private int enemySpawned = 0;

    private int waveIndex = 0;

    public int WaveIndex
    {
        get
        {
            return waveIndex;
        }

        set
        {
            waveIndex = value;
        }
    }

    public int EnemyCount
    {
        get
        {
            return enemyCount;
        }

        set
        {
            enemyCount = value;
        }
    }

    public int EnemySpawned
    {
        get
        {
            return enemySpawned;
        }

        set
        {
            enemySpawned = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UIManager.Instance.Wave = WaveIndex;
	}

    public void NextWave()
    {
        Debug.Log("NEXT WAVE");
        UIManager.Instance.Wave = waveIndex++;
        UIManager.Instance.NextWave.transform.gameObject.SetActive(false);
        GManager.Instance.BuildMode = false;
        GManager.Instance._SpawnMode = true;
        enemyCount = 0;
        enemySpawned = 0;

    }
}
