using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerDamageColor : MonoBehaviour
{
    [SerializeField] private Material _materialOff;
    [SerializeField] private Material _materialOn;

    private MeshRenderer _meshRenderer;

    public static TowerDamageColor Instance;

    void Start()
    {
        Instance = this;
        _meshRenderer = GetComponent<MeshRenderer>();
        SetMaterial(_materialOff);  
    }

    public void StartColorDamage()
    {
        StartCoroutine(ColorDamage());
    }
    
    private IEnumerator ColorDamage()
    {
        SetMaterial(_materialOn);
        yield return new WaitForSeconds(0.3f);
        SetMaterial(_materialOff);
    }


    private void SetMaterial(Material material) => _meshRenderer.material = material;
}
