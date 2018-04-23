using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = System.Random;

public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] private GameObject _enemy;

    [SerializeField] private GameObject _path1;
    [SerializeField] private GameObject _path2;
    [SerializeField] private GameObject _path3;

    private int _waveCount = 10;
    
    [SerializeField] private List<Enemy> _enemies;

    private bool _spawnMode = false;

    private List<GameObject> _spawnLocations = new List<GameObject>();
    private int _enemiesPerWave = 10;
    private int _enemyDied = 0;
    private int _enemySpawned = 0;
    
    
    private bool _canSpawn = true, _bossSpawn = true;
    private float _spawnTimer;
    private float _cooldownTimer = 1;


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
    
    // LIGHTNING SWORD
    private GameObject _lightningSword = null;

    public GameObject LightningSword
    {
        get { return _lightningSword; }
        set { _lightningSword = value; }
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
        
        // SPAWNS ENEMIES IF THE WAVE HAS STARTED
        if (_spawnMode && _enemySpawned < _enemiesPerWave)
        {
            Spawn();
        }
        // CHECKS WHEN THE LAST ENEMY DIES
        else if (_enemyDied == _enemiesPerWave)
        {
            // RESETS VALUES
            _spawnMode = false;
            _enemySpawned = 0;
            _enemyDied = 0;
            _canSpawn = true;
            _bossSpawn = true;
            
            // DEACTIVATE TOGGLE ABILITIES
            PlayerController.Instance.MultishotBool = false;
            PlayerController.Instance.OrbitingSwordBool = false;
            
            // UPDATE ENEMY COUNTER
            UIManager.Instance.EnemyCount(_enemySpawned, _enemyDied);
            
            // CHECKS IF THERES A NEXT WAVE
            if (_waveIndex == _waveCount)
            {
                StartCoroutine(UIManager.Instance.WinScreenFade());
                return;
            }
            
            // ACTIVATES NEXT WAVE BUTTON
            UIManager.Instance.NextWaveBtn.transform.gameObject.SetActive(true);
            // MINIMAP ICONS
            _path1.SetActive(true);
            _spawnLocations[1].transform.GetChild(0).gameObject.SetActive(true);
            
            
            
            // SETS THE TEXT FOR WAVE BUTTON
            switch (_waveIndex)
            {
                case 2:
                    UIManager.Instance.NextWaveBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Boss Wave";
                    UIManager.Instance.NextWaveBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1,0.5f,0);
                    break;
                
                case 6:
                    UIManager.Instance.NextWaveBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Boss Wave";
                    UIManager.Instance.NextWaveBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1,0.5f,0);
                    break;
                
                case 9:
                    UIManager.Instance.NextWaveBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Final Wave";
                    UIManager.Instance.NextWaveBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1,0,0);
                    break;
                
                default: 
                    UIManager.Instance.NextWaveBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next Wave";
                    UIManager.Instance.NextWaveBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1,1,1);
                    break;
            }
            
            // ACTIVATES MINIMAP ICONS
            if (_waveIndex >= 3)
            {
                _path2.SetActive(true);
                _spawnLocations[0].transform.GetChild(0).gameObject.SetActive(true);
                _enemiesPerWave = 20;
            }
            
            
            // ACTIVATES MINIMAP ICONS
            if (_waveIndex >= 7)
            {
                _path3.SetActive(true);
                _spawnLocations[2].transform.GetChild(0).gameObject.SetActive(true);
                _enemiesPerWave = 30;
            }
            
            
            
            // DEACTIVATES UNDO TOWER BUTTON
            TowerManager.Instance.LastTower = null;
            UIManager.Instance.UndoTower.interactable = false;
            
            // BOOST PLAYER SPEED WHILE WAVE IS NOT ACTIVE
            PlayerController.Instance.Speed = 25;
            
            // ACTIVATES BUILD PHASE
            UIManager.Instance.SetGrid(true);
            
            // DESTROYS LIGHTNING SWORD
            if (_lightningSword != null)
            {
                _lightningSword.GetComponent<LightningSword>().DestroySword();
            }
        }
        
        // UPDATES THE UI ENEMY COUNT IF ENEMIES HAVE SPAWNED
        if (_enemySpawned > 0) UIManager.Instance.EnemyCount(_enemySpawned, _enemyDied);
        
        // CHECKS IF BOSS WAVE AND SPAWNS A BOSS
        if (_enemySpawned == _enemiesPerWave && (_waveIndex == 3 || _waveIndex == 7 || _waveIndex == 10) && _bossSpawn)
        {
            _spawnMode = false;
            _enemiesPerWave++;
            StartCoroutine("SpawnBossesFirstMap");
            _bossSpawn = false;
        }
    }
    
    // ACTIVATES NEXT WAVE
    public void NextWave()
    {
        Debug.Log("NEXT WAVE");
        _waveIndex++;
        UIManager.Instance.Wave = _waveIndex;
        UIManager.Instance.NextWaveBtn.transform.gameObject.SetActive(false);
        UIManager.Instance.SetGrid(false);
        GManager.Instance.BuildMode = false;
        
        _path1.SetActive(false);
        _path2.SetActive(false);
        _path3.SetActive(false);
        _spawnLocations[0].transform.GetChild(0).gameObject.SetActive(false);
        _spawnLocations[1].transform.GetChild(0).gameObject.SetActive(false);
        _spawnLocations[2].transform.GetChild(0).gameObject.SetActive(false);
        
        _spawnMode = true;
        
        // DEACTIVATES UNDO TOWER BUTTON
        TowerManager.Instance.LastTower = null;
        UIManager.Instance.UndoTower.interactable = false;
        
        // SETS THE PLAYER SPEED TO NORMAL AFTER WAVE HAVE STARTED
        PlayerController.Instance.Speed = 10;
        PlayerController.Instance.GainDmg(10);

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
            
            
            FirstMapSpawn();
        }
    }

    private void FirstMapSpawn()
    {
        // stops enemies from spawning after the number of enemies per wave is reached
        if (_enemySpawned == _enemiesPerWave)
        {
            _canSpawn = false;
            return;
        }
        
        GameObject enemy;
        var spawn1 = _spawnLocations[1];
        enemy = Instantiate(_enemy, spawn1.transform.position, Quaternion.identity);
        enemy.GetComponent<EnemyController>().InitializeStats(_enemies[_waveIndex-1]);
        _enemySpawned++;

        if (_enemySpawned == _enemiesPerWave)
        {
            _canSpawn = false;
            return;
        }
        
        if (_waveIndex > 3)
        {
            var spawn2 = _spawnLocations[0];
            enemy = Instantiate(_enemy, spawn2.transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyController>().InitializeStats(_enemies[_waveIndex-1]);
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
            enemy = Instantiate(_enemy, spawn3.transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyController>().InitializeStats(_enemies[_waveIndex-1]);
            _enemySpawned++;
        }
        
        _canSpawn = false;
    }

    IEnumerator SpawnBossesFirstMap()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        GameObject enemy;
        var spawn1 = _spawnLocations[1];
        // SPAWN MINI BOSS ON WAVE 3 
        if (_waveIndex == 3)
        {
            enemy = Instantiate(_enemy, spawn1.transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyController>().InitializeStats(_enemies[10]);
            enemy.transform.localScale = new Vector2(1.5f,1.5f);
            _enemySpawned++;
        }
        
        // SPAWN MINI BOSS ON WAVE 7
        if (_waveIndex == 7)
        {
            enemy = Instantiate(_enemy, spawn1.transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyController>().InitializeStats(_enemies[11]);
            enemy.transform.localScale = new Vector2(1.5f,1.5f);
            _enemySpawned++;
        }
        
        // SPAWN FINAL BOSS ON WAVE 10
        if (_waveIndex == 10)
        {
            enemy = Instantiate(_enemy, spawn1.transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyController>().InitializeStats(_enemies[12]);
            enemy.transform.localScale = new Vector2(1.5f,1.5f);
            _enemySpawned++;
        }

    }
}
