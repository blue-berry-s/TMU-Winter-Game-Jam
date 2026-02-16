using UnityEngine;
using TMPro;
using System.Collections.Generic;


public class ShopManagementUI : MonoBehaviour
{
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text moneyText;

    private HealthManager healthManager;
    private MoneyManager moneyManager;

    public GameObject itemsDisplay;
    public Transform shopGrid;

    private void Start()
    {
        healthManager = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<HealthManager>();
        moneyManager = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<MoneyManager>();
    }

    public void updateTexts() {
        healthText.text = healthManager.getPlayerHealth().ToString();
        moneyText.text = moneyManager.getPlayerMoney().ToString();

    }

    public void displayShop(List<ShopItem> itemsForSale) {
        foreach (ShopItem i in itemsForSale) {
            GameObject newItemDisplay = Instantiate(itemsDisplay, shopGrid);
            ShopItemView itemView = newItemDisplay.GetComponent<ShopItemView>();
            itemView.Setup(i);
            
        }

    }
}
