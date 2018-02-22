using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMode : MonoBehaviour {

    // variable to hold on the camera
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject _towerPrefab;

    private GameObject tower;


    // bool variable to check buildmode from GManager
    private bool _BuildMode;



    // Update is called once per frame
    void Update () {

        _BuildMode = GManager.Instance.BuildMode;
        
        if (_BuildMode)
        {
            followMouseHover();
        }
        else
        {
            Destroy(tower);
        }
        
	}

    // The tower icon is following the mouse
    private void followMouseHover()
    {
        Vector2 pos = camera.ScreenToWorldPoint(Input.mousePosition);
        pos = new Vector2(Mathf.Round(pos.x + .5f), Mathf.Round(pos.y + .5f));

        if(tower == null)
        {
            tower = Instantiate(_towerPrefab, pos, Quaternion.identity);
        }

        tower.transform.position = pos;
        
    }
}
