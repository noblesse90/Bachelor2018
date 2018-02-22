using UnityEngine;
using Pathfinding;

// GManager is a part of the Singleton hierarchy and can be used by other scripts through the Singleton script
public class GManager : Singleton<GManager> {

    // variable that holds the enemy destination and spawns
    private GameObject _Destination;
    private GameObject[] _Spawn;

    // variable that holds a camera object
    [SerializeField] private Camera camera;
    
    // holds gameobjects
    [SerializeField] private GameObject _towerprefab;
    [SerializeField] private GameObject _sprite;

    // hold a vector3 position (x,y,z)
    private Vector3 pos;

    // A boolean to change from combat and build mode (used by child aswell)
    private bool _BuildMode = false;


    // getter for bool buildmode
    public bool BuildMode
    {
        get
        {
            return _BuildMode;
        }
    }

    void Start()
    {
        _Destination = GameObject.FindGameObjectWithTag("EnemyDestination");
        _Spawn = GameObject.FindGameObjectsWithTag("EnemySpawn");
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            
            if(BuildMode)
            {
                _BuildMode = false;
            }
            else
            {
                _BuildMode = true;
            }
            
        }

        // checking if buildmode is false or true
        if (BuildMode)
        {       
            // check if the user clicks the left mousebutton
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                placeTower();
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            _BuildMode = false;
        }
        
        
    }

    public void placeTower()
    {
        Vector2 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        if (!isEmpty(mousePosition))
        {
            _BuildMode = false;
            return;
        }
        
        mousePosition = new Vector2(Mathf.Round(mousePosition.x + .5f), Mathf.Round(mousePosition.y + .5f));
        // Instantiates (spawns) the tower prefab on the current position without altering its rotation
        // Instansiate(Gameobject, Vector3, Quaternion(rotation))
        GameObject tower = Instantiate(_towerprefab, mousePosition, Quaternion.identity);
        // gets the spriterenderer of the tower prefab and changes the sortingOrder according to its y position
        // this will make the tower infront always visible compared to the tower behind
        tower.GetComponent<SpriteRenderer>().sortingOrder = (int)Mathf.Round(pos.y)*-1;

        // gets the current position and adds the y value to place the sprite in the center
        Vector3 v = mousePosition;
        v.y += 2;
        // Instantiates the sprite prefab (image of the tower) and makes it a child of the tower that was made
        // Instantiate (GameObject, Vector3, Quaternion(rotation), Parent)
        GameObject sprite = Instantiate(_sprite, v, Quaternion.identity, tower.transform);

        // gets the spriterenderer of the sprite prefab and changes the sorting order according to its y position
        sprite.GetComponent<SpriteRenderer>().sortingOrder = (int)Mathf.Round(pos.y)*-1;

        if (checkValidTower(tower) != null)
        {
            // TODO subtract players currency
            Debug.Log("rip money");
        }
        //_BuildMode = false;
        
    }

    private bool isEmpty(Vector2 mousePos)
    {
        bool b = true;
        RaycastHit2D[] rayCenter, rayUp, rayRight, rayCorner;
        rayCenter = Physics2D.RaycastAll(mousePos, new Vector2(0,0) , 0F);
        mousePos.y += 1;
        rayUp = Physics2D.RaycastAll(mousePos, new Vector2(0, 0), 0F);
        mousePos.x += 1;
        rayCorner = Physics2D.RaycastAll(mousePos, new Vector2(0, 0), 0F);
        mousePos.y += -1;
        rayRight = Physics2D.RaycastAll(mousePos, new Vector2(0, 0), 0F);

        foreach (RaycastHit2D r in rayCenter)
        {
            if (r.collider.tag == "Tower" || r.collider.tag == "Obstacle")
            {
                Debug.Log("TOWER: 0.0");
                b = false;
            }
        }
        
        foreach (RaycastHit2D r in rayUp)
        {
            if (r.collider.tag == "Tower" || r.collider.tag == "Obstacle")
            {
                Debug.Log("TOWER: 1.0");
                b = false;
            }
        }

        foreach (RaycastHit2D r in rayCorner)
        {
            if (r.collider.tag == "Tower" || r.collider.tag == "Obstacle")
            {
                Debug.Log("TOWER: 1.1");
                b = false;
            }
        }

        foreach (RaycastHit2D r in rayRight)
        {
            if (r.collider.tag == "Tower" || r.collider.tag == "Obstacle")
            {
                Debug.Log("TOWER: 0.1");
                b = false;
            }
        }

        return b;
    }

    private GameObject checkValidTower(GameObject tower)
    {
        // check if the list is empty (no spawnpoint nodes)
        if (_Spawn != null && _Destination != null)
        {

            // gets the bounding volume of the towers renderer to make rough approximations about the tower's location
            var towerBox = new GraphUpdateObject(tower.GetComponent<BoxCollider2D>().bounds);

            // making the destination node
            var goalNode = AstarPath.active.GetNearest(_Destination.transform.position).node;

            // Making spawnpoints nodes and check them to see if there's a path available
            foreach (GameObject s in _Spawn)
            {
                var spawnPointNode = AstarPath.active.GetNearest(s.transform.position).node;

                if (!GraphUpdateUtilities.UpdateGraphsNoBlock(towerBox, spawnPointNode, goalNode, false))
                {
                    Destroy(tower);
                    return null;
                }
            }
        }
        else
        {
            Debug.Log("NO SPAWNS");
        }

        return tower;
    }

}
