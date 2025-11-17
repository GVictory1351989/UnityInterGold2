using System;

public enum SUIT
{
    SPADE,
    HEART,
    CLUB,
    DIAMOND,
    JOKER
}

public enum CARDRANK
{
    ACE,
    TWO,
    THREE,
    FOUR,
    FIVE,
    SIX,
    SEVEN,
    EIGHT,
    NINE,
    TEN,
    JACK,
    QUEEN,
    KING,
    PRINTED   // for Joker
}

public class StandardCard
{
    public SUIT suit;
    public CARDRANK rank;
    public bool isJoker;

    public int CardNumber;
    public int Score;

    public StandardCard(SUIT s, CARDRANK r, bool joker = false)
    {
        suit = s;
        rank = r;
        isJoker = joker;

        ComputeValues();
    }
    public  SUIT getCardSuitByName(string name)
    {
        foreach (SUIT _suit in Enum.GetValues(typeof(SUIT)))
        {
            if (name.Contains(_suit.ToString()))
                return _suit;
        }
        return SUIT.JOKER;
    }
    /// <summary>
    /// Get a CardRank by name of gameobject 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public  CARDRANK getCardRankByName(string name)
    {
        ////MyConsole.Log ("GET CARD NAME " + name);
        foreach (CARDRANK rank in Enum.GetValues(typeof(CARDRANK)))
        {
            // My//MyConsole.Log("RANK " + rank + "  , NAME OF CARD   " + name);
            if (name.Contains(rank.ToString()))
                return rank;
        }
        return CARDRANK.ACE;
    }
    // STRING constructor if server needs it
    public StandardCard(string id)
    {
        // convert id → suit + rank
        id = id.ToUpper();

        suit = getCardSuitByName(id);
        rank =getCardRankByName(id);

        isJoker = id.Contains("JOKER") || id.Contains("PRINTED");

        ComputeValues();
    }

    private void ComputeValues()
    {
        // CARD NUMBER
        if (isJoker)
            CardNumber = 0;
        else if (rank == CARDRANK.PRINTED)
            CardNumber = 14;
        else if (rank == CARDRANK.ACE)
            CardNumber = 13;
        else
            CardNumber = (int)rank;

        // SCORE
        if (isJoker)
        {
            Score = 0;
        }
        else if (rank == CARDRANK.ACE || rank == CARDRANK.JACK ||
                 rank == CARDRANK.QUEEN || rank == CARDRANK.KING)
        {
            Score = 10;
        }
        else
        {
            Score = ((int)rank) + 1;
        }
    }

    public string GetID()
    {
        if (isJoker)
            return "JOKER_" + rank.ToString();

        return suit.ToString() + "_" + rank.ToString();
    }

    public bool SameCard(StandardCard other)
    {
        return suit == other.suit && rank == other.rank && isJoker == other.isJoker;
    }
}
