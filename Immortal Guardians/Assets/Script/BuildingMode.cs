using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Activates when player chooses a tower to build and controlls the hover icon

public class BuildingMode : Singleton<BuildingMode> {
    
    [Header("TowerPrefabs")]
    [SerializeField] private GameObject _basicTowerPrefabHoverIcon;
    [SerializeField] private GameObject _canonTowerPrefabHoverIcon;
    [SerializeField] private GameObject _iceTowerPrefabHoverIcon;
    
    [Header("TowerPrefabs")]
    [SerializeField] private Sprite _basicTowerGreen;
    [SerializeField] private Sprite _basicTowerRed;
    [SerializeField] private Sprite _cannonTowerGreen;
    [SerializeField] private Sprite _cannonTowerRed;
    [SerializeField] private Sprite _iceTowerGreen;
    [SerializeField] private Sprite _iceTowerRed;

    [Header("RangeCircleSprites")] 
    [SerializeField] private GameObject _basicTowerGreenCricle;
    [SerializeField] private GameObject _basicTowerRedCircle;
    [SerializeField] private GameObject _cannonTowerGreenCircle;
    [SerializeField] private GameObject _cannonTowerRedCircle;
    [SerializeField] private GameObject _iceTowerGreenCircle;
    [SerializeField] private GameObject _iceTowerRedCircle;

    private GameObject _towerSpriteCircleActive;
    private GameObject _towerSpriteCircleDeactive;

    private Sprite _towerSpriteActive;
    private Sprite _towerSpriteDeactive;

    private string _towerType;

    private GameObject _tower;


    // bool variable to check buildmode from GManager
    private bool _buildMode;


    public string TowerType
    {
        get { return _towerType; }
        set { _towerType = value; }
    }

    public GameObject Tower
    {
        get { return _tower; }
        set { _tower = value; }
    }

    private void Start()
    {
        _basicTowerPrefabHoverIcon.SetActive(false);
        _canonTowerPrefabHoverIcon.SetActive(false);
        _iceTowerPrefabHoverIcon.SetActive(false);
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

        if (_tower != null)
        {
            _tower.transform.position = pos;
            if (TowerManager.Instance.IsEmpty(pos))
            {
                _towerSpriteCircleActive.SetActive(true);
                _towerSpriteCircleDeactive.SetActive(false);
                _tower.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _towerSpriteActive;
            }
            else
            {
                _towerSpriteCircleActive.SetActive(false);
                _towerSpriteCircleDeactive.SetActive(true);
                _tower.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _towerSpriteDeactive;
            }
        }
        else
        {
            SetTower(pos);
            if (TowerManager.Instance.IsEmpty(pos))
            {
                _towerSpriteCircleActive.SetActive(true);
                _towerSpriteCircleDeactive.SetActive(false);
                _tower.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _towerSpriteActive;
            }
            else
            {
                _towerSpriteCircleActive.SetActive(false);
                _towerSpriteCircleDeactive.SetActive(true);
                _tower.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _towerSpriteDeactive;
            }
        }
        
        

    }

    private void SetTower(Vector2 pos)
    {
        switch (_towerType)
        {
            case "BasicTower":
                _tower = _basicTowerPrefabHoverIcon;
                _tower.transform.position = pos;
                _tower.SetActive(true);
                _towerSpriteCircleActive = _basicTowerGreenCricle;
                _towerSpriteCircleDeactive = _basicTowerRedCircle;
                _towerSpriteActive = _basicTowerGreen;
                _towerSpriteDeactive = _basicTowerRed;
                TowerManager.Instance.Towerprefab = TowerManager.Instance.BasicTowerPrefab;
                break;

            case "CanonTower":
                _tower = _canonTowerPrefabHoverIcon;
                _tower.transform.position = pos;
                _tower.SetActive(true);
                _towerSpriteCircleActive = _cannonTowerGreenCircle;
                _towerSpriteCircleDeactive = _cannonTowerRedCircle;
                _towerSpriteActive = _cannonTowerGreen;
                _towerSpriteDeactive = _cannonTowerRed;
                TowerManager.Instance.Towerprefab = TowerManager.Instance.CanonTowerPrefab;
                break;
            
            case "IceTower":
                _tower = _iceTowerPrefabHoverIcon;
                _tower.transform.position = pos;
                _tower.SetActive(true);
                _towerSpriteCircleActive = _iceTowerGreenCircle;
                _towerSpriteCircleDeactive = _iceTowerRedCircle;
                _towerSpriteActive = _iceTowerGreen;
                _towerSpriteDeactive = _iceTowerRed;
                TowerManager.Instance.Towerprefab = TowerManager.Instance.IceTowerPrefab;
                break;
        }
    }
}
