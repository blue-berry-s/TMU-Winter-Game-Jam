using UnityEngine;

[CreateAssetMenu(menuName = "Card Data")]
public class CardData : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }

    [field: SerializeField] public int Value { get; private set; }

    [field: SerializeField] public string Suit { get; private set; }

    [field: SerializeField] public int SecondValue { get; private set; }
}
