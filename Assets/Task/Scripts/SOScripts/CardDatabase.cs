using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDatabase", menuName = "Card/Card Database")]
public class CardDatabase : ScriptableObject
{
    public List<CardData> cards;
    /// <summary>
    /// Retrieve a sprite by card ID.
    /// </summary>
    public Sprite GetSpriteByCardID(string cardID)
    {
        StandardCard carda = new StandardCard(cardID);
        foreach (var card in cards)
        {
            if (card.suit == carda.suit && card.rank==carda.CardRank)
            {
                return card.cardSprite;
            }
        }
        return null;
    }
    public Sprite GetSpriteByStandardCard(StandardCard cardID)
    {
        foreach (var card in cards)
        {
            if (card.suit == cardID.suit && card.rank == cardID.CardRank)
            {
                return card.cardSprite;
            }
        }
        return null;
    }
    /// <summary>
    /// Retrieve a sprite by card name.
    /// </summary>
    public Sprite GetSpriteByName(string name)
    {
        foreach (var card in cards)
        {
            if (card.name == name)
            {
                return card.cardSprite;
            }
        }
        return null;
    }
}
