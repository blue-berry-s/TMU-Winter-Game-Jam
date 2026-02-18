using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BetButtonLogic : MonoBehaviour
{
    BettingManager bettingManager;
    public bool isBetted { get; private set; }
    public string partName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bettingManager = GameObject.FindGameObjectWithTag("BlackJackManagers").GetComponentInChildren<BettingManager>();
        isBetted = false;
    }

    public void setUp(BodyPartView view)
    {
        
        TMP_Text[] texts= gameObject.GetComponent<RectTransform>().parent.GetComponentsInChildren<TMP_Text>();
        if (texts.Length < 3) {
            Debug.Log("ERROR SETTING UP TEXTS");
            return;
        }

        texts[1].text = "Quantity: " + view.getAmount();
        texts[2].text = view.getHealth().ToString();
        texts[3].text = view.getName();
        texts[4].text = "$" + view.getMoney().ToString();

        partName = view.getName();

    }


    public void betOrgan() {
        BodyPartView view = gameObject.GetComponent<RectTransform>().parent.GetComponentInChildren<BodyPartView>();
        bettingManager.betBodyPart(view);
        Button thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.RemoveAllListeners();
        thisButton.onClick.AddListener(unbetOrgan);
        thisButton.GetComponentInChildren<TMP_Text>().text = "Unbet";
        isBetted = true;
    }

    public void unbetOrgan() {
        BodyPartView view = gameObject.GetComponent<RectTransform>().parent.GetComponentInChildren<BodyPartView>();
        bettingManager.unbetBodyPart(view);
        Button thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.RemoveAllListeners();
        thisButton.onClick.AddListener(betOrgan);
        thisButton.GetComponentInChildren<TMP_Text>().text = "Bet";
        isBetted = false;
    }

    public void lockBetButton() {
        Button thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.RemoveAllListeners();
        thisButton.GetComponentInChildren<TMP_Text>().text = "Locked";
        thisButton.interactable = false;

    }

    public void disableButton() {
        Button thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.RemoveAllListeners();
        thisButton.interactable = false;
    }

    public void unlockBetButton() {
        Button thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.RemoveAllListeners();
        thisButton.onClick.AddListener(betOrgan);
        thisButton.GetComponentInChildren<TMP_Text>().text = "Bet";
        thisButton.interactable = true;
        isBetted = false;
    }

    public void updateTexts(BodyPartView view) {
        TMP_Text[] texts = gameObject.GetComponent<RectTransform>().parent.GetComponentsInChildren<TMP_Text>();
        if (texts.Length < 3)
        {
            Debug.Log("ERROR SETTING UP TEXTS");
            return;
        }

        texts[1].text = "Quantity: " + view.getAmount();
        texts[2].text = view.getHealth().ToString();
        texts[3].text = view.getName();
        texts[4].text = "$" + view.getMoney().ToString();

    }
}
