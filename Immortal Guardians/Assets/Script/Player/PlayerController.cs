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
	
	private string _class = "Ranged";
	
    

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
		_rb.velocity = _rb.velocity.normalized * _speed;

    }

	private void Update()
	{
		// gets an input from the keyboard
		GetInput();
		// Attacks
		AttackAnimation();
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

	    if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)))
	    {
		    if(Input.GetKey(KeyCode.S))
		    {
			    _direction += Vector2.left;
			    _direction += Vector2.down;
		    }

		    if (Input.GetKey(KeyCode.W))
		    {
			    _direction += Vector2.left;
			    _direction += Vector2.up;
		    }
		    
		    _left.gameObject.SetActive(true);
	        
		    _animatorLeft.SetBool("Walking", true);
            
		    _up.gameObject.SetActive(false);
		    _right.gameObject.SetActive(false);
		    _down.gameObject.SetActive(false);
		    return;
	    }
	    
	    if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)))
	    {
		    if(Input.GetKey(KeyCode.S))
		    {
			    _direction += Vector2.right;
			    _direction += Vector2.down;
		    }

		    if (Input.GetKey(KeyCode.W))
		    {
			    _direction += Vector2.right;
			    _direction += Vector2.up;
		    }
		    
		    _right.gameObject.SetActive(true);
	        
		    _animatorRight.SetBool("Walking", true);
            
		    _up.gameObject.SetActive(false);
		    _left.gameObject.SetActive(false);
		    _down.gameObject.SetActive(false);
		    return;
	    }
	    
        if (Input.GetKey(KeyCode.W))
        {
            _direction += Vector2.up;
            _up.gameObject.SetActive(true);
	        
	        _animatorUp.SetBool("Walking", true);
            
            _left.gameObject.SetActive(false);
            _right.gameObject.SetActive(false);
            _down.gameObject.SetActive(false);
        } 
        

        if (Input.GetKey(KeyCode.A))
        {
            _direction += Vector2.left;
            _left.gameObject.SetActive(true);
	        
	        _animatorLeft.SetBool("Walking", true);
            
            _up.gameObject.SetActive(false);
            _right.gameObject.SetActive(false);
            _down.gameObject.SetActive(false);
        }
        

        if (Input.GetKey(KeyCode.S))
        {
            _direction += Vector2.down;
            _down.gameObject.SetActive(true);
	        
	        _animatorDown.SetBool("Walking", true);
            
            _left.gameObject.SetActive(false);
            _right.gameObject.SetActive(false);
            _up.gameObject.SetActive(false);
	        
        }
        

        if (Input.GetKey(KeyCode.D))
        {
            _direction += Vector2.right;
            _right.gameObject.SetActive(true);
	        
	        _animatorRight.SetBool("Walking", true);
            
            _left.gameObject.SetActive(false);
            _up.gameObject.SetActive(false);
            _down.gameObject.SetActive(false);
        }

	    if (_direction.magnitude <= 0)
	    {
		    if (_down.gameObject.activeInHierarchy)
		    {
			    _animatorDown.SetBool("Walking", false);
		    }

		    if (_right.gameObject.activeInHierarchy)
		    {
			    _animatorRight.SetBool("Walking", false);
		    }

		    if (_left.gameObject.activeInHierarchy)
		    {
			    _animatorLeft.SetBool("Walking", false);
		    }

		    if (_up.gameObject.activeInHierarchy)
		    {
			    _animatorUp.SetBool("Walking", false);
		    }
	    }
    }
	
	private void AttackAnimation()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			if (_down.gameObject.activeInHierarchy)
			{
				if (_class.Equals("Melee"))
				{
					_animatorDown.SetTrigger("MeleeAttack");
					MeleeAttack(_down);
				}

				if (_class.Equals("Ranged"))
				{
					_animatorDown.SetTrigger("RangedAttack");
					RangedAttack();
				}
			}

			if (_right.gameObject.activeInHierarchy)
			{
				if (_class.Equals("Melee"))
				{
					_animatorRight.SetTrigger("MeleeAttack");
					MeleeAttack(_right);
				}

				if (_class.Equals("Ranged"))
				{
					_animatorRight.SetTrigger("RangedAttack");
					RangedAttack();
				}
			}

			if (_left.gameObject.activeInHierarchy)
			{
				if (_class.Equals("Melee"))
				{
					_animatorLeft.SetTrigger("MeleeAttack");
					MeleeAttack(_left);
				}

				if (_class.Equals("Ranged"))
				{
					_animatorLeft.SetTrigger("RangedAttack");
					RangedAttack();
				}
			}

			if (_up.gameObject.activeInHierarchy)
			{
				if (_class.Equals("Melee"))
				{
					_animatorUp.SetTrigger("MeleeAttack");
					MeleeAttack(_up);
				}

				if (_class.Equals("Ranged"))
				{
					_animatorUp.SetTrigger("RangedAttack");
					RangedAttack();
				}
			}
		}
	}

	private void MeleeAttack(Transform direction)
	{
		List<GameObject> targets = direction.GetChild(1).GetChild(0).gameObject.GetComponent<PlayerAttackCollider>().Targets;

		foreach (GameObject enemy in targets)
		{
			enemy.gameObject.GetComponent<EnemyController>().TakeDamage(100);
			Debug.Log("hit");
		}
	}

	private void RangedAttack()
	{
		GameObject projectile = ObjectPool.Instance.GetObject("PlayerArrow");
		projectile.GetComponentInChildren<PlayerProjectile>().InstantiateProjectile(50, 15f, transform.position);
		projectile.transform.position = transform.position;
	}
}
