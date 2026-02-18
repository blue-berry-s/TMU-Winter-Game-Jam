using UnityEngine;
using System.Collections.Generic;


public class ShopManager : MonoBehaviour
{
    private ShopManagementUI shopUI;

    public List<ShopItem> potentialItems;
    public int maxNumberItems;
    private MoneyManager moneyManager;
    public int initialRerollCost = 5;
    private int reRollCost;
    private int rerollAmount = 0;
    


    public List<ShopItem> randomShop = new();


    private void Start()
    {
        moneyManager = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<MoneyManager>();
        shopUI = GameObject.FindGameObjectWithTag("ShopManagers").GetComponentInChildren<ShopManagementUI>();
 
        reRollCost = initialRerollCost;
        randomizeShop();
        shopUI.updateRerollButton(reRollCost);
        shopUI.updateTexts();

    }

    public void randomizeShop()
    {
        randomShop = new();
        // makes sure player can get a heal when starting out
        if (moneyManager.getPlayerMoney() < 50)
        {
            ShopItem heal = Instantiate(potentialItems[3]);
            heal.Setup();
            randomShop.Add(heal);
        }

        int count = randomShop.Count;
        while (count < maxNumberItems)
        {
            int index = Random.Range(0, potentialItems.Count);
            ShopItem item = Instantiate(potentialItems[index]);
            item.Setup();
            randomShop.Add(item);
            count = randomShop.Count;
        }
        shopUI.displayShop(randomShop);
    }

    public void purchaseRandomize() {
        if (moneyManager.getPlayerMoney() >= reRollCost) {
            moneyManager.decPlayerMoney(reRollCost);
            rerollAmount++;
            reRollCost = Mathf.RoundToInt(0.5f*rerollAmount + initialRerollCost);
            randomizeShop();
            shopUI.updateTexts();
            shopUI.updateRerollButton(reRollCost);

        }
        

    }








}
