using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class CardUnit
{
    public string CardName;
    public StandardCard BaseCard(string CardName)
    {
        return Utils.getCardbyName(CardName,out bool x);
    }
    public string Id;
    public string OwnerId;
    public int Cost;
    public int Power;
    public int Health;
    public int EnergyValue;
    public int FinalScore;
    public string Ability;
    public Sprite CardDisplay;
}


[System.Serializable]
public class CardDataList
{
    public CardUnit[] cards;
}


[Serializable]
public class PlayerState
{
    public string playerId;
    public List<CardUnit> deck = new List<CardUnit>();
    public List<CardUnit> hand = new List<CardUnit>();
    public int score = 0;
    public int energy = 1;
}
