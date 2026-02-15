using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BodyPartView : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text bodyPartText;
    [SerializeField] private Button betButton;

    private BodyPart bodyPart;
    private BettingManager bettingManager;
    private bool isLocked = false;

    private void Start()
    {
        bettingManager = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<BettingManager>();
    }
    public void setUp(BodyPart bodyPart) {
        this.bodyPart = bodyPart;
        bodyPartText.text = bodyPart.Name;
        healthText.text = "Health Cost: " + bodyPart.HealthCost.ToString();
        moneyText.text = "Money Earned: $" + bodyPart.MoneyValue.ToString();
        betButton.onClick.AddListener(betBodyPart);
    }

    public int getMoney() {
        return bodyPart.MoneyValue;
    }

    public int getHealth()
    {
        return bodyPart.HealthCost;
    }

    public string getName() {
        return bodyPart.Name;
    }

    public void betBodyPart()
    {
        Debug.Log("BETTING");
        bettingManager.betBodyPart(this);
        betButton.GetComponentInChildren<TMP_Text>().text = "UnBet";
        betButton.onClick.AddListener(unBetBodyPart);
        betButton.onClick.RemoveListener(betBodyPart);
        
    }

    public void lockBet() {
        betButton.GetComponentInChildren<TMP_Text>().text = "Locked";
        betButton.onClick.RemoveAllListeners();
        betButton.interactable = false;
        isLocked = true;
    }

    public void disableBetButton()
    {
        betButton.GetComponentInChildren<TMP_Text>().text = "Bet";
        betButton.onClick.RemoveAllListeners();
        betButton.interactable = false;
    }

    public void enableBetButton() {
        betButton.GetComponentInChildren<TMP_Text>().text = "Bet";
        betButton.onClick.RemoveAllListeners();
        betButton.onClick.AddListener(betBodyPart);
        betButton.interactable = true;
        isLocked = false;
    }

    public void unBetBodyPart()
    {
        Debug.Log("REMOVING");
        bettingManager.unbetBodyPart(this);
        betButton.GetComponentInChildren<TMP_Text>().text = "Bet";
        betButton.onClick.RemoveListener(unBetBodyPart);
        betButton.onClick.AddListener(betBodyPart);
    }

    public bool getLocked() {
        return isLocked;
    }



}
