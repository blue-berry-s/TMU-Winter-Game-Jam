using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BodyPartView : MonoBehaviour
{

    private BodyPart bodyPart;

    private Sprite sprite;

    BettingManager bettingManager;
    public bool isBetted { get; private set; }
    public string partName;
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text quantityText;
    [SerializeField] Image organIcon;

    [SerializeField] Button button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bettingManager = GameObject.FindGameObjectWithTag("BlackJackManagers").GetComponentInChildren<BettingManager>();
        isBetted = false;
    }

    public void setUp(BodyPart bodyPart)
    {
        this.bodyPart = bodyPart;
        sprite = bodyPart.sprite;
        organIcon.sprite = sprite;
        updateDisplay();

    }


    public void betOrgan()
    {
        bettingManager.betBodyPart(this);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(unbetOrgan);
        button.GetComponentInChildren<TMP_Text>().text = "Unbet";
        isBetted = true;
    }

    public void unbetOrgan()
    {
       
        bettingManager.unbetBodyPart(this);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(betOrgan);
        button.GetComponentInChildren<TMP_Text>().text = "Bet";
        isBetted = false;
    }

    public void lockBetButton()
    {
        button.onClick.RemoveAllListeners();
        button.GetComponentInChildren<TMP_Text>().text = "Locked";
        button.interactable = false;

    }

    public void disableButton()
    {
        button.onClick.RemoveAllListeners();
        button.interactable = false;
    }

    public void unlockBetButton()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(betOrgan);
        button.GetComponentInChildren<TMP_Text>().text = "Bet";
        button.interactable = true;
        isBetted = false;
    }

    public void updateDisplay()
    {
        quantityText.text = "Quantity: " + bodyPart.Amount;
        healthText.text = bodyPart.HealthCost.ToString();
        moneyText.text = "$" + bodyPart.MoneyValue.ToString();
        organIcon.sprite = bodyPart.sprite;

    }

    public void darkenSprite()
    {
        organIcon.color = new Color32(127, 127, 127, 255);
    }

    public void lightenSprite()
    {
        organIcon.color = new Color32(255, 255, 255, 255);
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

    public int getAmount() {
        return bodyPart.Amount;
    }

    public void incAmount() {
        bodyPart.Amount++;
    }

    public void decAmount() {
        bodyPart.Amount--;
    }

    public BodyPart getBodyPart() {
        return bodyPart;
    }

    public Sprite getSprite() {
        return sprite;
    }

}
