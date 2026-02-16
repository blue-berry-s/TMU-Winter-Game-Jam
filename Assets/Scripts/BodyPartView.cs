using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BodyPartView : MonoBehaviour
{

    private BodyPart bodyPart;

    private Sprite sprite;


    private void Start()
    {
       
    }
    public void setUp(BodyPart bodyPart) {
        this.bodyPart = bodyPart;
        sprite = bodyPart.sprite;
        this.gameObject.GetComponentInChildren<Image>().sprite = sprite;
    }

    public int getMoney() {
        return bodyPart.MoneyValue;
    }

    public int getHealth()
    {
        return bodyPart.HealthCost;
    }

    public string getName() {
        return bodyPart.Name;
    }

}
