using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : Singleton<TowerRange> {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            transform.GetComponentInParent<TowerController>().Targets.Enqueue(collision.gameObject);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            transform.GetComponentInParent<TowerController>().CurrentTarget = null;
        }
       
    }
}
