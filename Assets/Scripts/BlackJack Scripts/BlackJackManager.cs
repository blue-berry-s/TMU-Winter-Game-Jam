using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Splines;

public class BlackJackManager : MonoBehaviour
{
    // This is mostly to help with the animation of dealer cards (this should not be used in logic)
    [SerializeField] int maxDealerHandSize = 10;

    private int maxPlayerHandSize = 10;

    // Pull all player cards from Card Manager - this now becomes the source of truth for cards
    private List<Card> currentWaveDeck = new();

    //Connect to UI (should probably move to its own thing)
    [SerializeField] private Button standButton;
    [SerializeField] private Button hitButton;
    [SerializeField] private Button startButton;
    [SerializeField] private Button doubleDownButton;

    [SerializeField] private SplineContainer dealerSplineContainer;
    [SerializeField] private SplineContainer playerSplineContainer;



    private List<GameObject> playerCards = new();
    private List<GameObject> dealerCards = new();
    private List<Card> DiscardedCards = new();

    private CardsVisualManager cardVisualManager;
    private BettingManager bettingManager;
    private CardManager playerFullDeck;
    private MoneyManager moneyManager;
    private HealthManager healthManager;
    private BlackJackUIManager uiManager;
    private DebtManager debtManager;

    private Coroutine stand;
    private bool roundOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        playerFullDeck = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<CardManager>();
        moneyManager = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<MoneyManager>();
        healthManager = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<HealthManager>();

        bettingManager = GameObject.FindGameObjectWithTag("BlackJackManagers").GetComponentInChildren<BettingManager>();
        cardVisualManager = GameObject.FindGameObjectWithTag("BlackJackManagers").GetComponentInChildren<CardsVisualManager>();
        uiManager = GameObject.FindGameObjectWithTag("BlackJackManagers").GetComponentInChildren<BlackJackUIManager>();
        debtManager = GameObject.FindGameObjectWithTag("BlackJackManagers").GetComponentInChildren<DebtManager>();

        uiManager.setDebtText();
        uiManager.updateAllText();
        bettingManager.unlockBets();

        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(startRound);
        disableStartButton();
        addPlayerDeckToRound();
        disablePlayButtons();
    }

    private void Update()
    {
        bool betted = bettingManager.hasBets();
        if (betted && playerCards.Count == 0)
        {
            enableStartButton();
        }
        else if (!betted && playerCards.Count == 0)
        {
            disableStartButton();
        }
        else if (bettingManager.betTimes == 2) {
            bettingManager.clearBetTimes();
            PlayerDrawCard();
            if (isBust(playerCards))
            {
                flipDealerCard();
                compareValues();

            }
            else {
                callPlayerStand();
            }
        }
    }

    //Set up the cards 
    public void addPlayerDeckToRound() {
        if (playerFullDeck.deckEmpty()) {
            playerFullDeck.setUpDeck();
        }
        List<Card> allCards = playerFullDeck.getPlayerCards();
        for (int i = 0; i < allCards.Count; i++)
        {
            currentWaveDeck.Add(allCards[i]);
        }

    }

    public void PlayerDrawCard() {
        Card card = DrawCard();
        GameObject obj = cardVisualManager.spawnCard(false, card);
        playerCards.Add(obj);
        cardVisualManager.DrawCardAnimation(playerCards, maxPlayerHandSize, playerSplineContainer);

        bool busted = isBust(playerCards);
        if (busted)
        {
            flipDealerCard();
            if (stand != null)
            {
                StopCoroutine(stand);
                stand = null;
            }
            StartCoroutine(compareValues());

        }
    }

    public Card DrawCard() {
        Card card = currentWaveDeck[Random.Range(0, currentWaveDeck.Count)];
        currentWaveDeck.Remove(card);
        return card;
    }


    public void DealerDrawCard(bool flipped) {
        Card card = DrawCard();
        GameObject obj = cardVisualManager.spawnCard(flipped, card);
        dealerCards.Add(obj);
        cardVisualManager.DrawCardAnimation(dealerCards, maxDealerHandSize, dealerSplineContainer);
        if (isBust(dealerCards)) {
            flipDealerCard();
            StartCoroutine(compareValues());
        }
    }

    public void discardPlayerCard() {
        DiscardedCards.Add(playerCards[playerCards.Count - 1].GetComponent<CardView>().getCard());
        StartCoroutine( cardVisualManager.DiscardCardAnimation(playerCards[playerCards.Count - 1], 0.25f));
    }


    private IEnumerator setUpDealer() {
        DealerDrawCard(false);

        yield return new WaitForSeconds(0.15f);
        DealerDrawCard(true);


    }

    public void startRound() {
        //Handle UI Elements
        enablePlayButtons();
        disableStartButton();

        //Game logic (player draws + dealer gets card)
        PlayerDrawCard();
        StartCoroutine(setUpDealer());
        PlayerDrawCard();

        //Handle Start Button UI
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(callResetBoard);
        startButton.GetComponentInChildren<TMP_Text>().text = "Next Round";
    }

    //This is needed because a button needs this (Buttons can't have IEnumerator)
    public void callPlayerStand() {
        stand = StartCoroutine(playerStand());
    }

    private IEnumerator playerStand()
    {
        disablePlayButtons();
        disableStartButton();
        bettingManager.disableAllButtons();

        flipDealerCard();

        int dealerValue = calcValue(dealerCards);
        bool playerBusted = isBust(playerCards);
        bool busted = isBust(dealerCards);

        if (busted)
        {
            StartCoroutine(compareValues());
            if (stand != null)
            {
                StopCoroutine(stand);
                stand = null;
            }
            yield break;
        }
        else if (playerBusted) {
            StartCoroutine(compareValues());
            if (stand != null)
            {
                StopCoroutine(stand);
                stand = null;
            }
            yield break;
        }
        else
        {
            while (dealerValue < 17 && !busted)
            {
                DealerDrawCard(false);
                dealerValue = calcValue(dealerCards);
                yield return new WaitForSeconds(1f);
            }

            StartCoroutine(compareValues());
        }

        
    }

    public void playerDoubleDown() {
        bettingManager.unlockBets();
    }



    //This can probably become its own thing
    public void flipDealerCard()
    {
        // Once we add flipping aniamtion lets put this under cardVisualManger
        foreach (GameObject c in dealerCards) {
            CardView view = c.GetComponentInChildren<CardView>();
            if (view.getFlipped()) {
                view.flip();
            }
        }
    }


    IEnumerator compareValues()
    {
        if (!roundOver) {
            setRoundOver();
            int playerValue = calcValue(playerCards);
            int dealerValue = calcValue(dealerCards);

            if (playerValue > 21)
            {
                playerLost("Player has busted");
            }
            else if (dealerValue > 21)
            {
                playerWon("Dealer has busted");
            }
            else if (playerValue < dealerValue)
            {
                playerLost("Less than dealer");
            }
            else if (playerValue > dealerValue)
            {
                playerWon("More than dealer");
            }
            else
            {
                uiManager.updateRoundText("It's a Tie");
            }

            yield return new WaitForSeconds(0.5f);

            debtManager.nextRound();
            disablePlayButtons();
            enableStartButton();

            roundOver = false;
        }
        

    }


    public int calcValue(List<GameObject> cards) {
        List<int> cardValues = new List<int>();
        for (int i = 0; i < cards.Count; i++) {
            CardView card = cards[i].GetComponent<CardView>();
            if (!card.getFlipped()) {
                cardValues.Add(card.getValue());
            }
            
        }

        int value = cardValues.Sum();
        if (value > 21 && cardValues.Contains(11))
        {
            for (int i = 0; i < cardValues.Count; i++)
            {
                if (cardValues[i] == 11)
                {
                    cardValues[i] = 1;
                }
            }
            return cardValues.Sum();
        }
        else {
            return value;
        }
    }

    public bool isBust(List<GameObject> cards) {
        int value = calcValue(cards);
        return value > 21;
    }

    //needed because it will be reference by buttons
    public void callResetBoard() {
        StartCoroutine(resetBoard());
    }

    public IEnumerator resetBoard() {
        uiManager.hideRoundText();
        disableStartButton();
        bettingManager.resetBets();

        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(startRound);
        startButton.GetComponentInChildren<TMP_Text>().text = "Start";

        // Complete the visuals
        yield return StartCoroutine(cardVisualManager.DiscardDeckAnimation(playerCards));
        yield return StartCoroutine(cardVisualManager.DiscardDeckAnimation(dealerCards));

        addToDiscardPile();
        uiManager.updateAllText();
        bettingManager.unlockBets();


    }

    private void addToDiscardPile() {
        foreach (GameObject o in playerCards) {
            DiscardedCards.Add(o.GetComponent<CardView>().getCard());
            Destroy(o);
        }

        foreach (GameObject o in dealerCards) {
            DiscardedCards.Add(o.GetComponent<CardView>().getCard());
            Destroy(o);
        }
        playerCards = new();
        dealerCards = new();
    }

    public void playerLost(string reason) {
        if (bettingManager.betInsured) {
            moneyManager.incPlayerMoney(Mathf.RoundToInt(bettingManager.betAmount * 0.5f));
        }
        healthManager.decPlayerHealth(bettingManager.betHealth);
        bettingManager.loseBodyParts();
        uiManager.updateAllText();
        uiManager.updateRoundText("player Lost: " + reason);
    }

    public void playerWon(string reason)
    {
        moneyManager.incPlayerMoney(bettingManager.betAmount);
        uiManager.updateAllText();
        uiManager.updateRoundText("player Won: " + reason);
         
    }

    public void disablePlayButtons() {
        standButton.interactable = false;
        hitButton.interactable = false;
        doubleDownButton.interactable = false;
    }

    public void enablePlayButtons() {
        standButton.interactable = true;
        hitButton.interactable = true;
        doubleDownButton.interactable = true;
    }

    public void disableStartButton() {
        startButton.interactable = false;
    }

    public void enableStartButton() {
        startButton.interactable = true;
    }


    public List<Card> getCurrentDeck() {
        return currentWaveDeck;
    }

    public List<Card> getDiscardDeck() {
        return DiscardedCards;
    }

    public int playerCardCount() {
        return playerCards.Count;
    }

    public int dealerCardCount()
    {
        return dealerCards.Count;
    }

    private void setRoundOver() {
        roundOver = true;
    }


}
