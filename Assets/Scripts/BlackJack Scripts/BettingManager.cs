using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;
public class BettingManager : MonoBehaviour
{

    [SerializeField] private List<BodyPartData> bodyPartData;
    [SerializeField] private BodyPartView bodyView;

    private List<BodyPart> currentBodyParts;
    private List<BodyPartView> bettedBodyParts = new();
    private List<BodyPartView> allBodyParts = new();

    [SerializeField] private Transform bodyUI;

    private int betAmount = 0;
    private int betHealth = 0;
    [SerializeField] private TMP_Text betText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private ResourceManager resourceManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        addBodyParts();
        displayBodyParts();

    }

    public void addBodyParts() {
        currentBodyParts = new();
        for (int i = 0; i < bodyPartData.Count; i++)
        {
            BodyPartData data = bodyPartData[i];
            BodyPart body = new(data);
            currentBodyParts.Add(body);

        }
    }

    public void displayBodyParts()
    {
        for (int i = 0; i < currentBodyParts.Count; i++) {
            BodyPartView currentPart = Instantiate(bodyView, bodyUI);
            currentPart.setUp(currentBodyParts[i]);
            allBodyParts.Add(currentPart);
        }

    }

    public void betBodyPart(BodyPartView view) {
        bettedBodyParts.Add(view);
        updateBetDisplays();
    }

    public void unbetBodyPart(BodyPartView view) {
        bettedBodyParts.RemoveAll(x => x.getName() == view.getName());
        updateBetDisplays();
    }

    public void updateBetDisplays() {
        betAmount = calcBetAmount();
        betHealth = calcBetHealth();
        betText.text = "$" + betAmount.ToString();
        healthText.text = betHealth.ToString();
    }

    private int calcBetAmount() {
        int amount = 0;
        for (int i = 0; i < bettedBodyParts.Count; i++) {
            amount += bettedBodyParts[i].getMoney();
        }
        return amount;
    }

    private int calcBetHealth()
    {
        int amount = 0;
        for (int i = 0; i < bettedBodyParts.Count; i++)
        {
            amount += bettedBodyParts[i].getHealth();
        }
        return amount;
    }

    public void playerLost() {
        resourceManager.decPlayerHealth(betHealth);
        clearBets();
    }

    public void playerWon()
    {
        resourceManager.incPlayerMoney(betAmount);
        clearBets();
    }

    public void clearBets() {
        for (int i = 0; i < bettedBodyParts.Count; i++) {
            bettedBodyParts[i].enableBetButton();
        }
        bettedBodyParts = new();
        unlockBetting();
        updateBetDisplays();
    }


    public bool hasBets() {
        return bettedBodyParts.Count > 0;
    }

    public void lockCurrentBets() {
        for (int i = 0; i < allBodyParts.Count; i++)
        {
            allBodyParts[i].disableBetButton();
        }
        for (int i = 0; i < bettedBodyParts.Count; i++)
        {
            bettedBodyParts[i].lockBet();
        }
        
    }

    public void unlockBetting() {
        for (int i = 0; i < allBodyParts.Count; i++)
        {
            if (!allBodyParts[i].getLocked()) {
                allBodyParts[i].enableBetButton();
            };
        }
    }

    

    

}
