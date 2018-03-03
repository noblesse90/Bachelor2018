using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Timers;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
	
	private int _rightClickCost = 10;
	private int _firstAbilityCost = 20;

	public int RightClickCost
	{
		get { return _rightClickCost; }
		set { _rightClickCost = value; }
	}

	public int FirstAbilityCost
	{
		get { return _firstAbilityCost; }
		set { _firstAbilityCost = value; }
	}


	// PLAYER STATS
	
	private float _mana, _maxMana = 100;
	private int _damage = 50;
	
	
	public float MaxMana
	{
		get { return _maxMana; }
		set { _maxMana = value; }
	}

	public float Mana
	{
		get { return _mana; }
		set { _mana = value; }
	}

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
		if (!EventSystem.current.IsPointerOverGameObject())
		{
			AttackAnimation();
		}

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
		LeftClick();
		RightClick();
		FirstAbilityClick();
	}

	private void ManaCost(int cost)
	{
		_mana -= cost;
		UIManager.Instance.ManaBar.fillAmount = _mana / _maxMana;
	}

	private void MeleeAttack(Transform aniDirection)
	{
		List<GameObject> targets = aniDirection.GetChild(1).GetChild(0).gameObject.GetComponent<PlayerAttackCollider>().Targets;

		foreach (GameObject enemy in targets)
		{
			enemy.gameObject.GetComponent<EnemyController>().TakeDamage(100);
			Debug.Log("hit");
		}
	}
	
	private void RangedAttack()
	{
		GameObject projectile = ObjectPool.Instance.GetObject("PlayerArrow");
		projectile.GetComponentInChildren<PlayerProjectile>().InstantiateProjectile(_damage, 25f, transform.position, GManager.Instance.GetMousePos(), 0);
	}
	
	private void MeleeRightClickAttack(Transform aniDirection)
	{
		ManaCost(_rightClickCost);
	}

	private void RangedRightClickAttack()
	{
		Color color = new Color(1f,1f,0.4f);
		GameObject projectile = ObjectPool.Instance.GetObject("PlayerArrow");
		projectile.GetComponentInChildren<PlayerProjectile>().InstantiateProjectile(_damage*0.75f, 25f, transform.position, GManager.Instance.GetMousePos(), 0);
		projectile.GetComponentInChildren<SpriteRenderer>().color = color;
		
		projectile = ObjectPool.Instance.GetObject("PlayerArrow");
		projectile.GetComponentInChildren<PlayerProjectile>().InstantiateProjectile(_damage*0.75f, 25f, transform.position, GManager.Instance.GetMousePos(), 5);
		projectile.GetComponentInChildren<SpriteRenderer>().color = color;
		
		projectile = ObjectPool.Instance.GetObject("PlayerArrow");
		projectile.GetComponentInChildren<PlayerProjectile>().InstantiateProjectile(_damage*0.75f, 25f, transform.position, GManager.Instance.GetMousePos(), -5);
		projectile.GetComponentInChildren<SpriteRenderer>().color = color;
		
		ManaCost(_rightClickCost);
	}
	
	// FIRST ABILITY (SCATTER SHOT)
	
	private void ScatterShot()
	{
		GameObject projectile = ObjectPool.Instance.GetObject("PlayerArrow");
		projectile.GetComponentInChildren<PlayerProjectile>().ScatterShot = true;
		projectile.GetComponentInChildren<PlayerProjectile>().InstantiateProjectile(_damage*0.75f, 25f, transform.position, GManager.Instance.GetMousePos(), 0);
		
		ManaCost(_firstAbilityCost);
	}

	private void LeftClick()
	{
		if (Input.GetKey(KeyCode.Mouse0))
		{
			
			if(UIManager.Instance.CanBasicAttack)
			{
				if (_class.Equals("Melee"))
				{
					if (_down.gameObject.activeInHierarchy)
					{
						_animatorDown.SetTrigger("MeleeAttack");
						MeleeAttack(_down);
					}

					if (_right.gameObject.activeInHierarchy)
					{
						_animatorRight.SetTrigger("MeleeAttack");
						MeleeAttack(_right);
					}

					if (_left.gameObject.activeInHierarchy)
					{
						_animatorLeft.SetTrigger("MeleeAttack");
						MeleeAttack(_left);
					}

					if (_up.gameObject.activeInHierarchy)
					{
						_animatorUp.SetTrigger("MeleeAttack");
						MeleeAttack(_up);
					}
				}

				if (_class.Equals("Ranged"))
				{
					if (_down.gameObject.activeInHierarchy)
					{
						_animatorDown.SetTrigger("RangedAttack");
						RangedAttack();
					}

					if (_right.gameObject.activeInHierarchy)
					{
						_animatorRight.SetTrigger("RangedAttack");
						RangedAttack();
					}

					if (_left.gameObject.activeInHierarchy)
					{
						_animatorLeft.SetTrigger("RangedAttack");
						RangedAttack();
					}

					if (_up.gameObject.activeInHierarchy)
					{
						_animatorUp.SetTrigger("RangedAttack");
						RangedAttack();
					}
				}

				UIManager.Instance.CanBasicAttack = false;
			}
		}
	}

	private void RightClick()
	{
		if (Input.GetKeyDown(KeyCode.Mouse1) && _mana >= _rightClickCost)
		{
			if (UIManager.Instance.CanGcdAttack)
			{
				if (_class.Equals("Melee"))
				{
					if (_down.gameObject.activeInHierarchy)
					{
						_animatorDown.SetTrigger("MeleeAttack");
						MeleeRightClickAttack(_down);
					}

					if (_right.gameObject.activeInHierarchy)
					{
						_animatorRight.SetTrigger("MeleeAttack");
						MeleeRightClickAttack(_right);
					}

					if (_left.gameObject.activeInHierarchy)
					{
						_animatorLeft.SetTrigger("MeleeAttack");
						MeleeRightClickAttack(_left);
					}

					if (_up.gameObject.activeInHierarchy)
					{
						_animatorUp.SetTrigger("MeleeAttack");
						MeleeRightClickAttack(_up);
					}
				}
				
				if (_class.Equals("Ranged"))
				{
					if (_down.gameObject.activeInHierarchy)
					{
						_animatorDown.SetTrigger("RangedAttack");
						RangedRightClickAttack();
					}

					if (_right.gameObject.activeInHierarchy)
					{
						_animatorRight.SetTrigger("RangedAttack");
						RangedRightClickAttack();
					}

					if (_left.gameObject.activeInHierarchy)
					{
						_animatorLeft.SetTrigger("RangedAttack");
						RangedRightClickAttack();
					}

					if (_up.gameObject.activeInHierarchy)
					{
						_animatorUp.SetTrigger("RangedAttack");
						RangedRightClickAttack();
					}
				}

				UIManager.Instance.CanGcdAttack = false;
			}
		}
	}

	private void FirstAbilityClick()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1) && _mana >= _firstAbilityCost)
		{
			if (UIManager.Instance.CanGcdAttack)
			{
				if (_down.gameObject.activeInHierarchy)
				{
					_animatorDown.SetTrigger("RangedAttack");
					ScatterShot();
				}

				if (_right.gameObject.activeInHierarchy)
				{
					_animatorRight.SetTrigger("RangedAttack");
					ScatterShot();
				}

				if (_left.gameObject.activeInHierarchy)
				{
					_animatorLeft.SetTrigger("RangedAttack");
					ScatterShot();
				}

				if (_up.gameObject.activeInHierarchy)
				{
					_animatorUp.SetTrigger("RangedAttack");
					ScatterShot();
				}

				UIManager.Instance.CanGcdAttack = false;
			}
		}
	}
}
