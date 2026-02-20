using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Effects/Heal Player")]
public class HealEffect : ShopItem
{
    private int amount;
    private HealthManager healthManager;
    private int minPrice;
    private int priceCheck;

    public override void Setup()
    {
        healthManager = getManager().GetComponentInChildren<HealthManager>();
        MoneyManager moneyManager = getManager().GetComponentInChildren<MoneyManager>();
        float percentage = Random.Range(0.1f, 0.5f);
        priceCheck = Mathf.RoundToInt(percentage* moneyManager.getPlayerMoney());
        amount = Mathf.RoundToInt(percentage*healthManager.getMaxHealth());

    }
    public override bool Apply()
    {
        healthManager.incPlayerHealth(amount);
        return true;
    }

    public override int getPrice()
    {
        return Mathf.Max(10, priceCheck);
    }

    public override string getName()
    {
        return "Heal " + amount.ToString();
    }
}
