using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationCollider : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<EnemyController>().Release();


            if (UIManager.Instance.Life > 0)
            {
                AudioManager.Instance.Play("Lose_Life");
                
                UIManager.Instance.Life -= 1;
            }
            
        }

    }
}
