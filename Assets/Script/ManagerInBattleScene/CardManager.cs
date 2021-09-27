using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
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
        card.GetComponent<Outline>().enabled = true;
        card.gameObject.SetActive(true);

        UpdateAvailableHand();
    }

    /// <summary>
    /// 특정 위치에 있는 카드를 제거하는 기능
    /// </summary>
    /// <param name="index"></param>
    public void RemoveCardInHandAt(int index)
    {
        hand.RemoveAt(index);
    }

    /// <summary>
    /// 카드 사용을 승인 및 카드 효과 처리가 완료됐을 때 처리
    /// </summary>
    /// <param name="usedCard"></param>
    public void HandleUsedCard(Card usedCard)
    {
        if(usedCard.HasCardProperty(CardProperty.Recycle))
        {
            AddCardInDeck(usedCard);
        }
        else
        {
            AddCardInGrave(usedCard);
        }
    }

    // ---------GRAVE-----------

    public void AddCardInGrave(Card card)
    {
        grave.Add(card);
        grave.Sort(); // used IComparable in Card class
        card.GetComponent<RectTransform>().SetParent(graveTransform);
        card.GetComponent<Outline>().enabled = false;

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
        card.GetComponent<Outline>().enabled = false;
        //card.gameObject.SetActive(true);

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
        // int[] cardIDList = {0, 0, 1, 1, 2, 7, 7, 7, 7, 7}; // recycle deck
        // int[] cardIDList = {4, 4, 4, 4, 8, 8, 8, 8}; // recycle deck
        // int[] cardIDList = {10, 10, 10, 12, 12, 12, 12, 0}; // status effect deck
        for(int i = 0; i < cardIDList.Length; i++)
        {
            Card newCard = CardDataLoader.instance.GetCard(cardIDList[i]);
            AddCardInDeck(newCard);
        }
    }

    // UI
    public List<GameObject> GetDeckCards()
    {
        List<GameObject> deck = new List<GameObject>();
        for(int i = 0; i < deckTransform.childCount; i++)
        {
            deck.Add(deckTransform.GetChild(i).gameObject);
        }
        return deck;
    }

    public List<GameObject> GetGraveCards()
    {
        List<GameObject> grave = new List<GameObject>();
        for(int i = 0; i < graveTransform.childCount; i++)
        {
            grave.Add(graveTransform.GetChild(i).gameObject);
        }
        return grave;
    }

    /// <summary>
    /// 현재 턴의 캐릭터의 스테미나에 따라서 패에서 사용 가능한 카드를 바꿈.
    /// </summary>
    public void UpdateAvailableHand()
    {
        if(hand.Count > 0) // 
        {
            Character curTurnChar = BattleManager.instance.GetCurrentTurnChar();
            if(curTurnChar != null)
            {
                if(curTurnChar.faction == Faction.Player)
                {
                    for(int i = 0; i < hand.Count; i++)
                    {
                        hand[i].GetComponent<Cardpop>().isEnoughCost =
                            BattleManager.instance.GetCurrentTurnChar().stat.stamina >= hand[i].GetComponent<Card>().cost;
                    }
                }
                else // diable hand when enemy turn
                {
                    for(int i = 0; i < hand.Count; i++)
                    {
                        hand[i].GetComponent<Cardpop>().isEnoughCost = false;
                    }
                }
            }
            else
            {
                return;
            }
        }
    }
}