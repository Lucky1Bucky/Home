using System.Collections;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _speedMove;
    [SerializeField] Transform _tempPoints;
    [SerializeField] private int _isDeadDamage;

    private HealthBar _healthBar;
    private Transform[] _points;
    private NavMeshAgent _agent;
    private int _currentMovePoints;
    private float _health;

    private bool _isBurn;
    private bool _isFreezing;
    private bool _isPoison;



    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _healthBar = GetComponent<HealthBar>();
        _health = _maxHealth;
        _healthBar.RefreshHealthBar(_health, _maxHealth);
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
            PlayerProgress.Instance.TakeDamage(_isDeadDamage);
            Kill();
        }

    }


    public void TakeDamage(float damage, int effectID)
    {
        _health -= damage;
        _healthBar.RefreshHealthBar(_health, _maxHealth);

        switch (effectID)
        {
            case 1: StartCoroutine(Burn()); break;
            case 2: StartCoroutine(Freezing()); break;
            case 3: Poison(); break;

        }


        CheckIsDead();
    }

    private IEnumerator Burn()
    {
        _isBurn = true;
        yield return null;
        _isBurn = false;
    }

    private IEnumerator Freezing()
    {
        if (_isFreezing == true)
        {
            _isFreezing = true;
            _agent.speed *= 0.8f;


            yield return new WaitForSeconds(Random.Range(0, 8));


            _agent.speed /= 0.8f;
            _isFreezing = false;
        }
       
    }

    private void Poison()
    {
        _isPoison = true;
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

    public void Kill()
    {
        Destroy(gameObject);
    }
}
