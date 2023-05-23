using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _speedMove;

    private float _health; 



    void Start()
    {
        _health = _maxHealth;
    }

    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        CheckIsDead();
    }

    public void CheckIsDead()
    {
        if (_health < 0)
        {
            Kill();
        }
    }

    public void Kill() => Destroy(gameObject);
}
