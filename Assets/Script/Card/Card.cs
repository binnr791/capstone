using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.UI;

public class Card : MonoBehaviour, IComparable<Card>
{
    [Header("UI")]
    private Text cardNameText;
    private Text cardDescriptionText;
    private Text costText;

    [Header("Data")]
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _cost;

    public CardEffect.effectFunc effect; // 카드 효과

    public int id { get => _id; private set => _id = value;} // this is called when read value(get) or assign value(set)
    public new string name { get => _name; private set
    {
        _name = value;
        cardNameText.text = _name.ToString(); }} // automatically update text when field is changed
    public string description { get => _description; private set
    {
        _description = value;
        cardDescriptionText.text = _description.ToString(); }}
    public int cost { get => _cost; private set
    {
        _cost = value;
        costText.text = _cost.ToString(); }}

    private void Awake()
    {
        RectTransform transform = GetComponent<RectTransform>();
        cardNameText = transform.GetChild(0).GetComponent<Text>();
        cardDescriptionText = transform.GetChild(1).GetComponent<Text>();
        costText = transform.GetChild(2).GetComponent<Text>();
    }

    public void Init(int id, string name, string description, int cost) // constructor
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.cost = cost;
    }

    public void Init(cardData data) // constructor overload
    {
        this.id = data.id;
        this.name = data.name;
        this.description = data.description;
        this.cost = data.cost;
    }

    public int CompareTo(Card other) // used in sorting
    {
        if (this.cost.CompareTo(other.cost) != 0)
        {
            return this.cost.CompareTo(other.cost);
        }
        else if (this.id.CompareTo(other.id) != 0)
        {
            return this.id.CompareTo(other.id);
        }
        else
        {
            return 0;
        }
    }
}
