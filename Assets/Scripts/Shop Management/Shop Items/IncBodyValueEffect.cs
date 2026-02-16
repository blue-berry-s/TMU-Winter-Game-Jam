using UnityEngine;
[CreateAssetMenu(menuName = "Shop/Effects/Increase Body Part Value")]
public class IncBodyValueEffect : ShopItemEffect
{
    public BodyPartData bodyPart;
    public int amount;

    //Probably need to switch the logic so that it caluclates the amount to increase (deprecating in amount)
    public override void Apply(GameObject managerObjects)
    {
        BodyPartManager bodyPartManager = managerObjects.GetComponentInChildren<BodyPartManager>();
        bodyPartManager.increaseBodyPartValue(bodyPart, amount);
    }
}
