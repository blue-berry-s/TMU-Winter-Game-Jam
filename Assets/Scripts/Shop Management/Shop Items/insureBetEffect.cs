using UnityEngine;
[CreateAssetMenu(menuName = "Shop/Effects/Insure Bet Effect")]
public class insureBetEffect : ShopItem
{
    BettingManager bettingManager;
    public override void Setup()
    {
        
    }
    public override bool Apply()
    {
        FindFirstObjectByType<SoundManager>().playInsureBet();
        bettingManager = GameObject.FindGameObjectWithTag("BlackJackManagers").GetComponentInChildren<BettingManager>();
        bettingManager.insureBet();
        return true;
    }
}
