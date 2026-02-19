using UnityEngine;

public class BodyPart
{
    private readonly BodyPartData bodyData;

    public BodyPart(BodyPartData bodyData)
    {
        this.bodyData = bodyData;
        Name = bodyData.Name;
        MoneyValue = bodyData.MoneyValue;
        HealthCost = bodyData.HealthCost;
        Amount = bodyData.Amount;
        sprite = bodyData.sprite;

    }
    public string Name { get;  }
    public int MoneyValue { get; set; }
    public int HealthCost { get; set; }

    public int Amount { get; set; }

    public Sprite sprite { get; set; }
}
