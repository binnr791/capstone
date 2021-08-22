using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct cardData
{
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int cost { get; set; }

    public cardData(int id, string name, string description, int cost) // constructor
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.cost = cost;
    }
}

[RequireComponent(typeof(CardEffect))]
public class CardDataLoader : MonoBehaviour
{
    public static CardDataLoader instance;

    [SerializeField] GameObject cardPrefab;

    public Dictionary<int, cardData> cardDataDict;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        cardDataDict = new Dictionary<int, cardData>();

        cardDataDict[0] = new cardData(0, "Blood Sucking", "대상 적에게 4 피해를 주고, 체력을 3 회복한다.", 3);
        cardDataDict[1] = new cardData(1, "Defense", "방어도를 6 얻는다.", 3);
        cardDataDict[2] = new cardData(2, "Single Target Attack", "대상 적에게 6 피해를 준다.", 3);
        cardDataDict[3] = new cardData(3, "Single Target Heal", "대상 아군의 체력을 4 회복한다.", 3);
        cardDataDict[4] = new cardData(4, "Attack All Enemy", "대상 아군의 체력을 4 회복한다.", 8);
        cardDataDict[5] = new cardData(5, "Dedicatation", "대상 아군의 스테미나를 2 회복한다.", 1);
        cardDataDict[6] = new cardData(6, "Dedicate and Heal", "대상 아군의 스테미나를 1 회복하고, 체력을 3 회복한다.", 3);
        cardDataDict[7] = new cardData(7, "Recylce Attack", "재활용, 대상 적에게 7 피해를 준다.", 4);
        cardDataDict[8] = new cardData(8, "Attack All Enemy", "대상 아군의 체력을 4 회복한다.", 4);

    }

    // public Card CreateCard()
    // {

    // }

    public Card GetCard(int id)
    {
        GameObject newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
        Card result = newCard.GetComponent<Card>();

        cardData newCardData = cardDataDict[id];
        result.Init(newCardData);
        result.effect = GetComponent<CardEffect>().GetEffect(newCardData.id);

        newCard.SetActive(false);
        return result;
    }
}
