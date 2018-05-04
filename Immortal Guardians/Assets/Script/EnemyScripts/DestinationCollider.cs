using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Destroys enemies when entering destination and removes life

public class DestinationCollider : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collisionObject)
    {
        if (collisionObject.CompareTag("Enemy"))
        {
            collisionObject.transform.GetComponent<EnemyController>().Release();


            if (UIManager.Instance.Life > 0)
            {
                AudioManager.Instance.Play("Lose_Life");

                if (collisionObject.GetComponent<EnemyController>().Boss)
                {
                    UIManager.Instance.Life -= 10;
                }
                else
                {
                    UIManager.Instance.Life -= 1;
                }
                
            }
            
        }

    }
}
