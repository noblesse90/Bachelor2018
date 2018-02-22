using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour {
    GridLayout gl;
    Tilemap tm;
    public Camera camera;
    // Use this for initialization
    void Start () {
        gl = transform.parent.GetComponent<GridLayout>();
        tm = GetComponent<Tilemap>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnMouseUp()
    {
        Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = gl.WorldToCell(pos);
        Debug.Log(tm.GetCellCenterWorld(cellPosition));

        Debug.Log(tm.HasTile(cellPosition));
    }
}
