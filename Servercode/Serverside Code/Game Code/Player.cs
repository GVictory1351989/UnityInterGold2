using PlayerIO.GameLibrary;
using System;
using System.Collections.Generic;

public class Player : BasePlayer
{
    public string Name;
    public string CurrentRoomId;

    public bool IsReady = false;
    public bool InQueue = false;
    public CardDB CardDB;
    // Deck, hand, discard pile
    public List<int> Deck = new List<int>();
    public List<int> Hand = new List<int>();
    public List<int> DiscardPile = new List<int>();
    public List<int> PlayedThisTurn = new List<int>(); // Cards queued for this turn

    public bool BlockNext = false;
    public int Score = 0;
    public int Energy = 0;
    public int MaxEnergy = 6;
    public int EnergyPerTurn = 2;

    public string LastMove = "";

    /// <summary>
    /// Create a new hardcoded deck of 12 cards and shuffle it
    /// </summary>
    public void CreateDeck()
    {
        Deck.Clear();

        // Hardcode cards 1 to 12
        for (int i = 1; i <= 12; i++)
        {
            Deck.Add(i);
        }

        Shuffle(Deck);

        Console.WriteLine($"Deck created for {Name}: {string.Join(", ", Deck)}");
    }

    /// <summary>
    /// Shuffle a list
    /// </summary>
    private void Shuffle(List<int> list)
    {
        var rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    /// <summary>
    /// Increase energy at the start of the turn
    /// </summary>
    public void RefillEnergy()
    {
        Energy += EnergyPerTurn;
        if (Energy > MaxEnergy)
            Energy = MaxEnergy;

        Console.WriteLine($"{Name} energy refilled to {Energy}/{MaxEnergy}");
    }

    /// <summary>
    /// Draw random cards from deck
    /// </summary>
    public void DrawCards(int count)
    {
        if (Deck.Count == 0)
        {
            Console.WriteLine($"{Name} Deck is empty. Cannot draw cards.");
            return;
        }

        var rng = new Random();

        for (int i = 0; i < count; i++)
        {
            if (Deck.Count == 0) break;

            int index = rng.Next(Deck.Count);
            int cardId = Deck[index];

            Hand.Add(cardId);
            Deck.RemoveAt(index);
        }

        Console.WriteLine($"{Name} drew {Hand.Count} card(s). Hand: {string.Join(", ", Hand)}");
        Console.WriteLine($"Remaining Deck: {string.Join(", ", Deck)}");
    }

    /// <summary>
    /// Play a card from hand (queues for resolution)
    /// </summary>
    public int PlayCard(int handIndex)
    {
        if (handIndex < 0 || handIndex >= Hand.Count)
            return -1; // Invalid index

        int cardId = Hand[handIndex];

        // For now we assume card cost = 1 for all cards
        int cardCost = 1;

        if (Energy < cardCost)
        {
            Console.WriteLine($"{Name} does not have enough energy to play card {cardId}");
            return -2;
        }

        Energy -= cardCost;
        Hand.RemoveAt(handIndex);
        PlayedThisTurn.Add(cardId);
        LastMove = cardId.ToString();

        Console.WriteLine($"{Name} played card {cardId}. Energy left: {Energy}. PlayedThisTurn: {string.Join(", ", PlayedThisTurn)}");
        return cardId;
    }

    /// <summary>
    /// Reset everything for a new game
    /// </summary>
    public void ResetForNewGame()
    {
        Hand.Clear();
        DiscardPile.Clear();
        Deck.Clear();
        PlayedThisTurn.Clear();
        Score = 0;
        Energy = 0;
        LastMove = "";
        IsReady = false;
        InQueue = false;

        Console.WriteLine($"{Name} reset for new game");
    }
}
