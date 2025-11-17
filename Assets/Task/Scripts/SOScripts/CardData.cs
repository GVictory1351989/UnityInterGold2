using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Card/Create New Card")]
public class CardData : ScriptableObject
{
    public SUIT suit;
    public CARDRANK rank;
    public Sprite cardSprite;
    /// <summary>
    /// Get the Card ID based on suit and rank.
    /// </summary>
    public string CardID => suit.ToString() + rank.ToString();
}

