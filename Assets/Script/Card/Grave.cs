using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class Grave : MonoBehaviour
{
    // ref
    private new RectTransform transform;

    // ui
    [SerializeField] private Text graveCount;

    // data
    [SerializeField] List<Card> grave;

    private void Awake()
    {
        transform = GetComponent<RectTransform>();
        
        grave = new List<Card>();
    }

    public void AddCard(Card card)
    {
        grave.Add(card);
        grave.Sort(); // used IComparable in Card class
        card.GetComponent<RectTransform>().SetParent(transform);

        updateDeckUI();
    }

    public void RemoveCardAt(int index)
    {
        grave.RemoveAt(index);
        updateDeckUI();
    }

    private void updateDeckUI()
    {
        graveCount.text = grave.Count.ToString();
    }
}
