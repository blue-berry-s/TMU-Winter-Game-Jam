using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;


public class DeckDisplayUI : MonoBehaviour
{

    private BlackJackManager blackJackManager;

    [SerializeField] private Transform cardContainer;

    [SerializeField] GameObject cardPrefab;

    [SerializeField] Sprite backOfCardSprite;

    private List<Card> currentDeck;

    public float maxRotation = 10f; // degrees


    private void Start()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        blackJackManager = GameObject.FindGameObjectWithTag("BlackJackManagers").GetComponentInChildren<BlackJackManager>();
        currentDeck = blackJackManager.getDiscardDeck();
        displayDeck();
    }

    private void OnDisable()
    {
        
    }

    public void closeViewDeck() {
        FindFirstObjectByType<SoundManager>().playRandomizePitchSound("CardsShuffle", 0.75f, 0.75f);
        foreach (Transform child in cardContainer)
        {
            Destroy(child.gameObject);
        }
        gameObject.SetActive(false);
    }

    private void displayDeck() {

        spawnCards(currentDeck, cardContainer);


    }

    private void spawnCards(List<Card> allDeck, Transform position) {
        foreach (Card c in allDeck)
        {
            float randomZ = Random.Range(-maxRotation, maxRotation);
            transform.rotation = Quaternion.Euler(0f, 0f, randomZ);
            GameObject newCard = Instantiate(cardPrefab, position);
            newCard.GetComponent<RectTransform>().rotation = Quaternion.Euler(0,0, randomZ);
            newCard.GetComponent<Image>().sprite = c.Sprite;
        }
    }

    private void destroyCards(Transform parent)
    {
        int count = parent.childCount;
        for (int i = 0; i < count; i++) {
            Destroy(parent.GetChild(0).gameObject);
        }
    }

    }
