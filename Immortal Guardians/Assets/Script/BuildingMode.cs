using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMode : Singleton<BuildingMode> {

    private GameObject _basicTowerPrefab;
    private GameObject _canonTowerPrefab;

    private string _towerType;

    

    private GameObject _tower;


    // bool variable to check buildmode from GManager
    private bool _buildMode;


    public string TowerType
    {
        get { return _towerType; }
        set { _towerType = value; }
    }
    
    private void Start()
    {
        _basicTowerPrefab = GameObject.Find("BasicTowerHoverIcon");
        _basicTowerPrefab.SetActive(false);
        _canonTowerPrefab = GameObject.Find("CanonTowerHoverIcon");
        _canonTowerPrefab.SetActive(false);
    }


    // Update is called once per frame
    private void Update () {

        _buildMode = GManager.Instance.BuildMode;
        
        if (_buildMode)
        {
            FollowMouseHover();
        }
        else
        {
            if(_tower != null)
            {
                _tower.SetActive(false);
            }         
        }
        
	}

    // The tower icon is following the mouse
    private void FollowMouseHover()
    {
        Vector2 pos = GManager.Instance.GetMousePos();
        pos = new Vector2(Mathf.Round(pos.x + .5f), Mathf.Round(pos.y + .5f));

        if (_tower == null)
        {
            SetTower(pos);
        }
        else if (!_tower.activeInHierarchy)
        {
            SetTower(pos);
        }
        else
        {
            _tower.transform.position = pos;
        }
    }

    private void SetTower(Vector2 pos)
    {
        
        switch (_towerType)
        {
            case "BasicTower":
                _tower = _basicTowerPrefab;
                _tower.transform.position = pos;
                _tower.SetActive(true);
                TowerManager.Instance.Towerprefab = TowerManager.Instance.BasicTowerPrefab;
                break;

            case "CanonTower":
                _tower = _canonTowerPrefab;
                _tower.transform.position = pos;
                _tower.SetActive(true);
                TowerManager.Instance.Towerprefab = TowerManager.Instance.CanonTowerPrefab;
                break;
        }
    }
}
