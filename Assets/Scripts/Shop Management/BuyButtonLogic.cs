using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BuyButtonLogic : MonoBehaviour
{
    public bool isBought { get; private set; }
    MoneyManager moneyManager;
    private ShopItemView shopItem;
    Button thisButton;
    ShopManagementUI shopUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moneyManager = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<MoneyManager>();
        shopUI = GameObject.FindGameObjectWithTag("ShopManagers").GetComponentInChildren<ShopManagementUI>();
        isBought = false;
        thisButton = gameObject.GetComponentInChildren<Button>();
        thisButton.onClick.AddListener(BuyItem);
    }

    public void setUp(ShopItemView shopItem)
    {
        this.shopItem = shopItem;
    }

    public void BuyItem()
    {
        if (moneyManager.getPlayerMoney() >= shopItem.itemCost)
        {
            shopItem.purchase();
            moneyManager.decPlayerMoney(shopItem.itemCost);
            changeBoughtDisplay();
            shopUI.updateTexts();
        }
    }

    public void changeBoughtDisplay() {
        thisButton.interactable = false;

        //Grey out the icon once bought
        Image itemIcon = gameObject.transform.parent.GetComponentInChildren<Image>();
        itemIcon.color = new Color(0.122f, 0.122f, 0.122f);
    }
}
