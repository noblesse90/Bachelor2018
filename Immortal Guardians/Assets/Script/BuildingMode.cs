using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMode : MonoBehaviour {

    // variable to hold on the camera
    [SerializeField] private Camera camera;

    // variable for the gameobjects spriterenderer
    SpriteRenderer sprite;

    // bool variable to check buildmode from GManager
    private bool _BuildMode;


    private void Start()
    {
        // initiate sprite
        sprite = GetComponent<SpriteRenderer>();
        // makes the sprite invisible at the start of the game
        sprite.enabled = false;
        // gets the current state of buildmode from GManager
        _BuildMode = GManager.Instance.BuildMode;
    }

    // Update is called once per frame
    void Update () {

        // buildmode if test
        if (Input.GetKeyUp(KeyCode.B))
        {
            // checks the current buildmode state
            _BuildMode = GManager.Instance.BuildMode;
            if (_BuildMode == false)
            {
                sprite.enabled = true;
            }
            else
            {
                sprite.enabled = false;
            }
                
        }

        // checking if buildmode is true or false
        if (sprite.enabled)
        {
            FollowMouseHover();
        }
        
	}

    // The tower icon is following the mouse
    private void FollowMouseHover()
    {
        transform.position = camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(Mathf.Round(transform.position.x + .5f), Mathf.Round(transform.position.y + .5f)) ;
    }
}
