using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Pathfinding;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// GManager is a part of the Singleton hierarchy and can be used by other scripts through the Singleton script
public class GManager : Singleton<GManager> {

    // A boolean to change from combat and build mode (used by child aswell)
    private bool _buildMode = false;
    
    // USED FOR RAPID BUILDING
    private string _towerToBuild;
    
    // SELECTED TOWER
    GameObject _tower = null;
    
    // CHECK IF GAME IS STARTED OR PAUSED
    private bool _gameStarted = false;
    private bool _paused = false;
    
    // TIMER FOR RAPID BUILDING
    private bool _canBuild = true;
    private float _buildTimer, _buildCooldown = 0.05f;

    // getter for bool buildmode
    public bool BuildMode
    {
        get { return _buildMode; }
        set { _buildMode = value; }
    }
    
    public string TowerToBuild
    {
        get { return _towerToBuild; }
        set { _towerToBuild = value; }
    }
    
    public bool GameStarted
    {
        get { return _gameStarted; }
        set { _gameStarted = value; }
    }
    
    public bool Paused
    {
        get { return _paused; }
        set { _paused = value; }
    }

    
    // LIMITS THE FPS TO 144
    private void Awake()
    {
        Application.targetFrameRate = 144;
    }


    // Update is called once per frame
    private void LateUpdate()
    {
        // TOWER CODE
        TowerCode();
        Game();
        
        if (UIManager.Instance.Life <= 0)
        {
            UIManager.Instance.LoseScreenFade();
        }

    }

    private void Game()
    {
        // CHECKS IF THE GAME HAS STARTED
        if (_gameStarted)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                UIManager.Instance.Pause();
                UIManager.Instance.CloseHelp();
            }

            // CHECKS IF THE GAME IS PAUSED
            if (_paused) return;
            // starts buildingmode for cannontower
            if (Input.GetKeyUp(KeyCode.V))
            {
                if (!(UIManager.Instance.Currency - TowerManager.Instance.GetCannonTowerCost < 0))
                {
                    _buildMode = true;
                    TowerManager.Instance.CurrentTower = null;
                    UIManager.Instance.TowerStatsUi.SetActive(false);
                    _towerToBuild = "CanonTower";
                    BuildingMode.Instance.TowerType = _towerToBuild;
                }
            }
        
            // starts buildingmode for BasicTower
            if (Input.GetKeyUp(KeyCode.B))
            {
                if (!(UIManager.Instance.Currency - TowerManager.Instance.GetBasicTowerCost < 0))
                {
                    _buildMode = true;
                    TowerManager.Instance.CurrentTower = null;
                    UIManager.Instance.TowerStatsUi.SetActive(false);
                    _towerToBuild = "BasicTower";
                    BuildingMode.Instance.TowerType = _towerToBuild;
                }
            }
        }
    }

    private void TowerCode()
    {
        // checking if buildmode is false or true
        if (BuildMode)
        {
            // CONTINUOUS TOWER PLACEMENT
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    switch (_towerToBuild)
                    {
                        case "BasicTower":
                            if (!(UIManager.Instance.Currency - TowerManager.Instance.GetBasicTowerCost < 0))
                            {
                                TowerManager.Instance.PlaceTower();
                                _buildMode = true;
                                BuildingMode.Instance.TowerType = _towerToBuild;
                            }
                            else
                            {
                                _buildMode = false;
                            }
                            break;
                        
                        case "CanonTower":
                            if (!(UIManager.Instance.Currency - TowerManager.Instance.GetCannonTowerCost < 0))
                            {
                                TowerManager.Instance.PlaceTower();
                                _buildMode = true;
                                BuildingMode.Instance.TowerType = _towerToBuild;
                            }
                            else
                            {
                                _buildMode = false;
                            }
                            break;
                    }
                }
                
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _buildMode = false;
            }
            // END OF CONTINUOUS TOWER PLACEMENT
            
            if (Input.GetKey(KeyCode.LeftShift)) return;
            if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Escape))
            {
                BuildMode = false;
            }
            TowerManager.Instance.CurrentTower = null;
            // check if the user clicks the left mousebutton
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    TowerManager.Instance.PlaceTower();
                }
            }
        }
        
        // Select tower
        else if (!_paused)
        {
            
            if (Input.GetKey(KeyCode.Space))
            {
                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    SelectTower();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;
               DeselectTower();    
            }
        }
    }

    private void SelectTower()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            _tower = TowerManager.Instance.GetTower();
            if(_tower != null)
            {
                TowerManager.Instance.CurrentTower = _tower;
                UIManager.Instance.SetTowerStats(_tower.GetComponent<TowerController>());
                UIManager.Instance.TowerStatsUi.SetActive(true);
                UIManager.Instance.SelectTower();
            }
            else
            {
                TowerManager.Instance.CurrentTower = null;
                UIManager.Instance.TowerStatsUi.SetActive(false);
            }
        }
    }

    public void DeselectTower()
    {
        if (_tower != null)
        {
            TowerManager.Instance.CurrentTower = null;
            UIManager.Instance.TowerStatsUi.SetActive(false);
        }
    }

    public RaycastHit2D[] GetMouseCast()
    {
        RaycastHit2D[] ray = Physics2D.RaycastAll(GetMousePos(), new Vector2(0, 0), 0F);

        return ray;
    }

    public Vector2 GetMousePos()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return mousePosition;
    }
    
}
