using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    [SerializeField] public int HP;
    [SerializeField] public int CoinCount;


    public static PlayerProgress Instance;
    private void Awake() => Instance = this;

    void Update()
    {

    }

    public void TakeDamage(int Damage)
    {
        HP -= Damage;
        TowerDamageColor.Instance.StartColorDamage();
        if (HP <= 0)
        {
            Debug.Log("Dead");
        }

    }

    public bool IsBuy(int price)
    {
        if (CoinCount >= price)
        {
            CoinCount -= price;
            return true;
        }
        else
        {
            return false;
        }
    }



    ///Need to do 5 different Towers and 3 enemies. Need price of tower
}
