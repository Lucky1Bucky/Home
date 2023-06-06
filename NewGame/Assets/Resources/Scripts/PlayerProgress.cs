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

    public bool IsBuy(int price)
    {
        if (CoinCount > price)
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
