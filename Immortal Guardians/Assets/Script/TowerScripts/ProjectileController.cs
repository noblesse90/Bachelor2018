using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    private GameObject target;

    TowerController tc;


    private void Start()
    {
        tc = transform.GetComponentInParent<TowerController>();
        target = tc.CurrentTarget;

    }

    // Update is called once per frame
    void Update () {
        moveToTarget();
        destroyProjectile();
	}

    public void moveToTarget()
    {
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * tc.ProjectileSpeed);
        }
    }

    public void destroyProjectile()
    {
        if(target == null)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(target != null)
        {
            if (collision.GetComponent<Collider2D>().Equals(target.GetComponent<Collider2D>()))
            {
                target.GetComponent<EnemyController>().takeDamage(10);
                Destroy(gameObject);
            }
        }
       
    }
}
