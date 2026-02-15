using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Splines;
using DG.Tweening;
using System.Linq;


public class DeckBuilderManager : MonoBehaviour
{

    [SerializeField] private int maxPerSuit = 20;

    private List<CardData> clubsDatas = new();
    private List<CardData> heartsDatas = new();
    private List<CardData> diamondsDatas = new();
    private List<CardData> spadesDatas = new();

    [SerializeField] Transform spawnPoint;
    [SerializeField] private CardView cardView;

    private List<Card> playerClubs = new();
    private List<Card> playerHearts = new();
    private List<Card> playerDiamonds = new();
    private List<Card> playerSpades = new();

    private List<GameObject> clubsDisplay = new();
    private List<GameObject> heartsDisplay = new();
    private List<GameObject> diamondsDisplay = new();
    private List<GameObject> spadesDisplay = new();

    private List<Card> allPlayerCards = new();

    [SerializeField] private SplineContainer heartsSpline;
    [SerializeField] private SplineContainer clubsSpline;
    [SerializeField] private SplineContainer diamondsSpline;
    [SerializeField] private SplineContainer spadesSpline;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        
        CardData[] temp = Resources.LoadAll<CardData>("Clubs");
        for (int i = 0; i < temp.Length; i++) {
            clubsDatas.Add(temp[i]);
        }
        clubsDatas = clubsDatas.OrderBy(c => c.SecondValue).ToList();
        CardData[] temp2 = Resources.LoadAll<CardData>("Hearts");
        for (int i = 0; i < temp2.Length; i++)
        {
            heartsDatas.Add(temp2[i]);
        }
        heartsDatas = heartsDatas.OrderBy(c => c.SecondValue).ToList();
        CardData[] temp3 = Resources.LoadAll<CardData>("Diamonds");
        for (int i = 0; i < temp3.Length; i++)
        {
            diamondsDatas.Add(temp3[i]);
        }
        diamondsDatas = diamondsDatas.OrderBy(c => c.SecondValue).ToList();
        CardData[] temp4 = Resources.LoadAll<CardData>("Spades");
        for (int i = 0; i < temp4.Length; i++)
        {
            spadesDatas.Add(temp4[i]);
        }
        spadesDatas = spadesDatas.OrderBy(c => c.SecondValue).ToList();
    }

    public void setUp() {
        Start();
    }

    public void setUpDeck() {
        for (int i = 0; i < clubsDatas.Count; i++)
        {
            CardData data = clubsDatas[i];
            Card card = new(data);
            playerClubs.Add(card);
            allPlayerCards.Add(card);
        }

        for (int i = 0; i < heartsDatas.Count; i++)
        {
            CardData data = heartsDatas[i];
            Card card = new(data);
            playerHearts.Add(card);
            allPlayerCards.Add(card);
        }

        for (int i = 0; i < diamondsDatas.Count; i++)
        {
            CardData data = diamondsDatas[i];
            Card card = new(data);
            playerDiamonds.Add(card);
            allPlayerCards.Add(card);
        }

        for (int i = 0; i < spadesDatas.Count; i++)
        {
            CardData data = spadesDatas[i];
            Card card = new(data);
            playerSpades.Add(card);
            allPlayerCards.Add(card);
        }
    }

    public void showFullDeck() {
        setUpDeck();
        StartCoroutine(displayCards());
    }

    IEnumerator displayCards() {
        StartCoroutine(MoveCards(clubsDisplay, playerClubs, clubsSpline));  
        yield return new WaitForSeconds(0.15f);
        StartCoroutine(MoveCards(heartsDisplay, playerHearts, heartsSpline));
        yield return new WaitForSeconds(0.15f);
        StartCoroutine(MoveCards(spadesDisplay, playerSpades, spadesSpline));
        yield return new WaitForSeconds(0.15f);
        StartCoroutine(MoveCards(diamondsDisplay, playerDiamonds, diamondsSpline));
    }
    IEnumerator MoveCards(List<GameObject> display, List<Card> cards, SplineContainer splineContainer) {

        for (int i = 0; i < cards.Count; i++) {
            CardView view = Instantiate(cardView, spawnPoint.position, spawnPoint.rotation);
            view.Setup(cards[i]);
            display.Add(view.gameObject);

        }
        if (cards.Count == 0) yield return new WaitForSeconds(0.15f); ;
        float cardSpacing = 1f / maxPerSuit;
        float firstCardPosition = 0.5f - (cards.Count - 1) * cardSpacing / 2;
        Spline spline = splineContainer.Spline;
        for (int i = 0; i < cards.Count; i++)
        {
            float p = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, forward).normalized);
            display[i].transform.DOMove(splinePosition, 0.15f);
            display[i].transform.DOLocalRotateQuaternion(rotation, 0.15f);
            yield return new WaitForSeconds(0.12f);

        }

    }

    public void clearDeck() {
        for (int i = 0; i < clubsDisplay.Count; i++) {
            Destroy(clubsDisplay[i].gameObject);
        }
        for (int i = 0; i < heartsDisplay.Count; i++)
        {
            Destroy(heartsDisplay[i].gameObject);
        }
        for (int i = 0; i < diamondsDisplay.Count; i++)
        {
            Destroy(diamondsDisplay[i].gameObject);
        }
        for (int i = 0; i < spadesDisplay.Count; i++)
        {
            Destroy(spadesDisplay[i].gameObject);
        }

        clubsDisplay = new();
        heartsDisplay = new();
        diamondsDisplay= new();
        spadesDisplay= new();

        playerClubs = new List<Card>();
        playerHearts = new List<Card>();
        playerDiamonds = new List<Card>();
        playerSpades = new List<Card>();
    }

    public bool deckEmpty() {
        Debug.Log("DECKBUILDERMANAGER: allPlayerCards: "+ allPlayerCards.Count);
        return (allPlayerCards.Count < 3);
    }

    public List<Card> getPlayerCards() {
        return allPlayerCards;
    }

 


}
