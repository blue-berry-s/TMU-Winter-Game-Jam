using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Effects/Heal Player")]
public class HealEffect : ShopItem
{
    private int amount;
    private HealthManager healthManager;
    private MoneyManager moneyManager;
    private int priceCheck;

    public override void Setup()
    {
        healthManager = getManager().GetComponentInChildren<HealthManager>();
        moneyManager = getManager().GetComponentInChildren<MoneyManager>();
        setAmounts();




    }
    public override bool Apply()
    {
        healthManager.incPlayerHealth(amount);
        return true;
    }

    public override int getPrice()
    {
        if (moneyManager.getPlayerMoney() < 20)
        {
            return 5;
        }
        else
        {
            return Mathf.Max(5, priceCheck);
        }
        
    }

    public override string getName()
    {
        return "Heal " + amount.ToString();
    }

    private void setAmounts() {

        float percentage = Random.Range(0.1f, 0.75f);
        priceCheck = Mathf.RoundToInt(percentage * moneyManager.getPlayerMoney());


        float money = moneyManager.getPlayerMoney();
        int maxHealth = healthManager.getMaxHealth(); // should be 30

        // Prevent divide by zero
        if (money <= 0f)
        {
            amount = 0;
        }
        else
        {
            float priceRatio = (float)priceCheck / money;
            amount = Mathf.RoundToInt(priceRatio * maxHealth);
        }

        // Final safety clamp
        amount = Mathf.Clamp(amount, 0, maxHealth);
    }
}
