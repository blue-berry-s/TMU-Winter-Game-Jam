using UnityEngine;
[CreateAssetMenu(menuName = "Shop/Effects/Insure Bet Effect")]
public class insureBetEffect : ShopItem
{
    BlackJackManager blackJackManager;
    public override void Setup()
    {
        
    }
    public override void Apply()
    {
        blackJackManager = GameObject.FindGameObjectWithTag("BlackJackManagers").GetComponentInChildren<BlackJackManager>();
        blackJackManager.discardPlayerCard();
    }
}
