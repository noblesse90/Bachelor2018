using UnityEngine;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// GManager is a part of the Singleton hierarchy and can be used by other scripts through the Singleton script
public class GManager : Singleton<GManager> {

    
    [SerializeField] private int totalSpawned;

    // A boolean to change from combat and build mode (used by child aswell)
    private bool _BuildMode = false;
    public bool _SpawnMode = false;


    // getter for bool buildmode
    public bool BuildMode
    {
        get
        {
            return _BuildMode;
        }

        set
        {
            _BuildMode = value;
        }
    }


    public int TotalSpawned
    {
        get
        {
            return totalSpawned;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // goes into buildmode
        if (Input.GetKeyUp(KeyCode.B))
        {
            
            if(BuildMode)
            {
                _BuildMode = false;
            }
            else
            {
                if (UIManager.Instance.Currency - 2 < 0)
                {
                    Debug.Log("Not enough money");
                }
                else
                {
                    _BuildMode = true;
                }
            }
            
        }

        // resets the game
        if (Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }

        // checking if buildmode is false or true
        if (BuildMode)
        {
            TowerManager.Instance.CurrentTower = null;
            // check if the user clicks the left mousebutton
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                TowerManager.Instance.placeTower();
               
            }
        }
        // Select tower
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            GameObject tower = TowerManager.Instance.getTower();
            if(tower != null)
            {
                Debug.Log("HIT");
                TowerManager.Instance.CurrentTower = tower;
            }
            else
            {
                TowerManager.Instance.CurrentTower = null;
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }

        // destroys a tower in the given mouse position
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            //_BuildMode = false;
            TowerManager.Instance.destroyTower();
        }
        
        
    }

    
}
