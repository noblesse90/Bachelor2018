using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GManager : MonoBehaviour {

    [SerializeField] private Camera camera;
    [SerializeField] private Tilemap tm;
    private Vector3 pos;

    RaycastHit2D[] hits;

    // Use this for initialization
    void Start()
    {
        tm = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.tag == "Background")
                {
                    Debug.Log(hits[i].collider.gameObject.transform.position);
                }
            }
        }

        if (Input.GetKey(KeyCode.E))
        {
            getPosition();
        }
    }

    public void getPosition()
    {
        Vector2 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mousePosition);
        hits = Physics2D.RaycastAll(mousePosition, new Vector2(0, 0), 0.01F);
    }
}
