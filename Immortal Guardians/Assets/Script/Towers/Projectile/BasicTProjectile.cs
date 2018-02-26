using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTProjectile : MonoBehaviour {

    private GameObject _target;

    private float _speed = 0;

    private int _damage = 0;

    public int Damage
    {
        set
        {
            _damage = value;
        }
    }


    // Update is called once per frame
    private void Update () {
        MoveToTarget();
	}

    private void MoveToTarget()
    {
        if(_target != null && _target.activeInHierarchy)
        {           
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * _speed);

            Vector2 dir = _target.transform.position - transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            
            transform.rotation
                 = Quaternion.AngleAxis(angle, Vector3.forward);

        }
        else
        {
            _target = null;
            ObjectPool.Instance.ReleaseObject(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_target.activeSelf) return;
        if (!collision.GetComponent<Collider2D>().Equals(_target.GetComponent<Collider2D>())) return;
        _target.GetComponent<EnemyController>().TakeDamage(_damage);
        
        GameObject aniPrefab = ObjectPool.Instance.GetObject("ArrowExplosion");
        aniPrefab.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
        
        ObjectPool.Instance.ReleaseObject(gameObject);

    }

    public void SetTargetAndSpeed(GameObject target, float speed)
    {
        this._target = target;
        this._speed = speed;
    }
}
