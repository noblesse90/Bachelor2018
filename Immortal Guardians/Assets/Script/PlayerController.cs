using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int speed = 3;

    public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {

        var x = Input.GetAxis("Horizontal") * speed;
        var y = Input.GetAxis("Vertical") * speed;

        Vector2 movement = new Vector2(x, y);

        
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
        else
        {
            rb.velocity = movement;
        }



    }
}
