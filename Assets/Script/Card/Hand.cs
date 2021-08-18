using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Hand : MonoBehaviour
{
    [SerializeField] List<Card> hand;

    RectTransform rectTransform;
    [SerializeField] RectTransform grabbingCard;

    private void Awake()
    {
        hand = new List<Card>();
        rectTransform = GetComponent<RectTransform>();

    }

    public void AddCard(Card card)
    {
        if(hand.Count >= 10)
        {
            return;
        }
        int cardNum = rectTransform.childCount;
        card.GetComponent<Cardpop>().index = cardNum;
        card.GetComponent<Cardpop>().hand = rectTransform;
        card.GetComponent<Cardpop>().grabbingCard = grabbingCard;
        hand.Add(card);
        
        card.GetComponent<RectTransform>().SetParent(this.GetComponent<RectTransform>());
        card.gameObject.SetActive(true);
    }

    public void RemoveCardAt(int index)
    {
        hand.RemoveAt(index);
    }
}
