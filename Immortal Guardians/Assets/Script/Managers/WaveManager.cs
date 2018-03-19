using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = System.Random;

public class WaveManager : Singleton<WaveManager>
{

    [SerializeField] private GameObject _path1;
    [SerializeField] private GameObject _path2;
    [SerializeField] private GameObject _path3;

    private int _waveCount = 10;
    
    [SerializeField] private List<Enemy> _enemies;

    private bool _spawnMode = false;

    private List<GameObject> _spawnLocations = new List<GameObject>();
    private int _enemiesPerWave = 15;
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
    
    public bool SpawnMode
    {
        get { return _spawnMode; }
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
            
            // UPDATE ENEMY COUNTER
            UIManager.Instance.EnemyCount(_enemySpawned, _enemyDied);
            
            // CHECKS IF THERES A NEXT WAVE
            if (_waveIndex == _waveCount) return;
            UIManager.Instance.NextWaveBtn.transform.gameObject.SetActive(true);
            _path1.SetActive(true);
            _spawnLocations[1].transform.GetChild(0).gameObject.SetActive(true);
            if (_waveIndex >= 3)
            {
                _path2.SetActive(true);
                _spawnLocations[0].transform.GetChild(0).gameObject.SetActive(true);
                _enemiesPerWave = 30;
            }

            if (_waveIndex >= 7)
            {
                _path3.SetActive(true);
                _spawnLocations[2].transform.GetChild(0).gameObject.SetActive(true);
                _enemiesPerWave = 45;
            }
            if (_waveIndex == 9)
            {
                UIManager.Instance.NextWaveBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Final Wave";
            }
            
            // UNDO TOWER
            TowerManager.Instance.LastTower = null;
            UIManager.Instance.UndoTower.interactable = false;
            
            // BOOST PLAYER SPEED WHILE WAVE IS NOT ACTIVE
            PlayerController.Instance.Speed = 20;
        }
        
        if (_enemySpawned > 0) UIManager.Instance.EnemyCount(_enemySpawned, _enemyDied);
    }

    public void NextWave()
    {
        Debug.Log("NEXT WAVE");
        _waveIndex++;
        UIManager.Instance.Wave = _waveIndex;
        UIManager.Instance.NextWaveBtn.transform.gameObject.SetActive(false);
        GManager.Instance.BuildMode = false;
        
        _path1.SetActive(false);
        _path2.SetActive(false);
        _path3.SetActive(false);
        _spawnLocations[0].transform.GetChild(0).gameObject.SetActive(false);
        _spawnLocations[1].transform.GetChild(0).gameObject.SetActive(false);
        _spawnLocations[2].transform.GetChild(0).gameObject.SetActive(false);
        
        _spawnMode = true;
        
        // UNDO TOWER
        TowerManager.Instance.LastTower = null;
        UIManager.Instance.UndoTower.interactable = false;
        
        // SETS THE PLAYER SPEED TO NORMAL AFTER WAVE HAVE STARTED
        PlayerController.Instance.Speed = 10;
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
            if (_enemySpawned == _enemiesPerWave)
            {
                _canSpawn = false;
                return;
            }
            GameObject enemy;
            var spawn1 = _spawnLocations[1];
            enemy = ObjectPool.Instance.GetObject("Enemy");
            enemy.GetComponent<EnemyController>().InitializeStats(_enemies[_waveIndex-1]);
            enemy.transform.position = spawn1.transform.position;
            _enemySpawned++;

            if (_enemySpawned == _enemiesPerWave)
            {
                _canSpawn = false;
                return;
            }
            if (_waveIndex > 3)
            {
                var spawn2 = _spawnLocations[0];
                enemy = ObjectPool.Instance.GetObject("Enemy");
                enemy.GetComponent<EnemyController>().InitializeStats(_enemies[_waveIndex-1]);
                enemy.transform.position = spawn2.transform.position;
                _enemySpawned++;
            }

            if (_enemySpawned == _enemiesPerWave)
            {
                _canSpawn = false;
                return;
            }
            if (_waveIndex > 7)
            {
                var spawn3 = _spawnLocations[2];
                enemy = ObjectPool.Instance.GetObject("Enemy");
                enemy.GetComponent<EnemyController>().InitializeStats(_enemies[_waveIndex-1]);
                enemy.transform.position = spawn3.transform.position;
                _enemySpawned++;
            }
            
            _canSpawn = false;
            
        }
    }
}
