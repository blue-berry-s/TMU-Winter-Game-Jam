using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


public class CardView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SpriteRenderer cardImage;
    [SerializeField] private Sprite BackSprite;

    private bool flipped = false;
    private Card card;

    private float tiltAmount = 15f;   
    private float tiltSpeed = 0.3f;
    private float randomOffset;

    private float flipDuration = 0.15f;
    private bool isFlipping = false;



    public void Setup(Card card) {
        this.card = card;
        cardImage.sprite = card.Sprite;
        Suit = card.Suit;
        randomOffset = Random.Range(0f, Mathf.PI * 2f);
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

        if (!isFlipping)
        {
            StartCoroutine(FlipRoutine());
        }
        
    }

    private IEnumerator FlipRoutine()
    {
        isFlipping = true;

        float elapsed = 0f;
        Vector3 originalScale = transform.localScale;

        // First half: shrink to 0
        while (elapsed < flipDuration / 2f)
        {
            elapsed += Time.deltaTime;
            float scaleX = Mathf.Lerp(1f, 0f, elapsed / (flipDuration / 2f));
            transform.localScale = new Vector3(scaleX, originalScale.y, originalScale.z);
            yield return null;
        }

        FindFirstObjectByType<SoundManager>().playRandomizePitchSound("CardFlip");
        // Swap sprite at midpoint
        if (flipped)
        {
            cardImage.sprite = card.Sprite;
        }
        else
        {
            cardImage.sprite = BackSprite;
        }
        flipped = !flipped;


        elapsed = 0f;

        // Second half: expand back out
        while (elapsed < flipDuration / 2f)
        {
            elapsed += Time.deltaTime;
            float scaleX = Mathf.Lerp(0f, 1f, elapsed / (flipDuration / 2f));
            transform.localScale = new Vector3(scaleX, originalScale.y, originalScale.z);
            yield return null;
        }

        transform.localScale = originalScale;
        isFlipping = false;
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

    private void Update()
    {
        float tiltX = Mathf.Sin(Time.time * tiltSpeed + randomOffset) * tiltAmount;
        float tiltY = Mathf.Cos(Time.time * tiltSpeed + randomOffset) * tiltAmount;

        transform.rotation = Quaternion.Euler(tiltX, tiltY, 0f);
    }
}
