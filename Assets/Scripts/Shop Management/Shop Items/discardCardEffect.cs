using UnityEngine;
[CreateAssetMenu(menuName = "Shop/Effects/Discard Effect")]
public class discardCardEffect : ShopItem
{
    private BlackJackManager blackJackManager;
    public override void Setup()
    {
        
    }
    public override bool Apply()
    {
        blackJackManager = GameObject.FindGameObjectWithTag("BlackJackManagers").GetComponentInChildren<BlackJackManager>();
        Debug.Log("A:" + blackJackManager.playerCardCount());
        if (blackJackManager.playerCardCount() > 0)
        {
            blackJackManager.discardPlayerCard();
            return true;
        }
        else {
            return false;
        }
       
    }
}
