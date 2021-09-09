using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    [Header("Limit Num of Card")]
    [SerializeField] int maxHandCount;
    [SerializeField] int initialDrawCards;

    [Header("Card")]
    public List<Card> grave;
    public List<Card> deck;
    public List<Card> hand;

    [Header("UI")]
    [SerializeField] Text graveCount;
    [SerializeField] Text deckCount;

    [Header("Transform")]
    public RectTransform grabbingCard;

    [SerializeField] private RectTransform graveTransform; // use inspector
    [SerializeField] private RectTransform deckTransform;
    [SerializeField] private RectTransform handTransform;

    public RectTransform GraveTransform { get => graveTransform; private set => graveTransform = value; }
    public RectTransform DeckTransform  { get => deckTransform;  private set => deckTransform  = value; }
    public RectTransform HandTransform  { get => handTransform;  private set => handTransform  = value; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        grave = new List<Card>();
        deck  = new List<Card>();
        hand  = new List<Card>();
    }

    private void Start()
    {
        GenerateDeck();
        for(int i = 0; i < initialDrawCards; i++)
        {
            DrawCard();
        }

        updateGraveUI();
    }

    // ----------HAND------------

    public void AddCardInHand(Card card)
    {
        if(hand.Count >= maxHandCount)
        {
            Debug.Log("You can't draw more than 10 cards!");
            return;
        }
        int cardNum = handTransform.childCount;
        card.GetComponent<Cardpop>().index = cardNum;
        card.GetComponent<Cardpop>().hand = handTransform;
        card.GetComponent<Cardpop>().grabbingCard = grabbingCard;
        hand.Add(card);
        
        card.GetComponent<RectTransform>().SetParent(handTransform);
        card.gameObject.SetActive(true);
    }

    /// <summary>
    /// 특정 위치에 있는 카드를 제거하는 기능
    /// </summary>
    /// <param name="index"></param>
    public void RemoveCardInHandAt(int index)
    {
        hand.RemoveAt(index);
    }

    // ---------GRAVE-----------

    public void AddCardInGrave(Card card)
    {
        grave.Add(card);
        grave.Sort(); // used IComparable in Card class
        card.GetComponent<RectTransform>().SetParent(graveTransform);

        updateGraveUI();
    }

    public void RemoveCardInGraveAt(int index)
    {
        grave.RemoveAt(index);
        updateGraveUI();
    }

    private void updateGraveUI()
    {
        graveCount.text = grave.Count.ToString();
    }

    // ----------DECK-----------

    public void AddCardInDeck(Card card)
    {
        deck.Add(card);
        deck.Sort(); // used IComparable in Card class
        card.GetComponent<RectTransform>().SetParent(deckTransform);

        updateDeckUI();
    }

    public Card DrawCard()
    {
        Card cardToDraw;
        if(deck.Count > 0)
        {
            int index = Random.Range(0, deck.Count);
            cardToDraw = deck[index];
            RemoveCardInDeckAt(index);
            AddCardInHand(cardToDraw);
        }
        else if(deck.Count <= 0 && grave.Count <= 0)
        {
            return null;
        }
        else // change grave to deck
        {
            List<Card> tempDeck = deck;
            deck = grave;
            grave = tempDeck;
            cardToDraw = DrawCard();
        }

        return cardToDraw;
    }

    public void DrawCardDebugVer()
    {
        DrawCard();
    }

    public void RemoveCardInDeckAt(int index)
    {
        deck.RemoveAt(index);
        updateDeckUI();
    }

    private void updateDeckUI()
    {
        deckCount.text = deck.Count.ToString();
    }

    public void GenerateDeck() // use for deck building
    {
        // int[] cardIDList = {1, 1, 1, 1, 1, 1}; // this will be changed to deck cards.
        int[] cardIDList = {0, 0, 0, 1, 1, 1, 2, 3, 4}; // this will be changed to deck cards.
        
        for(int i = 0; i < cardIDList.Length; i++)
        {
            Card newCard = CardDataLoader.instance.GetCard(cardIDList[i]);
            AddCardInDeck(newCard);
        }
    }
}