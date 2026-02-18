using UnityEngine;
[CreateAssetMenu(menuName = "Shop/Effects/Force Dealer Draw")]
public class forceDealerDrawEffect : ShopItem
{
    private BlackJackManager blackJackManager;
    public override void Setup()
    {
        
    }
    public override void Apply()
    {
        blackJackManager = GameObject.FindGameObjectWithTag("BlackJackManagers").GetComponentInChildren<BlackJackManager>();
        blackJackManager.DealerDrawCard(false);
    }
}
