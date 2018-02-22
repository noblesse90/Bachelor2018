using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public GameObject target;

    TowerController tc;


    private void Start()
    {
        tc = transform.GetComponentInParent<TowerController>();
        target = tc.CurrentTarget;
    }

    // Update is called once per frame
    void Update () {
        moveToTarget();
	}

    public void moveToTarget()
    {
        if(target != null && target.activeInHierarchy)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * tc.ProjectileSpeed);
        }
        else
        {
            Release();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(target.activeSelf)
        {
            if (collision.GetComponent<Collider2D>().Equals(target.GetComponent<Collider2D>()))
            {
                target.GetComponent<EnemyController>().takeDamage(10);
                Release();
            }
        }
       
    }

    private void Release()
    {
        gameObject.SetActive(false);
        transform.parent.GetComponent<TowerController>().CurrentTarget = null;
    }
}
