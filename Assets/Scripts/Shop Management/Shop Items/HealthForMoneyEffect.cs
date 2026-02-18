using UnityEngine;
[CreateAssetMenu(menuName = "Shop/Effects/Health For Money Effect")]
public class HealthForMoneyEffect : ShopItem
{

    private int Healthamount;
    private int moneyAmount;
    

    private HealthManager healthManager;
    private MoneyManager moneyManager;
   


    public override void Setup()
    {
        healthManager = getManager().GetComponentInChildren<HealthManager>();
        moneyManager = getManager().GetComponentInChildren<MoneyManager>();
        Healthamount = getHealthAmount();
        moneyAmount = calculateMoneyValue();
        setHealthTrue();

    }
    public override bool Apply()
    {
        
        healthManager.decPlayerHealth(Healthamount);
        moneyManager.incPlayerMoney(moneyAmount);
        return true;

    }


    public override int getPrice()
    {
        return Healthamount;
    }

    public override string getName()
    {
        return "Sacrafice for $" + moneyAmount.ToString();
    }

    private int getHealthAmount() {
        if (moneyManager.getPlayerMoney() == 0)
        {
            return 5;
        }
        else { 
            return Random.Range(10, 25);
        }
    }

    private int calculateMoneyValue() {
        if (moneyManager.getPlayerMoney() == 0)
        {
            return 10;
        }
        else {
            return Mathf.RoundToInt((Healthamount * 0.5f) * (moneyManager.getPlayerMoney() * 0.2f));
        }
            
    }
}
