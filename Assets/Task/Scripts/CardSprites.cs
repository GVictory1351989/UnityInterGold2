using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class CardSprites : ScriptableObject
{
    /// <summary>
    /// Club Card
    /// </summary>
    public List<Card> clubcard;
    /// <summary>
    /// Heart Card 
    /// </summary>
    public List<Card> heartcard;
    /// <summary>
    /// Diamond Card
    /// </summary>
    public List<Card> diamondcard;
    /// <summary>
    /// Spade Card
    /// </summary>
    public List<Card> spadecard;
    /// <summary>
    /// Joker Card
    /// </summary>
    public Card jokercard;
    /// <summary>
    /// spade Cards
    /// </summary>
    public List<JokerCard> spadejokers;
    /// <summary>
    /// club jokers
    /// </summary>
    public List<JokerCard> clubjokers;
    /// <summary>
    /// heart jokers
    /// </summary>
    public List<JokerCard> heartjokers;
    /// <summary>
    /// diamond jokers
    /// </summary>
    public List<JokerCard> diamondjokers;


    public List<CardCodeSprite> codesprites;
}
[Serializable]
public class CardCodeSprite
{
    public string code;
    public Sprite jokersprite;
    public Sprite purejokersprite;
}
[Serializable]
public class JokerCard : Card
{
    /// <summary>
    /// Joker Card string value 
    /// </summary>
    private const string jokercard = "JOKER";
    /// <summary>
    /// Card ID of a Screen
    /// </summary>
    public new string CardId
    {
        get
        {
            return cardrank.ToString() + cardsuit.ToString() + jokercard;
        }
    }
}


[Serializable]
public class Card
{
    public string CardId
    {
        get
        {
            return cardrank.ToString() + cardsuit.ToString();
        }
    }
    public CARDRANK cardrank;
    public SUIT cardsuit;
    public Sprite cardsprite;
}
