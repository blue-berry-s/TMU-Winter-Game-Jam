using UnityEngine;
using System.Collections.Generic;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] GameObject itemPrefab;
    InventoryManager inventoryManager;

    List<ShopItem> items;
    int prevCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<InventoryManager>();
        items = inventoryManager.getInventory();
        prevCount = items.Count;
        updateInventory();
    }

    public void updateInventory() {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        items = inventoryManager.getInventory();
        foreach (ShopItem i in items) {
            ShopItemView view = Instantiate(itemPrefab, gameObject.transform).GetComponent<ShopItemView>();
            view.setUpUse(i);
        }
    }

    private void Update()
    {
        if (inventoryManager.getInventoryCount() != prevCount) {
            updateInventory();
            prevCount = inventoryManager.getInventoryCount();
        }
    }
}
