using UnityEngine;
[CreateAssetMenu(menuName = "Shop/Effects/Discard Effect")]
public class discardCardEffect : ShopItem
{
    private BlackJackManager blackJackManager;
    public override void Setup()
    {
        
    }
    public override void Apply()
    {
        blackJackManager = GameObject.FindGameObjectWithTag("BlackJackManagers").GetComponentInChildren<BlackJackManager>();
        blackJackManager.discardPlayerCard();
    }
}
