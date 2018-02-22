using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class pathfinding : MonoBehaviour {
    GameObject _Destination;
    Seeker _seeker;
    Vector2 _end;
    Vector2 _start;


    // Use this for initialization
    void Start () {

        _Destination = GameObject.FindGameObjectWithTag("EnemyDestination");

        _end = _Destination.transform.position;
        _start = transform.position;

        Debug.Log(_start);
        Debug.Log(_end);
        
        _seeker = GetComponent<Seeker>();
        _seeker.StartPath(_start, _end, OnPathComplete);

    }

    void Update()
    {
        
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
