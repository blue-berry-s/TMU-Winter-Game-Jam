using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemView : MonoBehaviour
{
    private ShopItem shopItem;
    private MoneyManager moneyManager;

    [SerializeField] private Image ShopItemImage;
    [SerializeField] private BuyButtonLogic BuyButton;
    [SerializeField] private TMP_Text ItemLabel;
    [SerializeField] private TMP_Text ItemCost;

    public int itemCost { get; private set; }

    private void Start()
    {
        
    }

    public void Setup(ShopItem shopItem)
    {
        this.shopItem = shopItem;
        ShopItemImage.sprite = shopItem.icon;

        itemCost = shopItem.price;
        ItemCost.text = shopItem.price.ToString();

        ItemLabel.text = shopItem.name;
        BuyButton.setUp(this);
    }

    public void purchase() {
        shopItem.Purchase();
    }

  


}
