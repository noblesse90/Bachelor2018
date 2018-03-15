using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TowerManager : Singleton<TowerManager> {

    // variable that holds the enemy destination and spawns
    private GameObject _destination;
    private GameObject _currentTower;

    // holds gameobjects
    private GameObject _towerprefab;

    [SerializeField] private GameObject _basicTowerPrefab;
    [SerializeField] private GameObject _canonTowerPrefab;
    
    private const int BasicTowerCost = 10;
    private const int CannonTowerCost = 20;

    public GameObject CurrentTower
    {
        get { return _currentTower; }
        set { _currentTower = value; }
    }

    public GameObject Towerprefab
    {
        get { return _towerprefab; }
        set { _towerprefab = value; }
    }
    
    public GameObject BasicTowerPrefab
    {
        get { return _basicTowerPrefab; }
    }
 
    public GameObject CanonTowerPrefab
    {
        get { return _canonTowerPrefab; }
    }

    

    public int GetBasicTowerCost
    {
        get { return BasicTowerCost; }
    }

    public int GetCannonTowerCost
    {
        get { return CannonTowerCost; }
    }


    // Use this for initialization
    private void Start () {
        _destination = GameObject.FindGameObjectWithTag("EnemyDestination");
    }
	

    public void PlaceTower()
    {
        Vector2 mousePosition = GManager.Instance.GetMousePos();

        if (!IsEmpty(mousePosition))
        {
            GManager.Instance.BuildMode = false;
            return;
        }
        mousePosition = new Vector2(Mathf.Round(mousePosition.x + .5f), Mathf.Round(mousePosition.y + .5f));
        // Instantiates (spawns) the tower prefab on the current position without altering its rotation
        // Instansiate(Gameobject, Vector3, Quaternion(rotation))
        GameObject tower = Instantiate(Towerprefab, mousePosition, Quaternion.identity);
        // gets the spriterenderer of the tower prefab and changes the sortingOrder according to its y position
        // this will make the tower infront always visible compared to the tower behind
        int sortLayer = (int)Mathf.Round(mousePosition.y) * -1;
        tower.GetComponent<SpriteRenderer>().sortingOrder = sortLayer;
        // TowerSprite sort layer
        tower.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = sortLayer;
        // gets the spriterenderer of the sprite prefab and changes the sorting order according to its y position
        //tower.GetComponent<SpriteRenderer>().sortingOrder = (int)Mathf.Round(mousePosition.y) * -1;

        // checks if the tower is valid and subtracts money
        if (CheckValidTower(tower) != null)
        {
            UIManager.Instance.Currency -= tower.GetComponent<TowerController>().Price;
            Physics2D.IgnoreCollision(tower.GetComponent<BoxCollider2D>(), PlayerController.Instance.GetComponent<Collider2D>());
        }

        GManager.Instance.BuildMode = false;

    }

    private bool IsEmpty(Vector2 mousePos)
    {
        bool empty = true;
        RaycastHit2D[] rayCenter, rayUp, rayRight, rayCorner;
        rayCenter = Physics2D.RaycastAll(mousePos, new Vector2(0, 0), 0F);
        mousePos.y += 1;
        rayUp = Physics2D.RaycastAll(mousePos, new Vector2(0, 0), 0F);
        mousePos.x += 1;
        rayCorner = Physics2D.RaycastAll(mousePos, new Vector2(0, 0), 0F);
        mousePos.y += -1;
        rayRight = Physics2D.RaycastAll(mousePos, new Vector2(0, 0), 0F);

        foreach (RaycastHit2D r in rayCenter)
        {
            if (!r.collider.CompareTag("Tower") && !r.collider.CompareTag("Obstacle")) continue;
            Debug.Log("TOWER: 0.0");
            empty = false;
        }

        foreach (RaycastHit2D r in rayUp)
        {
            if (!r.collider.CompareTag("Tower") && !r.collider.CompareTag("Obstacle")) continue;
            Debug.Log("TOWER: 1.0");
            empty = false;
        }

        foreach (RaycastHit2D r in rayCorner)
        {
            if (!r.collider.CompareTag("Tower") && !r.collider.CompareTag("Obstacle")) continue;
            Debug.Log("TOWER: 1.1");
            empty = false;
        }

        foreach (RaycastHit2D r in rayRight)
        {
            if (!r.collider.CompareTag("Tower") && !r.collider.CompareTag("Obstacle")) continue;
            Debug.Log("TOWER: 0.1");
            empty = false;
        }

        return empty;
    }

    private GameObject CheckValidTower(GameObject tower)
    {
        // check if the list is empty (no spawnpoint nodes)
        if (WaveManager.Instance.SpawnLocations != null && _destination != null)
        {
            List<GraphNode> g = new List<GraphNode>();
            // gets the bounding volume of the towers renderer to make rough approximations about the tower's location
            var towerBox = new GraphUpdateObject(tower.GetComponent<BoxCollider2D>().bounds);

            // making the destination node
            var goalNode = AstarPath.active.GetNearest(_destination.transform.position).node;
            g.Add(goalNode);
            // Making spawnpoints nodes and check them to see if there's a path available
            foreach (GameObject s in WaveManager.Instance.SpawnLocations)
            {
                var spawnPointNode = AstarPath.active.GetNearest(s.transform.position).node;
                g.Add(spawnPointNode);
            }

            if (!GraphUpdateUtilities.UpdateGraphsNoBlock(towerBox, g, false))
            {
                Destroy(tower);
                return null;
            }


        }
        else
        {
            Debug.Log("NO SPAWNS");
        }

        return tower;
    }

    public void DestroyTower()
    {
        Destroy(_currentTower);
        // get the bounds of the currect object that was hit by the raycast (tower)
        Bounds b = _currentTower.transform.GetComponent<BoxCollider2D>().bounds;
        // expands the bounds since the nodes are further out
        b.Expand(2);
        // makes a graph update object
        GraphUpdateObject guo = new GraphUpdateObject(b);
        // updates the graph inside the given area of the bounds
        AstarPath.active.UpdateGraphs(guo);
    }

    public GameObject GetTower()
    {
        GameObject tower = null;
        RaycastHit2D[] ray = GManager.Instance.GetMouseCast();
        foreach (RaycastHit2D r in ray)
        {
            if (r.collider.CompareTag("Tower"))
            {
                tower = r.transform.gameObject;
            }
            else if (r.collider.CompareTag("TowerSprite"))
            {
                tower = r.transform.parent.gameObject;
            }
        }

        return tower;
    }

    public void SellTower()
    {
        if (_currentTower == null) return;
        
        UIManager.Instance.Currency += Mathf.FloorToInt(_currentTower.GetComponent<TowerController>().TotalPrice * 0.75f);
        UIManager.Instance.TowerStatsUi.SetActive(false);
        DestroyTower();
    }

    public void UpgradeTower()
    {
        if (_currentTower == null) return;
        TowerController tc = _currentTower.GetComponent <TowerController>();
        if (UIManager.Instance.Currency - tc.UpgradePrice < 0)
        {
            Debug.Log("Not enough money");
            return;
        }

        UIManager.Instance.Currency -= tc.UpgradePrice;
        tc.Upgrade();
        _currentTower.transform.GetChild(tc.Level).GetComponent<SpriteRenderer>().enabled = true;

        if (tc.Level <= 3) return;
        Debug.Log("MAX LEVEL");
        UIManager.Instance.UpgradeBtn.gameObject.SetActive(false);
    }
}
