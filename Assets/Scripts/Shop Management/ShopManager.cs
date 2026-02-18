using UnityEngine;
using System.Collections.Generic;


public class ShopManager : MonoBehaviour
{
    private ShopManagementUI shopUI;

    public List<ShopItem> potentialItems;
    public int maxNumberItems;



    private void Start()
    {
        
        shopUI = GameObject.FindGameObjectWithTag("ShopManagers").GetComponentInChildren<ShopManagementUI>();
        shopUI.updateTexts();
        shopUI.displayShop(potentialItems);
    }

    public void switchToGame() {
        SceneController.Instance
            .newTransition()
            .load(SceneDatabse.Slots.SessionContent, SceneDatabse.Scenes.BlackJack, setActive: true)
            .withOverlay()
            .Perform();
    }

}
