using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;




// OBSOLETE CODE


public class pathfinding : MonoBehaviour {
    GameObject _Destination;
    Seeker _seeker;
    Vector2 _end;
    Vector2 _start;

    private bool search;


    // Use this for initialization
    void Start () {

        _Destination = GameObject.FindGameObjectWithTag("EnemyDestination");

        _start = transform.position;
        _end = _Destination.transform.position;
       

        Debug.Log(_start);
        Debug.Log(_end);
        
        
        _seeker = GetComponent<Seeker>();
        _seeker.StartPath(_start, _end, OnPathComplete);

        
    }

    public void OnPathComplete(Path p)
    {
        if (p.error)
        {
            Debug.Log("NO PATH FOUND");
        }
        else
        {
            Debug.Log("PATH FOUND");
        }
    }
}
