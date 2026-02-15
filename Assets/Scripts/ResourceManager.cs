using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private int MaxplayerHealth = 30;
    private int playerHealth;
    [SerializeField] private int playerMoney = 0;

    [SerializeField] private TMP_Text playerHealthText;
    [SerializeField] private TMP_Text playerMoneyText;

    void Start() {
        playerHealth = MaxplayerHealth;
        updateDisplays();
    }


    public void incPlayerHealth(int amount) {
        if (playerHealth + amount > MaxplayerHealth)
        {
            playerHealth = MaxplayerHealth;
        }
        else {
            playerHealth += amount;
        }
        updateDisplays();
    }

    public void decPlayerHealth(int amount) {
        if (playerHealth - amount <= 0)
        {
            Debug.Log("You DIED");
        }
        else {
            playerHealth -= amount;
        }
        updateDisplays();
    }

    public void incPlayerMoney(int amount) {
        playerMoney += amount;
        updateDisplays();
    }

    public bool decPlayerMoney(int amount) {
        if (playerMoney - amount < 0)
        {
            return false;
        }
        else {
            playerMoney -= amount;
            updateDisplays();
            return true;
        }
        
    }

    private void updateDisplays() {
        playerHealthText.text = playerHealth.ToString();
        playerMoneyText.text = playerMoney.ToString();
    }
}
