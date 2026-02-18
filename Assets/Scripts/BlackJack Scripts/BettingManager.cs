using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class BettingManager : MonoBehaviour
{

    private List<BodyPartView> bettedBodyParts = new();
    private List<BodyPartView> allBodyParts = new();

    [SerializeField] private Transform bodyUI;

    public int betAmount { get; private set; }
    private int betAmountPrev;
    public int betHealth { get; private set; }
    [SerializeField] private TMP_Text betText;
    [SerializeField] private TMP_Text healthText;

    BodyPartManager playerBodyInventory;
    HealthManager healthManager;

    [SerializeField] private GameObject bodyViewPrefab;
    //private List<BetButtonLogic> allBetButtons = new();
    private SideMenu betMenu;

    [SerializeField] private TMP_Text betErrorMessage;

    public int betTimes { get; private set; }

    public bool betInsured { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerBodyInventory = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<BodyPartManager>();
        betMenu = GameObject.FindGameObjectWithTag("BlackJackManagers").GetComponentInChildren<SideMenu>();
        healthManager = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<HealthManager>();

        displayBodyParts();
        betAmount = 0;
        betHealth = 0;
        betTimes = 0;
        betInsured = false;
        clearErrorMessage();

    }

    public void displayBodyParts()
    {
        List<BodyPart> currentBodyParts = playerBodyInventory.getAllPlayerParts();
        for (int i = 0; i < currentBodyParts.Count; i++) {
            BodyPartView newComp = Instantiate(bodyViewPrefab, bodyUI).GetComponentInChildren<BodyPartView>();
            newComp.setUp(currentBodyParts[i]);
            allBodyParts.Add(newComp);

        }

    }

    //This will need to be implemented differently once we allow people to be multiple of the same body parts
    public void betBodyPart(BodyPartView view) {
        bettedBodyParts.Add(view);
        updateBetDisplays();
    }

    //This will need to be implemented differently once we allow people to be multiple of the same body parts
    public void unbetBodyPart(BodyPartView view) {
        bettedBodyParts.RemoveAll(x => x.getName() == view.getName());
        updateBetDisplays();
    }

    public void lockBets() {
        //Can't bet over your current health / can't bet all your organs (or else will reach lock state when doubling down)
        if ((betAmountPrev == 0) && hasBets() && (betHealth > healthManager.getMaxHealth())) {
            setErrorMessage("Can't Bet over Max Health on first bet");
            return;
        }

        //makes sure double down/current bet is higher than previous amount
        if (hasBets() && betAmount > betAmountPrev)
        {
            clearErrorMessage();
            betTimes++;
            betMenu.callCloseMenu();
            foreach (BodyPartView b in allBodyParts)
            {
                if (b.isBetted)
                {
                    b.lockBetButton();
                }
            }
            betAmountPrev = betAmount;
            

        }
        else {
            setErrorMessage("Must bet higher than $" + betAmountPrev);
        }
        

    }

    public void unlockBets() {
        betMenu.callOpenMenu();
        foreach (BodyPartView b in allBodyParts)
        {
            if (!b.isBetted && b.getAmount() > 0)
            {
                b.unlockBetButton();
            }
        }
    }

    public void disableAllButtons() {
        foreach (BodyPartView b in allBodyParts)
        {
            b.disableButton();
        }
    }


    public void resetBets() {
        bettedBodyParts = new();
        betInsured = false;
        updateBetDisplays();
        //Clear out the body UI - might be useful once we implement destorying the organ when player looses it
        // Switch out logic (I beleive BlackJackManager still stores old version of BetButtonLogic -> that needs to get refreshed
        //while (bodyUI.childCount > 0)
        //{
        //DestroyImmediate(bodyUI.transform.GetChild(0).gameObject);
        //}
        //displayBodyParts();

        foreach (BodyPartView b in allBodyParts)
        {
            
            if (b.getAmount() > 0)
            {
                b.unlockBetButton();
            }
            else {
                b.darkenSprite();
                b.disableButton();
            }
        }
        clearBetTimes();
        betAmountPrev = 0;
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

    public bool hasBets() {
        return bettedBodyParts.Count > 0;
    }

    public void clearBetTimes() {
        betTimes = 0;
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

    public void loseBodyParts() {
        foreach (BodyPartView b in bettedBodyParts) {
            b.decAmount();
            b.updateDisplay();
        }
    }

    public void insureBet() {
        betInsured = true;
    }

    public void setErrorMessage(string message) {
        betErrorMessage.text = message;
    }

    public void clearErrorMessage() {
        betErrorMessage.text = "";
    }

    





}
