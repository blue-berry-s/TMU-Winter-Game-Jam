using UnityEngine;
[CreateAssetMenu(menuName = "Shop/Effects/Delete Card")]
public class DeleteCardEffect : ShopItemEffect
{
    public CardData cardToDelete;

    public override void Apply(GameObject managerObjects)
    {
        CardManager playerCards = managerObjects.GetComponentInChildren<CardManager>();
        playerCards.deleteCard(cardToDelete);
    }
}
