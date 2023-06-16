using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _imageHP;
    [SerializeField] private Transform _cameraTranform;
    [SerializeField] private Transform _healthBar;


    private Transform _cameraTransform;



    private void Start()
    {
        _cameraTranform = Camera.main.transform;
    }

    public void RefreshHealthBar(float currntHP, float maxHP)
    {
        _imageHP.fillAmount = currntHP / maxHP;
    }

    private void Update()
    {
        _healthBar.LookAt(_cameraTranform.position);
    }
}
