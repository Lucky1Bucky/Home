using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private Text _textHP;
    [SerializeField] private Text _textCoinCount;

    public static StatsUI Instance;

    private void Awake() => Instance = this;


    private void Start() => RefreshUI();


    public void RefreshUI()
    {

        _textHP.text = $"Health: {PlayerProgress.Instance.HP}";
        _textCoinCount.text = $"Coin: {PlayerProgress.Instance.CoinCount}";
    }
}
