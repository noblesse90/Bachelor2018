using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
/*
public class GridController : MonoBehaviour {

    RaycastHit2D[] hits;
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
        pos.z = 0;
        Vector3Int cellPosition = gl.LocalToCell(pos);
        TileBase tile = null;

        if (Input.GetKey(KeyCode.E))
        {
            
            //Debug.Log(pos);
            //Debug.Log(tm.GetCellCenterLocal(cellPosition));
        }

        if (Input.GetKey(KeyCode.R))
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.tag == "Tower")
                {
                    Debug.Log(hits[i].collider.gameObject.transform.position);
                }
            }
        }
    }

    private void OnMouseUp()
    {
        Vector2 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        hits = Physics2D.RaycastAll(mousePosition, new Vector2(0, 0), 0.01F);
        Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);

        Vector3Int cellPosition = gl.WorldToCell(pos);
        
        
        Debug.Log(tm.GetCellCenterLocal(cellPosition));
        //Debug.Log(tm.HasTile(cellPosition));
    }
}
*/