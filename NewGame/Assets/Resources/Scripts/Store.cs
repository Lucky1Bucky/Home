using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] private Tower[] _towerPrefabs;
    [SerializeField] private Transform _buildPointsParent;

    public static Store Instance;
    private int _selectTowerID;

    private void Awake() => Instance = this;

    private void Start()
    {
        SetActiveBuiltPointsParent(false);
    }

    public void BuyTower(Transform buildpoint)
    {
        if (PlayerProgress.Instance.IsBuy(_towerPrefabs[_selectTowerID]._cost))
        {
            Instantiate(_towerPrefabs[_selectTowerID], buildpoint.position, Quaternion.identity);
            buildpoint.gameObject.SetActive(false);
            SetActiveBuiltPointsParent(false);
        }

    }
    public void SelectTowerID(int ID)
    {
        _selectTowerID = ID;
        SetActiveBuiltPointsParent(true);
    }

    private void SetActiveBuiltPointsParent(bool active) => _buildPointsParent.gameObject.SetActive(active);

}
