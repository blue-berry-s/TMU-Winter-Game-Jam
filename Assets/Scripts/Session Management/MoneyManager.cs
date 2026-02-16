using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private int playerMoney = 0;

    public void incPlayerMoney(int amount)
    {
        playerMoney += amount;
    }

    public bool decPlayerMoney(int amount)
    {
        if (playerMoney - amount < 0)
        {
            return false;
        }
        else
        {
            playerMoney -= amount;
            return true;
        }

    }

    public int getPlayerMoney() {
        return playerMoney;
    }
}
