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

	private Class _class = Class.Ranged;

	private LookDirection _lookDirection = LookDirection.Down;

	//Manacost for skills
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
	
	public Class GetClass
	{
		get { return _class; }
		set { _class = value; }
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
		    _lookDirection = LookDirection.Left;
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
		    _lookDirection = LookDirection.Right;
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
	        _lookDirection = LookDirection.Up;
            _up.gameObject.SetActive(true);
	        
	        _animatorUp.SetBool("Walking", true);
            
            _left.gameObject.SetActive(false);
            _right.gameObject.SetActive(false);
            _down.gameObject.SetActive(false);
        } 
        

        if (Input.GetKey(KeyCode.A))
        {
            _direction += Vector2.left;
	        _lookDirection = LookDirection.Left;
            _left.gameObject.SetActive(true);
	        
	        _animatorLeft.SetBool("Walking", true);
            
            _up.gameObject.SetActive(false);
            _right.gameObject.SetActive(false);
            _down.gameObject.SetActive(false);
        }
        

        if (Input.GetKey(KeyCode.S))
        {
            _direction += Vector2.down;
	        _lookDirection = LookDirection.Down;
            _down.gameObject.SetActive(true);
	        
	        _animatorDown.SetBool("Walking", true);
            
            _left.gameObject.SetActive(false);
            _right.gameObject.SetActive(false);
            _up.gameObject.SetActive(false);
	        
        }
        

        if (Input.GetKey(KeyCode.D))
        {
            _direction += Vector2.right;
	        _lookDirection = LookDirection.Right;
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
			GameObject explosion = ObjectPool.Instance.GetObject("ArrowExplosion");
			explosion.transform.position = enemy.transform.position;
		}
	}
	
	private void RangedAttack()
	{
		GameObject projectile = ObjectPool.Instance.GetObject("PlayerArrow");
		projectile.GetComponentInChildren<PlayerProjectile>().InstantiateProjectile(_damage, 25f, transform.position, GManager.Instance.GetMousePos(), 0);
	}
	
	
	private void AxeThrow()
	{
		GameObject axe = ObjectPool.Instance.GetObject("AxeThrow");
		axe.GetComponent<WeaponCollider>().InstantiateProjectile(_damage, 20, transform.position, GManager.Instance.GetMousePos(),0);
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
				switch (_class)
				{
					case Class.Ranged:
						switch (_lookDirection)
						{
							case LookDirection.Down:
								_animatorDown.SetTrigger("RangedAttack");
								RangedAttack();
								break;
							
							case LookDirection.Left:
								_animatorLeft.SetTrigger("RangedAttack");
								RangedAttack();
								break;
							
							case LookDirection.Right:
								_animatorRight.SetTrigger("RangedAttack");
								RangedAttack();
								break;
							
							case LookDirection.Up:
								_animatorUp.SetTrigger("RangedAttack");
								RangedAttack();
								break;
							
							default: break;
						}
						break;
					
					case Class.Melee:
						switch (_lookDirection)
						{
							case LookDirection.Down:
								_animatorDown.SetTrigger("MeleeAttack");
								StartCoroutine(MeleeAttackCoroutine(_down));
								break;
							
							case LookDirection.Left:
								_animatorLeft.SetTrigger("MeleeAttack");
								StartCoroutine(MeleeAttackCoroutine(_left));
								break;
							
							case LookDirection.Right:
								_animatorRight.SetTrigger("MeleeAttack");
								StartCoroutine(MeleeAttackCoroutine(_right));
								break;
							
							case LookDirection.Up:
								_animatorUp.SetTrigger("MeleeAttack");
								StartCoroutine(MeleeAttackCoroutine(_up));
								break;
							
							default: break;
						}
						break;
					
					default: break;
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
				switch (_class)
				{
					case Class.Ranged:
						switch (_lookDirection)
						{
							case LookDirection.Down:
								_animatorDown.SetTrigger("RangedAttack");
								RangedRightClickAttack();
								break;
							
							case LookDirection.Left:
								_animatorLeft.SetTrigger("RangedAttack");
								RangedRightClickAttack();
								break;
							
							case LookDirection.Right:
								_animatorRight.SetTrigger("RangedAttack");
								RangedRightClickAttack();
								break;
							
							case LookDirection.Up:
								_animatorUp.SetTrigger("RangedAttack");
								RangedRightClickAttack();
								break;
							
							default: break;
						}
						break;
					
					case Class.Melee:
						switch (_lookDirection)
						{
							case LookDirection.Down:
								_animatorDown.SetTrigger("AxeThrow");
								Invoke("AxeThrow", 0.5f);
								break;
							
							case LookDirection.Left:
								_animatorLeft.SetTrigger("AxeThrow");
								Invoke("AxeThrow", 0.5f);
								break;
							
							case LookDirection.Right:
								_animatorRight.SetTrigger("AxeThrow");
								Invoke("AxeThrow", 0.5f);
								break;
							
							case LookDirection.Up:
								_animatorUp.SetTrigger("AxeThrow");
								Invoke("AxeThrow", 0.5f);
								break;
							
							default: break;
						}
						break;
					
					default: break;
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
				switch (_class)
				{
					case Class.Ranged:
						switch (_lookDirection)
						{
							case LookDirection.Down:
								_animatorDown.SetTrigger("RangedAttack");
								ScatterShot();
								break;
							
							case LookDirection.Left:
								_animatorLeft.SetTrigger("RangedAttack");
								ScatterShot();
								break;
							
							case LookDirection.Right:
								_animatorRight.SetTrigger("RangedAttack");
								ScatterShot();
								break;
							
							case LookDirection.Up:
								_animatorUp.SetTrigger("RangedAttack");
								ScatterShot();
								break;
							
							default: break;
						}
						break;
					/*
					case Class.Melee:
						switch (_lookDirection)
						{
							case LookDirection.Down:
								_animatorDown.SetTrigger("MeleeAttack");
								MeleeAttack(_down);
								break;
							
							case LookDirection.Left:
								_animatorLeft.SetTrigger("MeleeAttack");
								MeleeAttack(_left);
								break;
							
							case LookDirection.Right:
								_animatorRight.SetTrigger("MeleeAttack");
								MeleeAttack(_right);
								break;
							
							case LookDirection.Up:
								_animatorUp.SetTrigger("MeleeAttack");
								MeleeAttack(_up);
								break;
							
							default: break;
						}
						break;
					*/
					default: break;
				}

				UIManager.Instance.CanGcdAttack = false;
			}
		}
	}

	
	private enum LookDirection
	{
		Left,
		Right,
		Up,
		Down
	}

	public enum Class
	{
		Ranged,
		Melee
	}


	IEnumerator MeleeAttackCoroutine(Transform direction)
	{
		yield return new WaitForSeconds(0.35f);
		MeleeAttack(direction);
	}
	
	// SWITCH CLASS
	public void SwitchClass()
	{
		_class = _class == Class.Melee ? Class.Ranged : Class.Melee;
		UIManager.Instance.SwitchClassIcons();
	}


	
	
}
