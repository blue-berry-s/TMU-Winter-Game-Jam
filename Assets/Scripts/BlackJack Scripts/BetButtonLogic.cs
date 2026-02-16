using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BetButtonLogic : MonoBehaviour
{
    BettingManager bettingManager;
    public bool isBetted { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bettingManager = GameObject.FindGameObjectWithTag("BlackJackManagers").GetComponentInChildren<BettingManager>();
        isBetted = false;
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
        isBetted = false;
    }
}
