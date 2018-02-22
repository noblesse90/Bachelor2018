using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMode : Singleton<BuildingMode> {

    private GameObject _basicTowerPrefab;
    private GameObject _canonTowerPrefab;

    private string towerType;

    private GameObject tower;


    // bool variable to check buildmode from GManager
    private bool _BuildMode;

    public string TowerType
    {
        get
        {
            return towerType;
        }

        set
        {
            towerType = value;
        }
    }

    private void Start()
    {
        _basicTowerPrefab = GameObject.Find("BasicTowerHoverIcon");
        _basicTowerPrefab.SetActive(false);
        _canonTowerPrefab = GameObject.Find("CanonTowerHoverIcon");
        _canonTowerPrefab.SetActive(false);
    }


    // Update is called once per frame
    void Update () {

        _BuildMode = GManager.Instance.BuildMode;
        
        if (_BuildMode)
        {
            followMouseHover();
        }
        else
        {
            if(tower != null)
            {
                tower.SetActive(false);
            }         
        }
        
	}

    // The tower icon is following the mouse
    private void followMouseHover()
    {
        Vector2 pos = GManager.Instance.getMousePos();
        pos = new Vector2(Mathf.Round(pos.x + .5f), Mathf.Round(pos.y + .5f));

        if (tower == null)
        {
            setTower(pos);
        }
        else if (!tower.activeInHierarchy)
        {
            setTower(pos);
        }
        else
        {
            tower.transform.position = pos;
        }
    }

    private void setTower(Vector2 pos)
    {
        
        switch (towerType)
        {
            case "BasicTower":
                tower = _basicTowerPrefab;
                tower.transform.position = pos;
                tower.SetActive(true);
                TowerManager.Instance.Towerprefab = TowerManager.Instance.BasicTowerPrefab;
                break;

            case "CanonTower":
                tower = _canonTowerPrefab;
                tower.transform.position = pos;
                tower.SetActive(true);
                TowerManager.Instance.Towerprefab = TowerManager.Instance.CanonTowerPrefab;
                break;
        }
    }
}
