using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _speedMove;
    [SerializeField] Transform _tempPoints;


    private Transform[] _points;
    private NavMeshAgent _agent;
    private int _currentMovePoints;
    private float _health; 



    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _health = _maxHealth;
        SetPoint(_tempPoints);
    }

    void FixedUpdate()
    {
        Move(); 
    }

    private void Move()
    {
        _agent.SetDestination(_points[_currentMovePoints].position);

        if (Vector3.Distance(transform.position, _points[_currentMovePoints].position) < 1)
        {
            _currentMovePoints++;
        }

        if(_currentMovePoints >= _points.Length)
        {
            Kill();
        }

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

    public void SetPoint(Transform pointsParent)
    {
        _points = new Transform[pointsParent.childCount];
        for (int i = 0; i < _points.Length; i++)
        {
            _points[i] = pointsParent.GetChild(i).transform;
        }
    }

    public void Kill() => Destroy(gameObject);
}
