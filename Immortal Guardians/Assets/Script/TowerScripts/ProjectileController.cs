using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    private GameObject target;

    private float speed = 0;

    private int damage = 0;

    public int Damage
    {
        set
        {
            damage = value;
        }
    }


    // Update is called once per frame
    void Update () {
        moveToTarget();
	}

    public void moveToTarget()
    {
        if(target != null && target.activeInHierarchy)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
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
                target.GetComponent<EnemyController>().takeDamage(damage);
                Release();
            }
        }
       
    }

    private void Release()
    {
        gameObject.SetActive(false);
    }

    public void SetTargetAndSpeed(GameObject target, float speed)
    {
        this.target = target;
        this.speed = speed;
    }
}
