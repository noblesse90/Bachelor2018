using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class PlayerController : Singleton<PlayerController> {

    // PLAYER SPEED
    private int _speed = 20;
	

	public int Speed
	{
		get { return _speed; }
		set { _speed = value; }
	}

	private Vector2 _mousePos;

	[SerializeField] private GameObject _orbitingSword;

    // Rigidbody variable to hold on a rigidbodody component
    private Rigidbody2D _rb;

    // vector2 variable that controlls direction
    private Vector2 _direction;
    
    // player transform directions
	private Transform _down, _up, _left, _right;

    private Animator _animatorDown, _animatorUp, _animatorLeft, _animatorRight;

	private Class _class;

	private LookDirection _lookDirection = LookDirection.Down;
	
	// SET TARGET FOR VCAM
	[SerializeField] private CinemachineVirtualCamera _vcam;

	//Manacost for skills
	private int _rightClickCost = 10;
	private int _scatterShotCost = 20;
	private float _orbitingSwordCost = 10;

	public int RightClickCost
	{
		get { return _rightClickCost; }
		set { _rightClickCost = value; }
	}

	public int ScatterShotCost
	{
		get { return _scatterShotCost; }
		set { _scatterShotCost = value; }
	}
	
	public float OrbitingSwordCost
	{
		get { return _orbitingSwordCost; }
		set { _orbitingSwordCost = value; }
	}
	
	// ORBITING SWORDS
	
	private bool _orbitingSwordBool = false;
	private GameObject _orbSword;
	
	public bool OrbitingSwordBool
	{
		get { return _orbitingSwordBool; }
		set { _orbitingSwordBool = value; }
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

	private void OnEnable()
	{
		_vcam.Follow = transform;
	}

	// PLAYER MOVEMENT
	private void FixedUpdate () {
		_rb.velocity = _rb.velocity.normalized * _speed;
    }

	private void Update()
	{
		if (GManager.Instance.Paused) return;
		// gets the mouse position since it's used multiple times
		_mousePos = GManager.Instance.GetMousePos();
		// gets an input from the keyboard
		GetInput();
		// move the gameobject according to the keypresses
		Move();
		// does the animation for the player according to mouse position
		AnimationDirection();
		
		// reduces the players mana if he's using Orbiting swords
		OrbitingSwordManaReduction();
		
		// stops player from attacking if he tries to select tower
		if (Input.GetKey(KeyCode.Space) || GManager.Instance.BuildMode) return;
		// Attacks
		if (!EventSystem.current.IsPointerOverGameObject())
		{
			AttackAnimation();
		}
		
	}
	
	// GET INPUT FROM USER
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

	private void Move()
    {
        transform.Translate(_direction * Time.deltaTime * _speed);
    }

	// ACTIVATES THE ANIMATION FOR THE DIRECTION THE MOUSE IS IN
	// First checks in a cross then it checks on 45 degress
	private void AnimationDirection()
	{
		var pos = _mousePos - (Vector2) transform.position;

		if (pos.x >= 0 && pos.y >= 0)
		{
			if (pos.x - pos.y < 0)
			{
				// UP
				_lookDirection = LookDirection.Up;
				_up.gameObject.SetActive(true);
	        
				// CHECKS IF WALKING OR NOT
				_animatorUp.SetBool("Walking", !(_direction.magnitude <= 0));

				_left.gameObject.SetActive(false);
				_right.gameObject.SetActive(false);
				_down.gameObject.SetActive(false);
			}
			else
			{
				// RIGHT
				_lookDirection = LookDirection.Right;
				_right.gameObject.SetActive(true);
	        
				// CHECKS IF WALKING OR NOT
				_animatorRight.SetBool("Walking", !(_direction.magnitude <= 0));

				_left.gameObject.SetActive(false);
				_up.gameObject.SetActive(false);
				_down.gameObject.SetActive(false);
			}
			
		}
		else if (pos.x >= 0 && pos.y < 0)
		{
			if (pos.x + pos.y < 0)
			{
				// DOWN
				_lookDirection = LookDirection.Down;
				_down.gameObject.SetActive(true);
	        
				// CHECKS IF WALKING OR NOT
				_animatorDown.SetBool("Walking", !(_direction.magnitude <= 0));

				_left.gameObject.SetActive(false);
				_right.gameObject.SetActive(false);
				_up.gameObject.SetActive(false);
			}
			else
			{
				// RIGHT
				_lookDirection = LookDirection.Right;
				_right.gameObject.SetActive(true);
	        
				// CHECKS IF WALKING OR NOT
				_animatorRight.SetBool("Walking", !(_direction.magnitude <= 0));

				_left.gameObject.SetActive(false);
				_up.gameObject.SetActive(false);
				_down.gameObject.SetActive(false);
			}
		}
		else if (pos.x < 0 && pos.y < 0)
		{
			if (pos.x - pos.y < 0)
			{
				// LEFT
				_lookDirection = LookDirection.Left;
				_left.gameObject.SetActive(true);
	        
				// CHECKS IF WALKING OR NOT
				_animatorLeft.SetBool("Walking", !(_direction.magnitude <= 0));

				_up.gameObject.SetActive(false);
				_right.gameObject.SetActive(false);
				_down.gameObject.SetActive(false);
			}
			else
			{
				// DOWN
				_lookDirection = LookDirection.Down;
				_down.gameObject.SetActive(true);
	        
				// CHECKS IF WALKING OR NOT
				_animatorDown.SetBool("Walking", !(_direction.magnitude <= 0));

				_left.gameObject.SetActive(false);
				_right.gameObject.SetActive(false);
				_up.gameObject.SetActive(false);
			}
		}
		else if (pos.x < 0 && pos.y >= 0)
		{
			if (pos.x + pos.y < 0)
			{
				// LEFT
				_lookDirection = LookDirection.Left;
				_left.gameObject.SetActive(true);
	        
				// CHECKS IF WALKING OR NOT
				_animatorLeft.SetBool("Walking", !(_direction.magnitude <= 0));

				_up.gameObject.SetActive(false);
				_right.gameObject.SetActive(false);
				_down.gameObject.SetActive(false);
			}
			else
			{
				// UP
				_lookDirection = LookDirection.Up;
				_up.gameObject.SetActive(true);
				
				// CHECKS IF WALKING OR NOT
				_animatorUp.SetBool("Walking", !(_direction.magnitude <= 0));

				_left.gameObject.SetActive(false);
				_right.gameObject.SetActive(false);
				_down.gameObject.SetActive(false);
			}
		}
	}

	
	// RUNS THE ATTACKANIMATIONS AND ABILITIES
	
	private void AttackAnimation()
	{
		LeftClick();
		RightClick();
		FirstAbilityClick();
	}
	
	// CALCULATE THE MANACOST

	private void ManaCost(int cost)
	{
		_mana -= cost;
		UIManager.Instance.ManaBar.fillAmount = _mana / _maxMana;
	}

	
	// ------------------------ RANGED ATTACKS ---------------
	
	
	// LEFT CLICK ATTACK
	
	private void RangedAttack()
	{
		AudioManager.Instance.Play("Bow_Release");
		
		GameObject projectile = ObjectPool.Instance.GetObject("PlayerArrow");
		projectile.GetComponentInChildren<PlayerProjectile>().InstantiateProjectile(_damage, 25f, transform.position, _mousePos, 0);
	}

	// RIGHT CLICK (MULTISHOT)
	
	private void Multishot()
	{
		AudioManager.Instance.Play("Bow_Release");
		
		Color color = new Color(1f,1f,0.4f);
		GameObject projectile = ObjectPool.Instance.GetObject("PlayerArrow");
		projectile.GetComponentInChildren<PlayerProjectile>().InstantiateProjectile(_damage*0.75f, 25f, transform.position, _mousePos, 0);
		projectile.GetComponentInChildren<SpriteRenderer>().color = color;
		
		projectile = ObjectPool.Instance.GetObject("PlayerArrow");
		projectile.GetComponentInChildren<PlayerProjectile>().InstantiateProjectile(_damage*0.75f, 25f, transform.position, _mousePos, 5);
		projectile.GetComponentInChildren<SpriteRenderer>().color = color;
		
		projectile = ObjectPool.Instance.GetObject("PlayerArrow");
		projectile.GetComponentInChildren<PlayerProjectile>().InstantiateProjectile(_damage*0.75f, 25f, transform.position, _mousePos, -5);
		projectile.GetComponentInChildren<SpriteRenderer>().color = color;
		
		ManaCost(_rightClickCost);
	}
	
	// FIRST ABILITY (SCATTER SHOT)
	
	private void ScatterShot()
	{
		AudioManager.Instance.Play("Bow_Release");
		
		GameObject projectile = ObjectPool.Instance.GetObject("PlayerArrow");
		projectile.GetComponentInChildren<PlayerProjectile>().ScatterShot = true;
		projectile.GetComponentInChildren<PlayerProjectile>().InstantiateProjectile(_damage, 25f, transform.position, _mousePos, 0);
		
		ManaCost(_scatterShotCost);	
	}
	
	// ------------------------- MELEE ATTACKS --------------------
	
	
	// LEFT CLICK ATTACK
	
	private void MeleeAttack()
	{
		AudioManager.Instance.Play("Sword_Swing");
		
		GameObject cone = ObjectPool.Instance.GetObject("ConeProjectile");
		cone.GetComponent<ConeProjectile>().InstantiateProjectile(_damage, 15, transform.position, GManager.Instance.GetMousePos());
	}
	
	// RIGHT CLICK ATTACK (AXE THROW) 
	
	private void AxeThrow()
	{
		AudioManager.Instance.Play("Axe_Throw");
		GameObject axe = ObjectPool.Instance.GetObject("AxeThrow");
		axe.GetComponent<AxeProjectile>().InstantiateProjectile(_damage, 20, transform.position, GManager.Instance.GetMousePos());
		ManaCost(_rightClickCost);
	}
	
	// FIRST ABILITY (ORBITING SWORDS)
	
	private void OrbitingSwords()
	{
		_orbSword = Instantiate(_orbitingSword, transform);
		_orbSword.GetComponent<OrbitingSwordScript>().TransformRotationDamage(new Vector3(-180, 0, 0), new Vector3(0, 0, 90), _damage/2f);
		_orbSword = Instantiate(_orbitingSword, transform);
		_orbSword.GetComponent<OrbitingSwordScript>().TransformRotationDamage(new Vector3(180, 0, 0), new Vector3(0, 0, -90), _damage/2f);
		_orbSword = Instantiate(_orbitingSword, transform);
		_orbSword.GetComponent<OrbitingSwordScript>().TransformRotationDamage(new Vector3(0, 180, 0), new Vector3(0, 0, 0), _damage/2f);
		_orbSword = Instantiate(_orbitingSword, transform);
		_orbSword.GetComponent<OrbitingSwordScript>().TransformRotationDamage(new Vector3(0, -180, 0), new Vector3(0, 0, 180), _damage/2f);

		_orbitingSwordBool = true;
	}

	private void DestroySwords()
	{
		if (_orbSword != null)
		{
			_orbitingSwordBool = false;
		}
	}

	private void OrbitingSwordManaReduction()
	{
		if (_orbitingSwordBool && _mana >= 0)
		{
			_mana -= _orbitingSwordCost * Time.deltaTime;
		}
		else
		{
			_orbitingSwordBool = false;
		}
	}
	
	// LEFT CLICK KEYPRESS

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
								StartCoroutine(MeleeAttackCoroutine());
								break;
							
							case LookDirection.Left:
								_animatorLeft.SetTrigger("MeleeAttack");
								StartCoroutine(MeleeAttackCoroutine());
								break;
							
							case LookDirection.Right:
								_animatorRight.SetTrigger("MeleeAttack");
								StartCoroutine(MeleeAttackCoroutine());
								break;
							
							case LookDirection.Up:
								_animatorUp.SetTrigger("MeleeAttack");
								StartCoroutine(MeleeAttackCoroutine());
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
	
	// RIGHT CLICK KEYPRESS

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
								Multishot();
								break;
							
							case LookDirection.Left:
								_animatorLeft.SetTrigger("RangedAttack");
								Multishot();
								break;
							
							case LookDirection.Right:
								_animatorRight.SetTrigger("RangedAttack");
								Multishot();
								break;
							
							case LookDirection.Up:
								_animatorUp.SetTrigger("RangedAttack");
								Multishot();
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

	// 1 KEY PRESS
	
	private void FirstAbilityClick()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1) )
		{
			if (UIManager.Instance.CanGcdAttack)
			{
				if (_class == Class.Ranged && _mana >= _scatterShotCost)
				{
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
				}
				else if (_class == Class.Melee)
				{
					if (OrbitingSwordBool)
					{
						DestroySwords();
						return;
					}
					
					if (_mana >= _orbitingSwordCost)
					{
						OrbitingSwords();
					}
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


	IEnumerator MeleeAttackCoroutine()
	{
		yield return new WaitForSeconds(0.1f);
		MeleeAttack();
	}

}
