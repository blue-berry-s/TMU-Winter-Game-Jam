using UnityEngine;
[CreateAssetMenu(menuName = "Shop/Effects/Gain a Body Part")]
public class gainPartEffect : ShopItem
{
    public BodyPart bodyPart;
    BodyPartManager bodyPartManager;

    public override void Setup()
    {
        bodyPartManager = getManager().GetComponentInChildren<BodyPartManager>();
        bodyPart = bodyPartManager.getRandomPart();

    }

    //Probably need to switch the logic so that it caluclates the amount to increase (deprecating in amount)
    public override bool Apply()
    {
        FindFirstObjectByType<SoundManager>().playBuyOrgan();
        bodyPartManager.addBodyPart(bodyPart);
        return true;
    }

    public override int getPrice()
    {
        return Mathf.RoundToInt(bodyPart.MoneyValue  + (bodyPart.MoneyValue * 0.25f));
    }

    public override string getName()
    {
        return "Buy " + bodyPart.Name;
    }

    public override Sprite getSprite()
    {
        return bodyPart.sprite;
    }
}
