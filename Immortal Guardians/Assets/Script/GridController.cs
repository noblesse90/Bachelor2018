using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour {

    private GridLayout gl;
    private Tilemap tm;

    [SerializeField] private Camera camera;

    // Use this for initialization
    void Start () {
        gl = transform.parent.GetComponent<GridLayout>();
        tm = GetComponent<Tilemap>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);
        pos.x += 1;
        pos.y += 3;
        Vector3Int cellPosition = gl.WorldToCell(pos);
        TileBase tile = null;

        if (Input.GetKey(KeyCode.E))
        {
            tile = tm.GetTile(cellPosition);
            Debug.Log(tm.GetCellCenterLocal(cellPosition));
        }

        if (Input.GetKey(KeyCode.R))
        {
            //if(tile != null)
            tm.SetTile(cellPosition, tile);
        }
    }

    private void OnMouseUp()
    {
        Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);
        pos.x += 1;
        pos.y += 3;
        Vector3Int cellPosition = gl.WorldToCell(pos);
        
        Debug.Log(tm.GetCellCenterLocal(cellPosition));
        tm.GetTile(cellPosition);
        Debug.Log(tm.HasTile(cellPosition));
    }
}
