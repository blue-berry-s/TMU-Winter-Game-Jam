using UnityEngine;
[CreateAssetMenu(menuName = "Body Part")]
public class BodyPartData : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int MoneyValue { get; private set; }
    [field: SerializeField] public int HealthCost { get; private set; }
    [field: SerializeField] public int Amount { get; private set; }

    [field: SerializeField] public Sprite sprite { get; private set; }
}
