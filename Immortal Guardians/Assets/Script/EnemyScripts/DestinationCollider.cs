using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationCollider : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<EnemyController>().Release();

            UIManager.Instance.Life -= 1;
            if (UIManager.Instance.Life == 0)
            {
                Debug.Log("YOU LOST!");
                Time.timeScale = 0;
            }
        }

    }
}
