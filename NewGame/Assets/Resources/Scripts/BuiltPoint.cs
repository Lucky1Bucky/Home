using UnityEngine;

public class BuiltPoint : MonoBehaviour
{
    [SerializeField] private Material _materialOff;
    [SerializeField] private Material _materialOn;

    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        SetMaterial(_materialOff);
    }
    private void OnMouseDown()
    {
        Store.Instance.BuyTower(transform); 
    }

    private void OnMouseEnter()
    {
        SetMaterial(_materialOn);
    }

    private void OnMouseExit()
    {
        SetMaterial(_materialOff);
    }

    private void SetMaterial(Material material) => _meshRenderer.material = material;



}
