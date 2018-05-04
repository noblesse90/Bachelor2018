using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Destroys enemies when entering destination and removes life

public class DestinationCollider : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collisionObject)
    {
        // checks if it is an enemy object entering
        if (collisionObject.CompareTag("Enemy"))
        {
            // runs the release (destroy) method from enemy
            collisionObject.transform.GetComponent<EnemyController>().Release();

            // checks if life is above 0 and reacts accordingly
            if (UIManager.Instance.Life > 0)
            {
                // Plays a "pling" sound when enemy enters
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
