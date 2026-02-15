using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;
using TMPro;

public class BlackJackManager : MonoBehaviour
{
    [SerializeField] private CardView cardView;


    private int maxPlayerHandSize = 3;

    [SerializeField] private SplineContainer playerSplineContainer;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform discardPoint;


    private int maxDealerHandSize = 10;
    [SerializeField] private SplineContainer dealerSplineContainer;


    private List<Card> shuffledDeck = new();

    //Connect to UI (should probably move to its own thing)
    [SerializeField] private Button standButton;
    [SerializeField] private Button hitButton;
    [SerializeField] private Button startButton;

    private List<GameObject> playerCards = new();
    private List<GameObject> dealerCards = new();

    private BettingManager bettingManager;

    private List<Card> DiscardedCards = new();

    private CardManager playerFullDeck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        bettingManager = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<BettingManager>();

        playerFullDeck = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<CardManager>();

        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(startRound);
        disableStartButton();
        addDeck();
    }

    private void Update()
    {
        
        if (bettingManager.hasBets() && playerCards.Count == 0)
        {
            
            enableStartButton();
        }
        else if (!bettingManager.hasBets() && playerCards.Count == 0)
        {
     
            disableStartButton();
        }
    }

    public List<GameObject> getPlayerCards() {
        return playerCards;
    }

    public List<GameObject> getDealerCards()
    {
        return dealerCards;
    }

    //Set up the cards
    public void addDeck() {
        if (playerFullDeck.deckEmpty()) {
            playerFullDeck.setUpDeck();
        }
        List<Card> allCards = playerFullDeck.getPlayerCards();
        for (int i = 0; i < allCards.Count; i++)
        {
            shuffledDeck.Add(allCards[i]);
        }

    }

    public void shuffleDeck() {
        if (shuffledDeck.Count < 3) {
            List<Card> allCards = playerFullDeck.getPlayerCards();
            for (int i = 0; i < allCards.Count; i++)
            {
                shuffledDeck.Add(allCards[i]);
            }
        }
    }

    public void PlayerDrawCard() {
        bool busted = isBust(playerCards);
        if (playerCards.Count < maxPlayerHandSize && shuffledDeck.Count > 0 && !(busted))
        {
            playerCards.Add(drawCard().gameObject);
            UpdateCardPositions(playerCards, maxPlayerHandSize, playerSplineContainer);
            if (isBust(playerCards))
            {
                disablePlayButtons();
                compareValues();
            }
            else if(playerCards.Count == maxPlayerHandSize)
            {
                StartCoroutine(playerStand());
            }
        }
        else if (busted) {
            disablePlayButtons();
            compareValues();
        }
        if (playerCards.Count == 2)
        {
            bettingManager.unlockBetting();
        }
        else {
            bettingManager.lockCurrentBets();
        }
    }

    public void DealerDrawCard() {
        bool busted = isBust(dealerCards);
        if (dealerCards.Count < maxDealerHandSize && shuffledDeck.Count > 0 && !busted)
        {
            dealerCards.Add(drawCard().gameObject);
            UpdateCardPositions(dealerCards, maxDealerHandSize, dealerSplineContainer);
        }
        else if (busted)
        {
            compareValues();
        }
        else {
            Debug.Log("Over Count/ No More cards");
        }
        
    }


    private IEnumerator setUpDealer() {
        DealerDrawCard();
        yield return new WaitForSeconds(0.15f);
        GameObject card = drawCard(true).gameObject;
        dealerCards.Add(card);
        UpdateCardPositions(dealerCards, maxDealerHandSize, dealerSplineContainer);
    }

    public CardView drawCard(bool flipped) {
        if (flipped)
        {
            Card drawnCard = shuffledDeck[Random.Range(0, shuffledDeck.Count)];
            shuffledDeck.Remove(drawnCard);
            CardView view = Instantiate(cardView, spawnPoint.position, spawnPoint.rotation);
            view.Setup(drawnCard, flipped);
            return view;
        }
        else {
            return drawCard();
        }
    }

    public CardView drawCard() {
        Card drawnCard = shuffledDeck[Random.Range(0, shuffledDeck.Count)];
        shuffledDeck.Remove(drawnCard);
        CardView view = Instantiate(cardView, spawnPoint.position, spawnPoint.rotation);
        view.Setup(drawnCard);
        return view;
    }

    private void UpdateCardPositions(List<GameObject> handCardCount, int maxHandSize, SplineContainer splineContainer) {
        if (handCardCount.Count == 0) return;
        float cardSpacing = 1f / maxHandSize;
        float firstCardPosition = 0.5f - (handCardCount.Count - 1) * cardSpacing / 2;
        Spline spline = splineContainer.Spline;
        for (int i = 0; i < handCardCount.Count; i++) {
            float p = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, forward).normalized);
            handCardCount[i].transform.DOMove(splinePosition, 0.25f);
            handCardCount[i].transform.DOLocalRotateQuaternion(rotation, 0.25f);


        }
    }

    public void startRound() {
        enablePlayButtons();
        disableStartButton();
        PlayerDrawCard();
        StartCoroutine(setUpDealer());
        bettingManager.lockCurrentBets();
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(resetBoard);
        startButton.GetComponentInChildren<TMP_Text>().text = "Next Round";

    }


    public void callPlayerStand() {
        StartCoroutine(playerStand());
    }
    private IEnumerator playerStand()
    {
        disablePlayButtons();
        disableStartButton();
        bettingManager.lockCurrentBets();

        dealerCards[dealerCards.Count - 1].GetComponent<CardView>().flip();

        int dealerValue = calcValue(dealerCards);
        int playerValue = calcValue(playerCards);
        bool busted = isBust(dealerCards);

        while (dealerValue < 17 && !busted)
        {
            DealerDrawCard();
            dealerValue = calcValue(dealerCards);
            busted = isBust(dealerCards);

            if (busted || dealerValue > playerValue || dealerValue == 21)
            {
                compareValues();
                yield break;
            }

            yield return new WaitForSeconds(1f);
        }

        compareValues();
    }


    public void compareValues()
    {
        int playerValue = calcValue(playerCards);
        int dealerValue = calcValue(dealerCards);

        if (playerValue > 21)
        {
            playerLost("Player has busted");
        }
        else if (dealerValue > 21) {
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
        else {
            Debug.Log("It's a Tie");
        }
        enableStartButton();

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


    public void resetBoard() {
        disableStartButton();
        bettingManager.clearBets();
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(startRound);
        startButton.GetComponentInChildren<TMP_Text>().text = "Start";
        StartCoroutine(deleteCards(1f));
        shuffleDeck();
    }

    IEnumerator deleteCards(float time) {
        StartCoroutine(discardCards(playerCards));

        yield return new WaitForSeconds(0.3f);

        StartCoroutine(discardCards(dealerCards));

        yield return new WaitForSeconds(time);
        for (int i = 0; i < dealerCards.Count; i++)
        {
            // Destory the game object from the scene
            Destroy(dealerCards[i]);
        }
        for (int i = 0; i < playerCards.Count; i++)
        {
            // Destory the game object from the scene  
            Destroy(playerCards[i]);
        }
        playerCards = new List<GameObject>();
        dealerCards = new List<GameObject>();
 


    }

    public void playerLost(string reason) {
        bettingManager.playerLost();
        Debug.Log("player Lost: " + reason);
    }

    public void playerWon(string reason)
    {
        bettingManager.playerWon();
        Debug.Log("player Won: " + reason);

    }

    public void disablePlayButtons() {
        standButton.interactable = false;
        hitButton.interactable = false;
    }

    public void enablePlayButtons() {
        standButton.interactable = true;
        hitButton.interactable = true;
    }

    public void disableStartButton() {
        startButton.interactable = false;
    }

    public void enableStartButton() {
        startButton.interactable = true;
    }

    IEnumerator discardCards(List<GameObject> handCardCount) {
        for (int i = 0; i < handCardCount.Count; i++)
        {
            DiscardedCards.Add(handCardCount[i].GetComponent<CardView>().getCard());
            handCardCount[i].transform.DOMove(discardPoint.position, 0.2f);
            handCardCount[i].transform.DOLocalRotateQuaternion(discardPoint.rotation, 0.2f);
            yield return new WaitForSeconds(0.1f);

        }
    }

    public void Shuffle<T>(List<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}
