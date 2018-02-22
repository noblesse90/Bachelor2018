using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    private bool canSpawn = true;
    private float spawnTimer;
    [SerializeField] private float cooldownTimer;


    // Update is called once per frame
    void Start () {
	}

    private void LateUpdate()
    {
        spawn();
    }


    private void spawn()
    {
        if (!canSpawn)
        {
            spawnTimer += Time.deltaTime;

            if(spawnTimer >= cooldownTimer)
            {
                canSpawn = true;
                spawnTimer = 0;
            }
        }

        if (canSpawn)
        {
            GameObject enemy = ObjectPool.Instance.GetObject("Enemy");
            enemy.transform.position = transform.position;

            canSpawn = false;
        }

        

    }
}
