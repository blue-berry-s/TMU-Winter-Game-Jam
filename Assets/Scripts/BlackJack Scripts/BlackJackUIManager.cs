using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlackJackUIManager : MonoBehaviour
{
    private MoneyManager moneyManager;
    private HealthManager healthManager;
    private DebtManager debtManager;

    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text roundText;
    [SerializeField] private TMP_Text debtText;
    void Start()
    {
        moneyManager = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<MoneyManager>();
        healthManager = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<HealthManager>();
        debtManager = GameObject.FindGameObjectWithTag("BlackJackManagers").GetComponentInChildren<DebtManager>();
    }

    public void updateHealthUI() {
        healthText.text = healthManager.getPlayerHealth().ToString();
    }
    public void updateMoneyUI()
    {
        moneyText.text = moneyManager.getPlayerMoney().ToString();
    }

    public void updateAllText() {
        updateHealthUI();
        updateMoneyUI();
        updateRoundText();
    }

    public void updateRoundText() {
        roundText.text = debtManager.getCurrentRound().ToString() + "/" + debtManager.getMaxRound().ToString();
    }

    public void setDebtText() {
        debtText.text = "DEBT: $" + debtManager.getCurrentDebt().ToString();
    }

}
