using UnityEngine;
using System.Collections.Generic;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] GameObject itemPrefab;
    InventoryManager inventoryManager;

    List<ShopItem> items;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<InventoryManager>();
        items = inventoryManager.getInventory();
        updateInventory();
    }

    public void addInventory(ShopItem item) {
        items.Add(item);
        updateInventory();
    }

    public void updateInventory() {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        foreach (ShopItem i in items) {
            ShopItemView view = Instantiate(itemPrefab, gameObject.transform).GetComponent<ShopItemView>();
            view.setUpUse(i);
        }
    }

    public void removeItem(ShopItem item) {
        items.Remove(item);
        updateInventory();
    }
}
