using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopItemView : MonoBehaviour
{
    public ShopItem shopItem;
    [SerializeField] Button button;
    [SerializeField] TMP_Text CostText;
    [SerializeField] TMP_Text NameText;
    [SerializeField] Image Icon;
    [SerializeField] Image coinImage;
    [SerializeField] GameObject coinCostContainer;
    MoneyManager moneyManager;
    HealthManager healthManager;
    ShopManagementUI shopUI;
    InventoryManager inventory;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<InventoryManager>();
        moneyManager = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<MoneyManager>();
        healthManager = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<HealthManager>();
        GameObject test = GameObject.FindGameObjectWithTag("ShopManagers");
            if (test != null) {
            shopUI = test.GetComponentInChildren<ShopManagementUI>();
        }
        
    }

    public void setUpBuy(ShopItem shopItem)
    {
        this.shopItem = shopItem;
        shopItem.Setup();
        if (shopItem.usesHealth)
        {
            CostText.text = shopItem.getPrice().ToString();
        }
        else {
            CostText.text = "$" + shopItem.getPrice().ToString();
        }
        NameText.text = shopItem.getName();
        Icon.sprite = shopItem.getSprite();
        setUpBuyButton();
        


    }

    public void setUpUse(ShopItem shopItem)
    {
        this.shopItem = shopItem;
        NameText.text = shopItem.getName();
        Icon.sprite = shopItem.getSprite();
        setUpUseButton();
    }


    public void setUpBuyButton()
    {
        button.GetComponentInChildren<TMP_Text>().text = "Buy";
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(BuyItem);
    }

    public void setUpUseButton()
    {
        button.GetComponentInChildren<TMP_Text>().text = "Use";
        CostText.gameObject.SetActive(false);
        NameText.gameObject.SetActive(false);
        coinImage.gameObject.SetActive(false);
        coinCostContainer.SetActive(false);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(UseItem);
    }





    public void BuyItem()
    {

        if (moneyManager.getPlayerMoney() >= shopItem.getPrice())
        {
            if (shopItem.forInventory && !inventory.inventoryFull())
            {
                inventory.addToInventory(shopItem);
            }
            else if (!shopItem.forInventory)
            {
                shopItem.Apply();

            }
            FindFirstObjectByType<SoundManager>().playRandomizePitchSound("ShopPurchase");
            moneyManager.decPlayerMoney(shopItem.getPrice());
            changeBoughtDisplay();
            shopUI.updateTexts();
        }
        else if (shopItem.usesHealth)
        {

            if (healthManager.getPlayerHealth() > shopItem.getPrice())
            {

                shopItem.Apply();
                changeBoughtDisplay();
                shopUI.updateTexts();

            }
            else {
                FindFirstObjectByType<SoundManager>().playUINotAllowed();
            }
        }
        else {
            FindFirstObjectByType<SoundManager>().playUINotAllowed();
        }

        shopUI.updateTexts();
    }

    public void UseItem() {
        GameObject test = GameObject.FindGameObjectWithTag("BlackJackManagers");
        if (test != null)
        {
            if (shopItem.Apply())
            {
                InventoryDisplay inventoryUI = GameObject.FindGameObjectWithTag("Inventory").GetComponentInChildren<InventoryDisplay>();
                inventory.deleteFromInventory(shopItem);
            }
            else
            {
                FindFirstObjectByType<SoundManager>().playUINotAllowed();
                return;
            }
        }
        else {
            FindFirstObjectByType<SoundManager>().playUINotAllowed();
        }
    }

    public void changeBoughtDisplay()
    {
        button.interactable = false;
        Icon.color = new Color32(122, 122, 122, 255);
        
    }
}
