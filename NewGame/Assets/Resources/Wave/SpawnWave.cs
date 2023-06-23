using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnWave : MonoBehaviour
{

    [SerializeField] private int _currentWave;
    [SerializeField] private Button _buttonStartWave;
    [SerializeField] private WaveSettings _waveSettings;
    [Space]
    [SerializeField] private Transform[] _pointsSpawn;

    private int _enemyAlive;


    public void StartWave()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1);

        _enemyAlive = 0;

        for (int i = 0; i < _waveSettings.Waves[_currentWave].Enemys.Length; i++)
        {
            _enemyAlive += _waveSettings.Waves[_currentWave].Enemys.Length;
        }
        for (int i = 0; i < _waveSettings.Waves[_currentWave].Enemys[i].EnemyCount; i++)
        {
            for(int k = 0; k < _waveSettings.Waves[_currentWave].Enemys[i].EnemyCount; k++)
            {
                var point = RandomPoints();
                var enemy = Instantiate(_waveSettings.Waves[_currentWave].Enemys[i].EnemyPreFab, point.position, Quaternion.identity).GetComponent<Enemy>();
                enemy.SetPoint(point.parent);
                yield return new WaitForSeconds(1);
            }
            
        }


    }

    private Transform RandomPoints()
    {
        return _pointsSpawn[Random.Range(0, _pointsSpawn.Length)];
    }
}
