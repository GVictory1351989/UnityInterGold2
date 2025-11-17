using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class PlayerUI
{
    [Header("Player Info")]
    public TMP_Text PlayerName;
    public TMP_Text PlayerID;

    [Header("Gameplay Info")]
    public TMP_Text TurnText;
    public TMP_Text Score;
    public TMP_Text Energy;

    [Header("Hand UI")]
    public Transform PlayerHandArea;
    public List<CardUnit> Cards;

    public string playerId;
    public List<CardUnit> deck = new List<CardUnit>();
    public List<CardUnit> hand = new List<CardUnit>();
    public int score = 0;
    public int energy = 1;

}
