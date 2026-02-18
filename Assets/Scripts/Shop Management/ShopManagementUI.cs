using UnityEngine;
using TMPro;
using System.Collections.Generic;


public class ShopManagementUI : MonoBehaviour
{
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text moneyText;

    private HealthManager healthManager;
    private MoneyManager moneyManager;

    public Transform shopGrid;

    [SerializeField] GameObject shopItemPrefab;

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
            ShopItemView view = Instantiate(shopItemPrefab, shopGrid).GetComponent<ShopItemView>();
            view.setUpBuy(i);
        }

    }
}
