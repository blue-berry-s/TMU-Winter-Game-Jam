using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class ShopItem : ScriptableObject
{

    public string itemName;
    public Sprite icon;
    public int price;
    public bool forInventory;
    public bool isBought { get; private set; }
    public bool usesHealth = false;

  

    public abstract void Setup();
    public abstract bool Apply();

    public void addToInventory() {
        Debug.Log("Added to inventory");
    }

    public virtual string getName() {
        return itemName;
    }

    public virtual int getPrice() {
        return price;
    }

    public virtual Sprite getSprite() {
        return icon;
    }

    public GameObject getManager()
    {
        GameObject managers = GameObject.FindGameObjectWithTag("SessionManagers");
        return managers;
    }

    public void setHealthTrue() {
        usesHealth = true;
    }

    public bool getUsesHealth() {
        return usesHealth;
    }

}
