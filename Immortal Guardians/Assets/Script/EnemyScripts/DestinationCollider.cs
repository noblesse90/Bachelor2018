using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationCollider : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<EnemyController>().Release();

            FindObjectOfType<AudioManager>().Play("Lose_Life");

            UIManager.Instance.Life -= 1;
        }

    }
}
