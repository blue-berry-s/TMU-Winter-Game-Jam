using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class CardManager : MonoBehaviour
{

    private List<CardData> clubsDatas = new();
    private List<CardData> heartsDatas = new();
    private List<CardData> diamondsDatas = new();
    private List<CardData> spadesDatas = new();

    private List<Card> playerClubs = new();
    private List<Card> playerHearts = new();
    private List<Card> playerDiamonds = new();
    private List<Card> playerSpades = new();

    private List<Card> allPlayerCards = new();


    private void Start()
    {

        CardData[] temp = Resources.LoadAll<CardData>("Clubs");
        for (int i = 0; i < temp.Length; i++) {
            clubsDatas.Add(temp[i]);
        }
        clubsDatas = clubsDatas.OrderBy(c => c.SecondValue).ToList();
        CardData[] temp2 = Resources.LoadAll<CardData>("Hearts");
        for (int i = 0; i < temp2.Length; i++)
        {
            heartsDatas.Add(temp2[i]);
        }
        heartsDatas = heartsDatas.OrderBy(c => c.SecondValue).ToList();
        CardData[] temp3 = Resources.LoadAll<CardData>("Diamonds");
        for (int i = 0; i < temp3.Length; i++)
        {
            diamondsDatas.Add(temp3[i]);
        }
        diamondsDatas = diamondsDatas.OrderBy(c => c.SecondValue).ToList();
        CardData[] temp4 = Resources.LoadAll<CardData>("Spades");
        for (int i = 0; i < temp4.Length; i++)
        {
            spadesDatas.Add(temp4[i]);
        }
        spadesDatas = spadesDatas.OrderBy(c => c.SecondValue).ToList();
    }

    public void setUpDeck() {
        for (int i = 0; i < clubsDatas.Count; i++)
        {
            CardData data = clubsDatas[i];
            Card card = new(data);
            playerClubs.Add(card);
            allPlayerCards.Add(card);
        }

        for (int i = 0; i < heartsDatas.Count; i++)
        {
            CardData data = heartsDatas[i];
            Card card = new(data);
            playerHearts.Add(card);
            allPlayerCards.Add(card);
        }

        for (int i = 0; i < diamondsDatas.Count; i++)
        {
            CardData data = diamondsDatas[i];
            Card card = new(data);
            playerDiamonds.Add(card);
            allPlayerCards.Add(card);
        }

        for (int i = 0; i < spadesDatas.Count; i++)
        {
            CardData data = spadesDatas[i];
            Card card = new(data);
            playerSpades.Add(card);
            allPlayerCards.Add(card);
        }
    }


    public bool deckEmpty() {
        return (allPlayerCards.Count < 3);
    }

    public List<Card> getPlayerCards() {
        return allPlayerCards;
    }

    public List<Card> getPlayerHearts(){
        return this.playerHearts;
    }

    public List<Card> getPlayerDiamonds()
    {
        return this.playerDiamonds;
    }

    public List<Card> getPlayerSpades()
    {
        return this.playerSpades;
    }

    public List<Card> getPlayerClubs()
    {
        return this.playerClubs;
    }

}
