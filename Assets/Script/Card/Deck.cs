using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class Deck : MonoBehaviour
{
    // ref
    [SerializeField] private Hand hand;

    private new RectTransform transform;

    // ui
    [SerializeField] private Text deckCount;

    // data
    [SerializeField] List<Card> deck;

    private int initialDrawCards = 4; // the number of cards when the game is started.

    private void Awake()
    {
        transform = GetComponent<RectTransform>();
        
        deck = new List<Card>();
    }

    private void Start()
    {
        GenerateDeck();

        for(int i = 0; i < initialDrawCards; i++)
        {
            DrawCard();
        }
    }

    public void AddCard(Card card)
    {
        deck.Add(card);
        deck.Sort(); // used IComparable in Card class
        card.GetComponent<RectTransform>().SetParent(transform);

        updateDeckUI();
    }

    public Card DrawCard()
    {
        int index = Random.Range(0, deck.Count);
        Card cardToDraw = deck[index];
        RemoveCardAt(index);
        hand.AddCard(cardToDraw);

        return cardToDraw;
    }

    public void DrawCardDebugVer()
    {
        DrawCard();
    }

    public void RemoveCardAt(int index)
    {
        deck.RemoveAt(index);
        updateDeckUI();
    }

    public void GenerateDeck() // use for deck building
    {
        int[] cardIDList = {0, 1, 2, 3, 4}; // this will be changed to deck cards.
        for(int i = 0; i < cardIDList.Length; i++)
        {
            Card newCard = CardDataLoader.instance.GetCard(cardIDList[i]);
            AddCard(newCard);
        }
    }

    private void updateDeckUI()
    {
        deckCount.text = deck.Count.ToString();
    }
}
