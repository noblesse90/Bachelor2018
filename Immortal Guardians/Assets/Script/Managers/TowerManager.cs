using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TowerManager : Singleton<TowerManager> {

    // variable that holds the enemy destination and spawns
    private GameObject _Destination;
    private GameObject[] _Spawn;
    private GameObject currentTower;

    private int price;


    // holds gameobjects
    [SerializeField] private GameObject _towerprefab;
    [SerializeField] private GameObject _sprite;

    public GameObject CurrentTower
    {
        get
        {
            return currentTower;
        }

        set
        {
            currentTower = value;
        }
    }

    public int Price
    {
        get
        {
            return price;
        }

        set
        {
            price = value;
        }
    }

    // Use this for initialization
    void Start () {
        _Destination = GameObject.FindGameObjectWithTag("EnemyDestination");
        _Spawn = GameObject.FindGameObjectsWithTag("EnemySpawn");
    }
	
	// Update is called once per frame
	void Update () {
		if(currentTower != null)
        {
            switch (currentTower.name)
            {
                case "BasicTower(Clone)":
                    Debug.Log("BASIC TOWER");
                    break;
                default: break;

            }
        }
	}

    public void placeTower()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (!isEmpty(mousePosition))
        {
            GManager.Instance.BuildMode = false;
            return;
        }
        mousePosition = new Vector2(Mathf.Round(mousePosition.x + .5f), Mathf.Round(mousePosition.y + .5f));
        // Instantiates (spawns) the tower prefab on the current position without altering its rotation
        // Instansiate(Gameobject, Vector3, Quaternion(rotation))
        GameObject tower = Instantiate(_towerprefab, mousePosition, Quaternion.identity);
        // gets the spriterenderer of the tower prefab and changes the sortingOrder according to its y position
        // this will make the tower infront always visible compared to the tower behind
        tower.GetComponent<SpriteRenderer>().sortingOrder = (int)Mathf.Round(mousePosition.y) * -1;

        // gets the current position and adds the y value to place the sprite in the center
        Vector3 v = mousePosition;
        v.y += 1.5f;
        v.x += 0.04f;
        // Instantiates the sprite prefab (image of the tower) and makes it a child of the tower that was made
        // Instantiate (GameObject, Vector3, Quaternion(rotation), Parent)
        GameObject sprite = Instantiate(_sprite, v, Quaternion.identity, tower.transform);

        // gets the spriterenderer of the sprite prefab and changes the sorting order according to its y position
        sprite.GetComponent<SpriteRenderer>().sortingOrder = (int)Mathf.Round(mousePosition.y) * -1;

        // checks if the tower is valid and subtracts money
        if (checkValidTower(tower) != null)
        {
            tower.GetComponent<TowerController>().checkTowerType();
            UIManager.Instance.Currency -= tower.GetComponent<TowerController>().Price;
            Physics2D.IgnoreCollision(tower.GetComponent<BoxCollider2D>(), PlayerController.Instance.GetComponent<Collider2D>());
        }

        GManager.Instance.BuildMode = false;

    }

    private bool isEmpty(Vector2 mousePos)
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
            if (r.collider.CompareTag("Tower") || r.collider.CompareTag("Obstacle"))
            {
                Debug.Log("TOWER: 0.0");
                empty = false;
            }
        }

        foreach (RaycastHit2D r in rayUp)
        {
            if (r.collider.CompareTag("Tower") || r.collider.CompareTag("Obstacle"))
            {
                Debug.Log("TOWER: 1.0");
                empty = false;
            }
        }

        foreach (RaycastHit2D r in rayCorner)
        {
            if (r.collider.CompareTag("Tower") || r.collider.CompareTag("Obstacle"))
            {
                Debug.Log("TOWER: 1.1");
                empty = false;
            }
        }

        foreach (RaycastHit2D r in rayRight)
        {
            if (r.collider.CompareTag("Tower") || r.collider.CompareTag("Obstacle"))
            {
                Debug.Log("TOWER: 0.1");
                empty = false;
            }
        }

        return empty;
    }

    private GameObject checkValidTower(GameObject tower)
    {
        // check if the list is empty (no spawnpoint nodes)
        if (_Spawn != null && _Destination != null)
        {
            List<GraphNode> g = new List<GraphNode>();
            // gets the bounding volume of the towers renderer to make rough approximations about the tower's location
            var towerBox = new GraphUpdateObject(tower.GetComponent<BoxCollider2D>().bounds);

            // making the destination node
            var goalNode = AstarPath.active.GetNearest(_Destination.transform.position).node;
            g.Add(goalNode);
            // Making spawnpoints nodes and check them to see if there's a path available
            foreach (GameObject s in _Spawn)
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

    public void destroyTower()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] ray = Physics2D.RaycastAll(mousePosition, new Vector2(0, 0), 0F);
        foreach (RaycastHit2D r in ray)
        {
            if (r.collider.CompareTag("Tower"))
            {
                // Destroys the tower
                Destroy(r.transform.gameObject);
                // get the bounds of the currect object that was hit by the raycast (tower)
                Bounds b = r.transform.GetComponent<BoxCollider2D>().bounds;
                // expands the bounds since the nodes are further out
                b.Expand(2);
                // makes a graph update object
                GraphUpdateObject guo = new GraphUpdateObject(b);
                // updates the graph inside the given area of the bounds
                AstarPath.active.UpdateGraphs(guo);
            }
        }
    }

    public GameObject getTower()
    {
        GameObject tower = null;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] ray = Physics2D.RaycastAll(mousePosition, new Vector2(0, 0), 0F);
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
        if (currentTower != null)
        {
            currentTower.GetComponent<TowerController>().checkTowerType();
            price = currentTower.GetComponent<TowerController>().Price;
            UIManager.Instance.Currency += price;
            Debug.Log(price);
            Destroy(currentTower);
        }
    }

    public void UpgradeTower()
    {
        if(currentTower != null)
        {

        }
    }
}
