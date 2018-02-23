using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController> {

    // Int variable to controll speed and can be changeable through the inspector (SerializeField)
    [SerializeField] private int _speed = 10;

    // Rigidbody variable to hold on a rigidbodody component
    private Rigidbody2D _rb;

    // vector2 variable that controlls direction
    private Vector2 _direction;

	// Use this for initialization
	private void Start () {
        // get the gameobjects rigidbody component and freezes the rotation of the object
        _rb = GetComponent<Rigidbody2D>();

        _rb.freezeRotation = true;
	}
	
	// Update is called once per frame
	private void FixedUpdate () {
        // gets an input from the keyboard
        GetInput();
        // move the gameobject according to the keypresses
        Move();

    }

    private void Move()
    {
        transform.Translate(_direction * Time.deltaTime * _speed);
    }

    private void GetInput()
    {
        // resets the direction
        _direction = Vector2.zero;

        // checks if the user presses w, a, s or d and acts accordingly
        if (Input.GetKey(KeyCode.W))
        {
            _direction += Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _direction += Vector2.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _direction += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _direction += Vector2.right;
        }
    }
}
