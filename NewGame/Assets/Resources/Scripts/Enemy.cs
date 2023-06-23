using System.Collections;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _speedMove;
    [SerializeField] private int _isDeadDamage;
    [SerializeField] private GameObject _burnFX;
    [SerializeField] private GameObject _freezingFX;
    [SerializeField] private GameObject _poisonFX;

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
    }

    void FixedUpdate()
    {
        Move(); 
    }

    private void Move()
    {
        if (_points[_currentMovePoints] != null)
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


    public void TakeDamage(float damage, int effectID = 0)
    {

        _health -= _isPoison ? damage /0.8f :damage;
        _healthBar.RefreshHealthBar(_health, _maxHealth);

        switch (effectID)
        {
            case 1: StartCoroutine(Burn(damage)); break;
            case 2: StartCoroutine(Freezing()); break;
            case 3: Poison(); break;

        }


        CheckIsDead();
    }

    private IEnumerator Burn (float damage)
    {
        if(_isBurn == false)
        {
            _isBurn = true;
            _burnFX.SetActive(true);
            for (int i = 0; i < Random.Range(5,8); i++)
            {
                TakeDamage(damage / 2, 0);
                yield return new WaitForSeconds(Random.Range(1,3));
            }

            _isBurn = false;
            _burnFX.SetActive(false);

        }

    }

    private IEnumerator Freezing()
    {
        
        if (_isFreezing == true)
        {
            _isFreezing = true;
            _freezingFX.SetActive(true);
            _agent.speed *= 0.8f;


            yield return new WaitForSeconds(Random.Range(0, 8));


            _agent.speed /= 0.8f;
            _isFreezing = false;
            _freezingFX.SetActive(false);
        }
       
    }

    private void Poison()
    {
        _isPoison = true;
        _poisonFX.SetActive(true);
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
