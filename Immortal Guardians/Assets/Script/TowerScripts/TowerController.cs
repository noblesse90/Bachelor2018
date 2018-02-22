using System.Collections;
using System.Collections.Generic;
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

    private int price;
    private float range;

    public GameObject CurrentTarget
    {
        get
        {
            return currentTarget;
        }

        set
        {
            currentTarget = value;
        }
    }

    public float ProjectileSpeed
    {
        get
        {
            return projectileSpeed;
        }
    }

    public float AttackCooldown
    {
        set
        {
            attackCooldown = value;
        }
    }

    public int Price
    {
        get
        {
            return price;
        }

        set
        {
            price = value;
        }
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
        projectile.GetComponent<ProjectileController>().Damage = GetComponent<BasicTower>().Damage;
        projectile.transform.position = currentPos;
        projectile.transform.rotation = Quaternion.identity;
        projectile.GetComponent<ProjectileController>().SetTargetAndSpeed(CurrentTarget, ProjectileSpeed);
        
        //Instantiate(projectile, currentPos, Quaternion.identity, this.transform);
    }

    private void Selected() 
    {
        rangeSprite.SetActive(true);
        rangeSprite.transform.localScale = new Vector2(range / 2.5f, range / 2.5f);
    }

    private void Deselected()
    {
        rangeSprite.SetActive(false);
    }

    public void checkTowerType()
    {
        switch (gameObject.name)
        {
            case "BasicTower(Clone)":
                Price = gameObject.GetComponent<BasicTower>().Price;
                range = gameObject.GetComponent<BasicTower>().Range;
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

        if (nearestEnemy != null && shortestDistance <= GetComponent<BasicTower>().Range)
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
