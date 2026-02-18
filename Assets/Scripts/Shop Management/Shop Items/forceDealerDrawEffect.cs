using UnityEngine;
[CreateAssetMenu(menuName = "Shop/Effects/Force Dealer Draw")]
public class forceDealerDrawEffect : ShopItem
{
    private BlackJackManager blackJackManager;
    public override void Setup()
    {
        
    }
    public override bool Apply()
    {
        blackJackManager = GameObject.FindGameObjectWithTag("BlackJackManagers").GetComponentInChildren<BlackJackManager>();
        Debug.Log("B:" + blackJackManager.dealerCardCount());
        if (blackJackManager.dealerCardCount() > 0)
        {
            blackJackManager.DealerDrawCard(false);
            return true;
        }
        else {
            return false;
        }
    }
}
