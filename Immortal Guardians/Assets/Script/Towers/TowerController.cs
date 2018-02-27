using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using JetBrains.Annotations;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    
    private GameObject _currentTarget;

    [SerializeField] private GameObject _projectile;

    private bool _canAttack = true;

    private float _attackTimer;

    private float _attackCooldown;

    [SerializeField] private float _projectileSpeed;    


    private GameObject _rangeSprite;

    private string _projectileType;


    // Tower stats (default tower)
    private string _towerType;
    private float _range;
    private float _damage;
    private int _price;
    private int _upgradePrice;
    private int _level;
    private int _totalPrice;

    // Canon tower
    private float _splashDamage = 0;

    public float Range
    {
        get { return _range; }
        set { _range = value; }
    }

    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public int Price
    {
        get { return _price; }
        set { _price = value; }
    }

    public int Level
    {
        get { return _level; }
        set { _level = value; }
    }

    public GameObject CurrentTarget
    {
        get { return _currentTarget; }
        set { _currentTarget = value; }
    }

    public float ProjectileSpeed
    {
        get { return _projectileSpeed; }
        set { _projectileSpeed = value; }
    }

    public float AttackCooldown
    {
        get { return _attackCooldown; }
        set { _attackCooldown = value; }
    }

    public int TotalPrice
    {
        get { return _totalPrice; }
        set { _totalPrice = value; }
    }

    public string TowerType
    {
        get { return _towerType; }
        set { _towerType = value; }
    }

    public int UpgradePrice
    {
        get { return _upgradePrice; }
        set { _upgradePrice = value; }
    }

    public float SplashDamage
    {
        get { return _splashDamage; }

        set { _splashDamage = value; }
    }

    private void Awake()
    {
        TowerTypeAndInitialiser();
    }

    // Use this for initialization
    private void Start()
    {
        _rangeSprite = transform.Find("RangeSprite").gameObject;

        InvokeRepeating("UpdateTarget", 0f, 0.2f);
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateTarget();

        if (TowerManager.Instance.CurrentTower == gameObject)
        {
            Selected();
        }
        else
        {
            Deselected();
        }
    }

    private void Shoot()
    {
        Vector3 currentPos = transform.position;
        //currentPos.y += 1;
        GameObject projectile = ObjectPool.Instance.GetObject(_projectileType);
        switch (_projectileType)
        {
                case "BasicProjectile":
                    projectile.GetComponentInChildren<BasicTProjectile>().Damage = (int)Damage;
                    projectile.transform.position = currentPos;
                    projectile.transform.rotation = Quaternion.identity;
                    projectile.GetComponentInChildren<BasicTProjectile>().SetTargetAndSpeed(CurrentTarget, ProjectileSpeed);
                    break;
                
                case "CanonProjectile":
                    projectile.GetComponent<CanonTProjectile>().Damage = (int)Damage;
                    projectile.transform.position = currentPos;
                    projectile.transform.rotation = Quaternion.identity;
                    projectile.GetComponent<CanonTProjectile>().SetTargetAndSpeed(CurrentTarget, ProjectileSpeed);
                    break;
        }
        
        
        //Instantiate(projectile, currentPos, Quaternion.identity, this.transform);
    }

    private void Selected() 
    {
        _rangeSprite.SetActive(true);
        _rangeSprite.transform.localScale = new Vector2(_range / 2.05f, _range / 2.05f);
    }

    private void Deselected()
    {
        _rangeSprite.SetActive(false);
    }

    private void TowerTypeAndInitialiser()
    {
        switch (gameObject.name)
        {
            case "BasicTower(Clone)":
                GetComponent<BasicTower>().InitialiserStats();
                _projectileType = "BasicProjectile";
                break;

            case "CanonTower(Clone)":
                GetComponent<CanonTower>().InitialiserStats();
                _projectileType = "CanonProjectile";
                break;

            case "LightningTower(Clone)":
                Debug.Log("LIGHTNING TOWER");
                break;

            case "IceTower(Clone)":
                Debug.Log("ICE TOWER");
                break;

            default: break;
        }
    }

    public void Upgrade()
    {
        switch (gameObject.name)
        {
            case "BasicTower(Clone)":
                GetComponent<BasicTower>().Upgrade(Level);

                break;

            case "CanonTower(Clone)":
                GetComponent<CanonTower>().Upgrade(Level);
                break;

            case "LightningTower(Clone)":
                Debug.Log("LIGHTNING TOWER");
                break;

            case "IceTower(Clone)":
                Debug.Log("ICE TOWER");
                break;

            default: break;
        }
    }


    private void UpdateTarget()
    {

        if (!_canAttack)
        {
            _attackTimer += Time.deltaTime;

            if (_attackTimer >= _attackCooldown)
            {
                _canAttack = true;
                _attackTimer = 0;
            }
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (!(distanceToEnemy < shortestDistance)) continue;
            shortestDistance = distanceToEnemy;
            nearestEnemy = enemy;
        }

        if (nearestEnemy != null && shortestDistance <= Range)
        {
            if (!_canAttack) return;
            _currentTarget = nearestEnemy;
            Shoot();
            _canAttack = false;

        }
        else
        {
            _currentTarget = null;
        }

    }
}
