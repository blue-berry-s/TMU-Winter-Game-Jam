using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Effects/Heal Player")]
public class HealEffect : ShopItemEffect
{
    public int amount;

    public override void Apply(GameObject managerObjects)
    {
        
        HealthManager healthManager = managerObjects.GetComponentInChildren<HealthManager>();
        Debug.Log("Health Before: " + healthManager.getPlayerHealth());
        healthManager.incPlayerHealth(amount);
        Debug.Log("Health After: " + healthManager.getPlayerHealth());
    }
}
