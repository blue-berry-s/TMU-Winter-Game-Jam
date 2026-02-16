using UnityEngine;

public class HealthManager : MonoBehaviour
{

    [SerializeField] private int maxPlayerHealth = 30;
    private int playerHealth;
    void Start()
    {
        playerHealth = maxPlayerHealth;

    }


    public void incPlayerHealth(int amount)
    {
        if (playerHealth + amount > maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
        }
        else
        {
            playerHealth += amount;
        }
    }

    public void decPlayerHealth(int amount)
    {
        if (playerHealth - amount <= 0)
        {
            Debug.Log("You DIED");
        }
        else
        {
            playerHealth -= amount;
        }
    }

    public int getPlayerHealth() {
        return playerHealth;
    }

    public void incPlayerMaxHealth(int amount) {
        maxPlayerHealth += amount;
    }

    public void decPlayerMaxHealth(int amount) {
        maxPlayerHealth -= amount;
    }
}
