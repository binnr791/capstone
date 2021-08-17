using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] List<Card> hand;

    private void Awake()
    {
        hand = new List<Card>();
    }

    public void AddCard(Card card)
    {
        if(hand.Count >= 10)
        {
            return;
        }
        hand.Add(card);
        card.GetComponent<RectTransform>().SetParent(this.GetComponent<RectTransform>());
        card.gameObject.SetActive(true);
    }

    public void RemoveCardAt(int index)
    {
        hand.RemoveAt(index);
    }
}
