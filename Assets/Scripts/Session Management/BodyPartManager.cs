using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BodyPartManager : MonoBehaviour
{

    private List<BodyPartData> allBodyPartData = new();
    private List<BodyPart> playerBodyInventory = new();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BodyPartData[] temp = Resources.LoadAll<BodyPartData>("BodyParts");
        for (int i = 0; i < temp.Length; i++)
        {
            allBodyPartData.Add(temp[i]);
        }
        loadInBody();
    }

    // Update is called once per frame
    private void loadInBody() {
        foreach (BodyPartData c in allBodyPartData) {
            BodyPart newPart = new BodyPart(c);
            playerBodyInventory.Add(newPart);
        }
        
    }

    public BodyPart getRandomPart() {
        return playerBodyInventory[Random.Range(0, playerBodyInventory.Count)];
    }

    public List<BodyPart> getAllPlayerParts() {
        return playerBodyInventory;
    }

    public int getBodyPartAmount(BodyPartView part) {
        foreach (BodyPart b in playerBodyInventory) {
            if (b.Name == part.getName()) {
                return b.Amount;
            }
        }
        return -1;
    }

    public bool playerHasBody(BodyPartView part) {
        foreach (BodyPart b in playerBodyInventory)
        {
            if (b.Name == part.getName())
            {
                return true;
            }
        }
        return false;

    }

    public void addBodyPart(BodyPart part) {
        playerBodyInventory.Add(part);
    }

    public void increaseBodyPartValue(BodyPartData part, int amount) {
        foreach (BodyPart p in playerBodyInventory) {
            if (p.Name == part.Name) {
                p.MoneyValue += amount;
            }
        }
    }



    

}
