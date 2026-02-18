using UnityEngine;
[CreateAssetMenu(menuName = "Shop/Effects/Increase Body Part Value")]
public class IncBodyValueEffect : ShopItem
{
    public BodyPart bodyPart;
    private int amount;
    BodyPartManager bodyPartManager;

    //Probably need to switch the logic so that it caluclates the amount to increase (deprecating in amount)

    public override void Setup()
    {
        BodyPartManager bodyPartManager = getManager().GetComponentInChildren<BodyPartManager>();
        bodyPart = bodyPartManager.getRandomPart();
        amount = Mathf.Max(1, Mathf.RoundToInt(bodyPart.HealthCost * 0.25f));
    }
    public override void Apply()
    {
        bodyPartManager.increaseBodyPartValue(bodyPart, amount);
    }

    public override int getPrice()
    {
        return Mathf.Min(50, Mathf.RoundToInt(300/(bodyPart.HealthCost+10)));
    }

    public override string getName()
    {
        return "Increase " + bodyPart.Name + " value";
    }
}
