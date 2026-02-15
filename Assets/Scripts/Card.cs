using UnityEngine;

public class Card
{
    private readonly CardData cardData;

    public Card(CardData cardData) {
        this.cardData = cardData;
        Value = cardData.Value;
        SecondValue = cardData.SecondValue;
        Suit = cardData.Suit;
    }

    //This allows you to customize your own cards (its values can now deviate from the original card data
    public Sprite Sprite { get => cardData.Sprite;  }
    public string Suit { get; set; }
    public int Value { get; set; }

    public int SecondValue { get; set; }



}
