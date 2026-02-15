using UnityEngine;

public class BodyPart
{
    private readonly BodyPartData bodyData;

    public BodyPart(BodyPartData bodyData)
    {
        this.bodyData = bodyData;
        this.Name = bodyData.Name;
        MoneyValue = bodyData.MoneyValue;
        HealthCost = bodyData.HealthCost;
    }
    public string Name { get;  }
    public int MoneyValue { get; set; }
    public int HealthCost { get; set; }
}
