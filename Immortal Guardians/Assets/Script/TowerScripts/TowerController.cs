using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using JetBrains.Annotations;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    
    private GameObject currentTarget;

    [SerializeField] private GameObject projectile;

    private bool canAttack = true;

    private float attackTimer;

    private float attackCooldown;

    [SerializeField] private float projectileSpeed;    


    private GameObject rangeSprite;


    // Tower stats
    private string _towerType;
    private float _range;
    private float _damage;
    private int _price;
    private int _upgradePrice;
    private int _level;
    private int _totalPrice;

    // Tower stats text (UI)
    [SerializeField] 

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
        get { return currentTarget; }
        set { currentTarget = value; }
    }

    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
        set { projectileSpeed = value; }
    }

    public float AttackCooldown
    {
        get { return attackCooldown; }
        set { attackCooldown = value; }
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

    private void Awake()
    {
        TowerTypeAndInitialiser();
    }

    // Use this for initialization
    void Start()
    {
        rangeSprite = transform.Find("RangeSprite").gameObject;

        InvokeRepeating("UpdateTarget", 0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
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

    private void shoot()
    {
        Vector3 currentPos = transform.position;
        currentPos.y += 1;
        GameObject projectile = ObjectPool.Instance.GetObject("ProjectileTest");
        projectile.GetComponent<ProjectileController>().Damage = (int)Damage;
        projectile.transform.position = currentPos;
        projectile.transform.rotation = Quaternion.identity;
        projectile.GetComponent<ProjectileController>().SetTargetAndSpeed(CurrentTarget, ProjectileSpeed);
        
        //Instantiate(projectile, currentPos, Quaternion.identity, this.transform);
    }

    private void Selected() 
    {
        rangeSprite.SetActive(true);
        rangeSprite.transform.localScale = new Vector2(_range / 2.05f, _range / 2.05f);
    }

    public void Deselected()
    {
        rangeSprite.SetActive(false);
    }

    public void TowerTypeAndInitialiser()
    {
        switch (gameObject.name)
        {
            case "BasicTower(Clone)":
                GetComponent<BasicTower>().InitialiserStats();
                
                break;

            case "CanonTower(Clone)":
                Debug.Log("CANON TOWER");
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
                Debug.Log("CANON TOWER");
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



    void UpdateTarget()
    {

        if (!canAttack)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= Range)
        {
            if (canAttack)
            {
                currentTarget = nearestEnemy;
                shoot();
                canAttack = false;
            }

        }
        else
        {
            currentTarget = null;
        }

    }
}
