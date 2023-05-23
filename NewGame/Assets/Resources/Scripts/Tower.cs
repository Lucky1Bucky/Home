using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float _damageBullet;
    [SerializeField] private float _speedBullet;
    [SerializeField] private float _timeReload;
    [SerializeField] private float _radiusAttack;
    [Space]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private LayerMask _layerEnemy;



    public TypeEffect typeEffect;

    public enum TypeEffect
    {
        None = 0, Fire = 1, Freezing = 2, Poison = 3,
    }

    private bool _isReload;
    

    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        if (Physics.CheckSphere(transform.position, _radiusAttack, _layerEnemy))
        {
            if(_isReload == false)
            {
                StartCoroutine(_countdownReload());
                Shot();
            }

        }
    }

    private void Shot()
    {
        var tempBullet = Instantiate(_bullet, _shotPoint.position, Quaternion.identity).GetComponent<Bullet>();
        
        tempBullet.SetStats(_damageBullet, _speedBullet, (int)typeEffect, FindClosestEnemy().transform);
    
    }

    private GameObject FindClosestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radiusAttack, _layerEnemy);

        float distance = Mathf.Infinity;
        Transform currentEnemy = null;
        for (int i = 0; i < colliders.Length; i ++)
        {
            float current = Vector3.Distance(transform.position, colliders[i].transform.position);
            
            if (current < distance)
            {
                distance = current;
                currentEnemy = colliders[i].transform;
            }
        }
        if (currentEnemy != null) return currentEnemy.gameObject;
        
        else return null;
    }

    private IEnumerator _countdownReload()
    {
        _isReload = true;
        yield return new WaitForSeconds(_timeReload);
        _isReload = false;
        
    }
}
