using System;
using UnityEngine;
/// <summary>
/// Type of a card 
/// </summary>
public enum SUIT
{
	SPADE,
	HEART,
	CLUB,
	DIAMOND,
	JOKER
};
/// <summary>
/// Card Rank 
/// </summary>
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
	PRINTED
};
/// <summary>
/// Standard Card 
/// </summary>
[Serializable]
public class StandardCard
{
	public bool isJoker;

	public int CardNumber = 0;
	/// <summary>
	/// Suit of a caRD
	/// </summary>
	public SUIT suit;
	/// <summary>
	/// rank of a card 
	/// </summary>
	public CARDRANK rank;
	/// <summary>
	/// Suit of a Card
	/// </summary>
	public SUIT Suit
	{
		get
		{
			return suit;
		}
	}
	/// <summary>
	/// Get a Carrd Rank
	/// </summary>
	public CARDRANK CardRank
	{
		get
		{
			return rank;
		}
	}


	private const string JOKER = "JOKER";

	private bool isjoker;
	public (int suitInt, int rankInt) ToServerData()
	{
		return ((int)this.suit, (int)this.rank);
	}
	public (int suitInt, int rankInt, bool isJoker) ToServerDataJoker()
	{
		return ((int)this.suit, (int)this.rank, isjoker);
	}
	public string StandardCardID
	{
		get
		{

			if (isjoker)
			{
				isJoker = this.isjoker;
				if (this.suit.ToString() == "PRINTED")
					return this.suit.ToString() + JOKER;
				else
				{

					if (!string.IsNullOrEmpty(this.suit.ToString()) && !string.IsNullOrEmpty(this.rank.ToString()))
					{
						string str = this.suit.ToString() + this.rank.ToString() + JOKER;
						if (str.Equals("JOKERPRINTEDJOKER"))
						{
							return "JOKERPRINTED";
						}

						if (str.Contains("JOKERJOKER"))
						{
							return this.suit.ToString() + this.rank.ToString();
						}

					}
					return this.suit.ToString() + this.rank.ToString() + JOKER;

				}
			}

			else
			{
				return this.suit.ToString() + this.rank.ToString();
			}

		}
	}
	public int score;
	public StandardCard(string str)
	{
		str = str.ToUpper();
		CardNumber = 0;
		this.suit = CardUtils.getCardSuitByName(str);
		this.rank = CardUtils.getCardRankByName(str);
		this.isjoker = (str.Contains("JOKER") || str.Contains("PRINTED")) ? true : false;
		string cardid = StandardCardID;
		if (cardid.Contains("ACE"))
			this.CardNumber = 13;
		if (!cardid.Contains("ACE") && (!cardid.Contains("PRINTED")))
		{
			this.CardNumber = Convert.ToInt32(rank);
		}


		if (rank == CARDRANK.ACE || rank == CARDRANK.QUEEN ||
			rank == CARDRANK.KING || rank == CARDRANK.JACK)
			score += 10;
		else
			score += Convert.ToInt32(rank + 1);
		if (cardid.Contains("JOKER"))
		{
			isJoker = true;
			score = 0;
		}
		CardNumberOperation();
	}
	//  private bool isCardJoker ;
	/// <summary>
	/// Standard Card of a Rank
	/// </summary>
	/// <param name="_suit"></param>
	/// <param name="_rank"></param>
	public StandardCard(SUIT _suit, CARDRANK _rank, bool isJoker)
	{
		CardNumber = 0;
		this.suit = _suit;
		this.rank = _rank;
		this.isjoker = isJoker;
		string cardid = StandardCardID;
		if (cardid.Contains("PRINTED"))
			this.CardNumber = 14;
		if (cardid.Contains("ACE"))
			this.CardNumber = 13;
		if (!cardid.Contains("ACE") && (!cardid.Contains("PRINTED")))
		{
			this.CardNumber = Convert.ToInt32(_rank);
		}


		if (rank == CARDRANK.ACE || rank == CARDRANK.QUEEN ||
			   rank == CARDRANK.KING || rank == CARDRANK.JACK)
			score += 10;
		else
			score += Convert.ToInt32(rank + 1);
		if (cardid.Contains("JOKER"))
		{
			isJoker = true;
			score = 0;
		}
		CardNumberOperation();
	}
	public bool EqualOtherCard(StandardCard carda)
	{
		return carda.suit == suit && carda.rank == rank;
	}
	public bool EqualCardDataID(StandardCard carda)
	{
		return (this.suit.ToString() + this.rank.ToString()).Equals(carda.suit.ToString() + carda.rank.ToString());
	}
	/// <summary>
	/// Cards the number operation.
	/// </summary>
	public void CardNumberOperation()
	{
		string cardid = StandardCardID;
		if (cardid.Contains("PRINTED"))
			this.CardNumber = 14;
		if (cardid.Contains("ACE"))
			this.CardNumber = 13;
		if (!cardid.Contains("ACE") && (!cardid.Contains("PRINTED")))
		{
			this.CardNumber = Convert.ToInt32(this.rank);
		}
		if (rank == CARDRANK.ACE || rank == CARDRANK.QUEEN ||
			   rank == CARDRANK.KING || rank == CARDRANK.JACK)
			score += 10;
		else
			score += Convert.ToInt32(rank + 1);
		if (cardid.Contains("JOKER"))
		{
			isJoker = true;
			score = 0;
		}

	}

}