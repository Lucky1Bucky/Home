using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _damage;
    private float _speed;
    private int _effectID;
    private Transform _target;


    public void SetStats(float damage, float speed, int effectID,Transform target)
    {
        _damage = damage;
        _speed = speed;
        _effectID = effectID;
        _target = target;
    }


    void FixedUpdate()
    {
        if(_target == null)
        {
            Kill();
            return;
        }


        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _target.position) < 1 )
        {
            if(_target.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_damage, _effectID);
                Kill();
            }
        }
    }


    public void Kill() => Destroy(gameObject);

}
