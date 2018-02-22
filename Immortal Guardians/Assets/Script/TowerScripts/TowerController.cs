using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{

    private Queue<GameObject> targets;

    private GameObject currentTarget;

    [SerializeField] private GameObject projectile;

    private bool canAttack = true;

    private float attackTimer;

    [SerializeField] private float attackCooldown;

    [SerializeField] private float projectileSpeed;

    private GameObject rangeSprite;


    public Queue<GameObject> Targets
    {
        get
        {
            return targets;
        }
    }

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


    // Use this for initialization
    void Start()
    {
        targets = new Queue<GameObject>();
        rangeSprite = transform.Find("RangeSprite").gameObject;

        InvokeRepeating("UpdateTarget", 0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        //Attack();
        UpdateTarget();

        if (GManager.Instance.CurrentTower == gameObject)
        {
            Selected();
        }
        else
        {
            Deselected();
        }
    }

    private void Attack()
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

        if (currentTarget == null && targets.Count > 0)
        {
            currentTarget = targets.Dequeue();
        }

        if (currentTarget != null && currentTarget.activeInHierarchy)
        {
            if (canAttack)
            {
                shoot();
                canAttack = false;
            }
        }


    }

    private void shoot()
    {
        Vector3 currentPos = transform.position;
        currentPos.y += 1;
        GameObject projectile = ObjectPool.Instance.GetObject("ProjectileTest");
        projectile.transform.position = currentPos;
        projectile.transform.rotation = Quaternion.identity;
        projectile.transform.parent = this.transform;
        projectile.GetComponent<ProjectileController>().target = currentTarget;
        //Instantiate(projectile, currentPos, Quaternion.identity, this.transform);
    }

    private void Selected()
    {
        rangeSprite.SetActive(true);
    }

    private void Deselected()
    {
        rangeSprite.SetActive(false);
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

        if (nearestEnemy != null && shortestDistance <= 5.0f)
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
