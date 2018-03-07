using UnityEngine;
using System.Collections.Generic;
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


    // Update is called once per frame
    private void Update()
    {
        // TOWER CODE
        TowerCode();
        
        
        
        // ------------------------ TEMPORARY CODE -----------------
        
        // resets the game
        if (Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }

        // quits the application (game)
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }

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
        
        
        // RAPID TOWER PLACEMENT
        if (BuildMode)
        {
            if (Input.GetKey(KeyCode.LeftShift))
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
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _buildMode = false;
            }
        }
    }

    private void TowerCode()
    {
        // checking if buildmode is false or true
        if (BuildMode)
        {
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
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject tower = TowerManager.Instance.GetTower();
            if(tower != null)
            {
                TowerManager.Instance.CurrentTower = tower;
                UIManager.Instance.SetTowerStats(tower.GetComponent<TowerController>());
                UIManager.Instance.TowerStatsUi.SetActive(true);
            }
            else
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    TowerManager.Instance.CurrentTower = null;
                    UIManager.Instance.TowerStatsUi.SetActive(false);
                }
            }
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
