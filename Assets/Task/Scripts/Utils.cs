using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public static class Utils
{
	private static System.Random rng = new System.Random();
	public static void BuildTransformListA<T>(this GameObject parent, ICollection<T> transforms)
	{
		if (parent == null) { return; }
		if (null == parent)
			return;

		foreach (Transform child in parent.transform)
		{
			if (null == child)
				continue;
			if (child.GetComponent<T>() != null)
				transforms.Add(child.GetComponent<T>());
			BuildTransformListA<T>(child.gameObject, transforms);
		}
	}
	public static Transform FindRecursive(this Transform self, Func<Transform, bool> selector)
	{
		foreach (Transform child in self)
		{
			if (selector(child))
			{
				return child;
			}
			var finding = child.FindRecursive(selector);
			if (finding != null)
			{
				return finding;
			}
		}

		return null;
	}

	/// <summary>
	/// Get card by Name
	/// </summary>
	/// <returns></returns>
	public static StandardCard getCardbyName(string cardname, out bool isjoker)
	{
		isjoker = cardname.Contains("JOKER");
		SUIT suit = getCardSuitByName(cardname);
		CARDRANK rank = getCardRankByName(cardname);
		StandardCard std = new StandardCard(suit, rank, isjoker);
		return std;
	}

	/// <summary>
	/// Get a Card Suit By Name
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public static SUIT getCardSuitByName(string name)
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
	public static CARDRANK getCardRankByName(string name)
	{
		foreach (CARDRANK rank in Enum.GetValues(typeof(CARDRANK)))
		{
			if (name.Contains(rank.ToString()))
				return rank;
		}
		return CARDRANK.ACE;
	}


}

public class CardUtils
{

	/// <summary>
	/// Get a Card Suit By Name
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public static SUIT getCardSuitByName(string name)
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
	public static CARDRANK getCardRankByName(string name)
	{
		foreach (CARDRANK rank in Enum.GetValues(typeof(CARDRANK)))
		{
			if (name.Contains(rank.ToString()))
				return rank;
		}
		return CARDRANK.ACE;
	}
	/// <summary>
	/// Get Card Feature 
	/// </summary>
	/// <param name="id"></param>
	/// <param name="_targetcards"></param>
	/// <returns></returns>
	public static Card GetCard(string id, List<Card> _targetcards)
	{
		for (int i = 0; i < _targetcards.Count; i++)
		{
			if (_targetcards[i].CardId.Contains(id))
				return _targetcards[i];
		}
		return null;
	}
}
