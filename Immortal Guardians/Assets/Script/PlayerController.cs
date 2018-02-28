using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class PlayerController : Singleton<PlayerController> {

    // Int variable to controll speed and can be changeable through the inspector (SerializeField)
    [SerializeField] private int _speed = 10;

    // Rigidbody variable to hold on a rigidbodody component
    private Rigidbody2D _rb;

    // vector2 variable that controlls direction
    private Vector2 _direction;
    
    // player transform directions
    private Transform _down, _up, _left, _right;

    private Animator _animatorDown, _animatorUp, _animatorLeft, _animatorRight;
    
    

	// Use this for initialization
	private void Start () {
        // get the gameobjects rigidbody component and freezes the rotation of the object
        _rb = GetComponent<Rigidbody2D>();

        _rb.freezeRotation = true;
	    
	    // player transform
	    _down = transform.GetChild(0);
	    _animatorDown = _down.gameObject.GetComponent<Animator>();
	    _up = transform.GetChild(1);
	    _animatorUp = _up.gameObject.GetComponent<Animator>();
	    _left = transform.GetChild(2);
	    _animatorLeft = _left.gameObject.GetComponent<Animator>();
	    _right = transform.GetChild(3);
		_animatorRight = _right.gameObject.GetComponent<Animator>();

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
            _up.gameObject.SetActive(true);
	        
	        //_animatorUp.SetTrigger("Walk");
            
            _left.gameObject.SetActive(false);
            _right.gameObject.SetActive(false);
            _down.gameObject.SetActive(false);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            _direction += Vector2.left;
            _left.gameObject.SetActive(true);
            
            _up.gameObject.SetActive(false);
            _right.gameObject.SetActive(false);
            _down.gameObject.SetActive(false);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            _direction += Vector2.down;
            _down.gameObject.SetActive(true);
	        _animatorDown.SetTrigger("Walk");
	        
            
            _left.gameObject.SetActive(false);
            _right.gameObject.SetActive(false);
            _up.gameObject.SetActive(false);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            _direction += Vector2.right;
            _right.gameObject.SetActive(true);
            
            _left.gameObject.SetActive(false);
            _up.gameObject.SetActive(false);
            _down.gameObject.SetActive(false);
        }

	    if (_direction.magnitude <= 0)
	    {
		    _animatorDown.SetBool("Idle", true);
		    Debug.Log(_direction);
	    }
	    else
	    {
		    _animatorDown.SetBool("Idle", false);
	    }
	    
    }
}
