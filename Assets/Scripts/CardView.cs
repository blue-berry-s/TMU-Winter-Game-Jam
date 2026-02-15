using UnityEngine;
using UnityEngine.EventSystems;


public class CardView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SpriteRenderer cardImage;
    [SerializeField] private Sprite BackSprite;

    private bool flipped = false;
    private Card card;

    public void Setup(Card card) {
        this.card = card;
        cardImage.sprite = card.Sprite;
        Suit = card.Suit;
    }

    public void Setup(Card card, bool flipped) {
        this.flipped = flipped;
        if (flipped)
        {
            this.card = card;
            cardImage.sprite = BackSprite;
        }
        else {
            Setup(card);
        }
    }

    public void OnPointerClick(PointerEventData ped)
    {
        Debug.Log("Clicked!");
    }

    public void flip() {
        if (flipped)
        {
            cardImage.sprite = card.Sprite;
        }
        else {
            cardImage.sprite = BackSprite;
        }
        flipped = !(flipped);
    }

    public int getValue() {
        return card.Value;
    }

    public int getSecondValue() {
        return card.SecondValue;
    }

    public bool getFlipped() {
        return flipped;
    }

    public Card getCard() {
        return this.card;
    }

    public string Suit { get; private set; }
}
