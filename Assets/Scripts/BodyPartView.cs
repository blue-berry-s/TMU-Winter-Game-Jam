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

    public int getAmount() {
        return bodyPart.Amount;
    }

    public void incAmount() {
        bodyPart.Amount++;
    }

    public void decAmount() {
        bodyPart.Amount--;
    }

    public BodyPart getBodyPart() {
        return bodyPart;
    }

    public void darkenSprite() {
        this.gameObject.GetComponentInChildren<Image>().color = new Color32(127, 127, 127, 255);
    }

    public void lightenSprite() {
        this.gameObject.GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);
    }

}
