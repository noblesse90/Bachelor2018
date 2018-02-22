using UnityEngine;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// GManager is a part of the Singleton hierarchy and can be used by other scripts through the Singleton script
public class GManager : Singleton<GManager> {

        // A boolean to change from combat and build mode (used by child aswell)
    private bool _BuildMode = false;

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

    // Update is called once per frame
    void Update()
    {
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
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                TowerManager.Instance.placeTower();
            }
        }
        // Select tower
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject tower = TowerManager.Instance.getTower();
            if(tower != null)
            {
                TowerManager.Instance.CurrentTower = tower;
                UIManager.Instance.setTowerStats(tower.GetComponent<TowerController>());
                UIManager.Instance.Ui.SetActive(true);
            }
            else
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    TowerManager.Instance.CurrentTower = null;
                    UIManager.Instance.Ui.SetActive(false);
                }
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

    public RaycastHit2D[] getMouseCast()
    {
        RaycastHit2D[] ray = Physics2D.RaycastAll(getMousePos(), new Vector2(0, 0), 0F);

        return ray;
    }

    public Vector2 getMousePos()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return mousePosition;
    }

    
}
