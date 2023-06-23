using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Wave
{
    [SerializeField] public OneWave[] Enemys;


    [System.Serializable]
    public class OneWave
    {
        [SerializeField] public GameObject EnemyPreFab;
        [SerializeField] public int EnemyCount;
    }
}
