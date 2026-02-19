using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class InventoryManager : MonoBehaviour
{
    List<ShopItem> inventory = new();
    [SerializeField] private int maxInventory = 4;
    public void addToInventory(ShopItem item) {
        inventory.Add(item);
    }

    public void deleteFromInventory(ShopItem item) {
        inventory.Remove(item);
    }

    public bool inventoryFull() {
        return inventory.Count == maxInventory;
    }

    public int getInventoryCount() {
        return inventory.Count();
    }

    public List<ShopItem> getInventory() {
        return inventory;
    }
}
