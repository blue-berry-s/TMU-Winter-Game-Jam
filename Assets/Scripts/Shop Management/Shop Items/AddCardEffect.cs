using UnityEngine;
[CreateAssetMenu(menuName = "Shop/Effects/Add Card")]
public class AddCardEffect : ShopItemEffect
{
    public CardData cardToGive;
    public override void Apply(GameObject managerObjects)
    {
        CardManager playerCards = managerObjects.GetComponentInChildren<CardManager>();
        playerCards.addCard(cardToGive);
    }
}
