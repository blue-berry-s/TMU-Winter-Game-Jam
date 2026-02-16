using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class ShopManager : MonoBehaviour
{
    private CardDisplayManager cardDisplayShop;
    private ShopManagementUI shopUI;

    public List<ShopItem> itemsForSale;


    private void Start()
    {
        cardDisplayShop = GameObject.FindGameObjectWithTag("ShopManagers").GetComponentInChildren<CardDisplayManager>();
        shopUI = GameObject.FindGameObjectWithTag("ShopManagers").GetComponentInChildren<ShopManagementUI>();
        shopUI.updateTexts();
        shopUI.displayShop(itemsForSale);
    }

    public void switchToGame() {
        SceneController.Instance
            .newTransition()
            .load(SceneDatabse.Slots.SessionContent, SceneDatabse.Scenes.BlackJack, setActive: true)
            .withOverlay()
            .Perform();
    }

}
