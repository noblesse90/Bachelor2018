using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTProjectile : MonoBehaviour {

    private GameObject _target;

    private float _speed = 0;

    private int _damage = 0;

    private Vector3 _targetPos;
    private bool _shot = false;

    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    // Update is called once per frame
    private void Update () {
        MoveToTarget();
	}

    private void MoveToTarget()
    {
        if(_target != null && _target.activeInHierarchy)
        {           
            _targetPos = _target.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, Time.deltaTime * _speed);

            Vector2 dir = _target.transform.position - transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            Explode();
        }
        else
        {
            _target = null;
            Release();
        }
        
        
    }

    private void Explode()
    {
        if (!(_shot) && Vector2.Distance(transform.position,_targetPos) < 1)
        {

            _target.GetComponent<EnemyController>().TakeDamage(_damage); 
            
            GameObject aniPrefab = ObjectPool.Instance.GetObject("ArrowExplosion");
            aniPrefab.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
			
            _shot = true;
            Release();
        }
    }

    public void SetTargetAndSpeed(GameObject target, float speed)
    {
        this._target = target;
        this._speed = speed;
    }

    private void Release()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
        gameObject.transform.position = gameObject.transform.parent.transform.position;
        _shot = false;
    }
}
