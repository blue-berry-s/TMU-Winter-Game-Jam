using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Shop Item")]
public class ShopItem:ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int price;

    public List<ShopItemEffect> effects;


    public void Purchase()
    {
        GameObject managers = GameObject.FindGameObjectWithTag("SessionManagers");
        foreach (var effect in effects)
        {
            effect.Apply(managers);
        }
    }

}
