using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    [SerializeField] public Deck deck;
    [SerializeField] public Hand hand;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}