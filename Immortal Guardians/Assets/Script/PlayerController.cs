using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController> {

    // Int variable to controll speed and can be changeable through the inspector (SerializeField)
    [SerializeField] private int speed = 10;

    // Rigidbody variable to hold on a rigidbodody component
    private Rigidbody2D rb;

    // vector2 variable that controlls direction
    private Vector2 direction;

	// Use this for initialization
	void Start () {
        // get the gameobjects rigidbody component and freezes the rotation of the object
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
        // gets an input from the keyboard
        getInput();
        // move the gameobject according to the keypresses
        move();

    }

    public void move()
    {
        // check the maximum velocity magnitude
        if (rb.velocity.magnitude > speed)
        {
            // normalizes the speed of the magnitude is too big
            rb.velocity = rb.velocity.normalized * Time.deltaTime * speed;
        }
        else
        {
            // applies movement to the object
            transform.Translate(direction * Time.deltaTime * speed);
        }
    }

    public void getInput()
    {
        // resets the direction
        direction = Vector2.zero;

        // checks if the user presses w, a, s or d and acts accordingly
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
    }
}
