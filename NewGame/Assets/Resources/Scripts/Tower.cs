using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float _damageBullet;
    [SerializeField] private float _speedBullet;
    [SerializeField] private float _timeReload;
    [SerializeField] private float _radiusAttack;
    [SerializeField] public int _cost;
    [Space]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform[] _shotPoints;
    [SerializeField] private LayerMask _layerEnemy;
    [Space]
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
                StartShot();
            }

        }
    }

    private void StartShot()
    {
        StartCoroutine(Shot());
    }
    
    private IEnumerator Shot()
    {
        for (int i = 0;  i < _shotPoints.Length;  i++)
        {
            var tempBullet = Instantiate(_bullet, _shotPoints[i].position, Quaternion.identity).GetComponent<Bullet>();
            tempBullet.SetStats(_damageBullet, _speedBullet, (int)typeEffect, FindClosestEnemy().transform);
            yield return new WaitForSeconds(1 + i);

        }
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
