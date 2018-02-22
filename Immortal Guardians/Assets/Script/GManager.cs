using UnityEngine;

// GManager is a part of the Singleton hierarchy and can be used by other scripts through the Singleton script
public class GManager : Singleton<GManager> {

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


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            
            if(BuildMode == false)
            {
                _BuildMode = true;
            }
            else
            {
                _BuildMode = false;
            }
        }

        // checking if buildmode is false or true
        if (BuildMode)
        {
            // check if the user clicks the left mousebutton
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                // gets the position of the mouse and places a gameobject down (tower with its sprite)
                getPosition();
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        
        
    }

    public void getPosition()
    {
        // converts the mouse position from 3d to 2d
        Vector2 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        // transforms the position of the gameobject to the mouseposition that's rounded up after adding .5f
        transform.position = new Vector2(Mathf.Round(mousePosition.x + .5f), Mathf.Round(mousePosition.y + .5f));
        // saves the gameobjects position in a vector2 variable
        Vector2 currentPosition = transform.position;

        // prints out to console for debugging purposes
        Debug.Log(currentPosition);
        // places a tower on current position
        placeTower(currentPosition);
    }

    public void placeTower(Vector2 pos)
    {
        // Instantiates (spawns) the tower prefab on the current position without altering its rotation
        // Instansiate(Gameobject, Vector3, Quaternion(rotation))
        GameObject tower = Instantiate(_towerprefab, transform.position, Quaternion.identity);
        // gets the spriterenderer of the tower prefab and changes the sortingOrder according to its y position
        // this will make the tower infront always visible compared to the tower behind
        tower.GetComponent<SpriteRenderer>().sortingOrder = (int)Mathf.Round(pos.y)*-1;

        // gets the current position and adds the y value to place the sprite in the center
        Vector3 v = transform.position;
        v.y += 2;
        // Instantiates the sprite prefab (image of the tower) and makes it a child of the tower that was made
        // Instantiate (GameObject, Vector3, Quaternion(rotation), Parent)
        GameObject sprite = Instantiate(_sprite, v, Quaternion.identity, tower.transform);

        // gets the spriterenderer of the sprite prefab and changes the sorting order according to its y position
        sprite.GetComponent<SpriteRenderer>().sortingOrder = (int)Mathf.Round(pos.y)*-1;

    }

}
