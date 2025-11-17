using System;
using System.Collections.Generic;

public class CardData
{
    public StandardCard BaseCard;
    public string UniqueId;
    public string OwnerId;
    public int Cost;
    public int Power;
    public int Health;
    public int EnergyValue;
    public int FinalScore;
    public string Ability;        // single ability
    public bool UsedThisTurn;
    public bool Destroyed;
    public bool CanPlay => !UsedThisTurn && !Destroyed;

    public CardData(StandardCard standard, string ownerId)
    {
        BaseCard = standard;
        OwnerId = ownerId;
        UniqueId = DateTime.UtcNow.Ticks.ToString() + "_" + BaseCard.GetID();
        ApplyGameStats();
    }

    private int Clamp(int value, int min, int max)
    {
        return Math.Max(min, Math.Min(max, value));
    }

    private void ApplyGameStats()
    {
        Cost = Clamp(BaseCard.CardNumber / 3, 1, 6);
        Power = Clamp(BaseCard.CardNumber / 2, 1, 10);
        Health = Clamp(BaseCard.CardNumber / 2 + 1, 1, 12);

        EnergyValue = Cost - 1;
        FinalScore = BaseCard.Score + Power;
        Ability = SuitAbility(BaseCard.suit);
    }

    private string SuitAbility(SUIT s)
    {
        switch (s)
        {
            case SUIT.SPADE: return "Damage:2";   // Attack buff
            case SUIT.HEART: return "Heal:2";     // Heal buff
            case SUIT.CLUB: return "Shield:1";    // Armor
            case SUIT.DIAMOND: return "Draw:1";   // Extra card
            case SUIT.JOKER: return "Wildcard";   // Special
        }
        return "None";
    }
}

public class CardDB
{
    public Dictionary<int, CardData> Cards;

    public CardDB(string ownerId)
    {
        Cards = new Dictionary<int, CardData>()
        {
            {1,  new CardData(new StandardCard(SUIT.SPADE, CARDRANK.TWO), ownerId)},
            {2,  new CardData(new StandardCard(SUIT.SPADE, CARDRANK.THREE), ownerId)},
            {3,  new CardData(new StandardCard(SUIT.HEART, CARDRANK.TWO), ownerId)},
            {4,  new CardData(new StandardCard(SUIT.HEART, CARDRANK.THREE), ownerId)},
            {5,  new CardData(new StandardCard(SUIT.CLUB, CARDRANK.TWO), ownerId)},
            {6,  new CardData(new StandardCard(SUIT.CLUB, CARDRANK.THREE), ownerId)},
            {7,  new CardData(new StandardCard(SUIT.DIAMOND, CARDRANK.FOUR), ownerId)},
            {8,  new CardData(new StandardCard(SUIT.DIAMOND, CARDRANK.FIVE), ownerId)},
            {9,  new CardData(new StandardCard(SUIT.SPADE, CARDRANK.FOUR), ownerId)},
            {10, new CardData(new StandardCard(SUIT.SPADE, CARDRANK.FIVE), ownerId)},
            {11, new CardData(new StandardCard(SUIT.SPADE, CARDRANK.ACE), ownerId)},
            {12, new CardData(new StandardCard(SUIT.HEART, CARDRANK.QUEEN), ownerId)},
        };
    }
}
