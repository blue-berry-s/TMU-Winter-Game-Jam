using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlackJackUIManager : MonoBehaviour
{
    private MoneyManager moneyManager;
    private HealthManager healthManager;

    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text moneyText;
    void Start()
    {
        moneyManager = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<MoneyManager>();
        healthManager = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<HealthManager>();
    }

    public void updateHealthUI() {
        healthText.text = healthManager.getPlayerHealth().ToString();
    }
    public void updateMoneyUI()
    {
        moneyText.text = moneyManager.getPlayerMoney().ToString();
    }
}
