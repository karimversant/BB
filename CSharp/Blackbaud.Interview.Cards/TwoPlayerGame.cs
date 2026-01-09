using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackbaud.Interview.Cards;

/// <summary>
/// Simple helper to play a two-player deal-and-score game using an <see cref="IDeck"/>.
/// Deals cards alternately up to <paramref name="cardsPerPlayer"/> per player (or until the deck is exhausted),
/// prints each player's cards, prints both scores (sum of Rank numeric values) and prints the winner or "Draw".
/// Note: Rank values come from the existing <see cref="Rank"/> enum (Ace = 1, Two = 2, ..., King = 13).
/// </summary>
public static class TwoPlayerGame
{
    /// <summary>
    /// Plays a two-player game using the provided deck and writes results to the console.
    /// </summary>
    /// <param name="player1Name">Name for player 1. If null/empty, defaults to "Player 1".</param>
    /// <param name="player2Name">Name for player 2. If null/empty, defaults to "Player 2".</param>
    /// <param name="deck">Deck to deal from. The method will call <see cref="IDeck.NextCard"/> to deal.</param>
    /// <param name="cardsPerPlayer">Maximum cards to give each player (defaults to 26).</param>
    public static void Play(string? player1Name, string? player2Name, IDeck deck, int cardsPerPlayer = 26)
    {
        if (deck is null) throw new ArgumentNullException(nameof(deck));
        player1Name = string.IsNullOrWhiteSpace(player1Name) ? "Player 1" : player1Name;
        player2Name = string.IsNullOrWhiteSpace(player2Name) ? "Player 2" : player2Name;
        if (cardsPerPlayer < 0) cardsPerPlayer = 0;

        var player1 = new List<Card>(cardsPerPlayer);
        var player2 = new List<Card>(cardsPerPlayer);

        // Deal alternately until both players have cardsPerPlayer or deck is empty.
        int totalToDeal = Math.Min(cardsPerPlayer * 2, deck.RemainingCards);
        bool dealToP1 = true;
        while ((player1.Count + player2.Count) < totalToDeal && !deck.Empty)
        {
            var card = deck.NextCard();
            if (card is null) break;

            if (dealToP1)
                player1.Add(card);
            else
                player2.Add(card);

            dealToP1 = !dealToP1;
        }

        // Print each player's hand
        Console.WriteLine($"{player1Name} cards ({player1.Count}):");
        foreach (var c in player1)
            Console.WriteLine($"{c.ToShortString()} - {c}");
        Console.WriteLine();

        Console.WriteLine($"{player2Name} cards ({player2.Count}):");
        foreach (var c in player2)
            Console.WriteLine($"{c.ToShortString()} - {c}");
        Console.WriteLine();

        // Calculate scores: sum of Rank numeric values from the Rank enum
        int score1 = player1.Sum(c => (int)c.Rank);
        int score2 = player2.Sum(c => (int)c.Rank);

        Console.WriteLine($"{player1Name} score: {score1}");
        Console.WriteLine($"{player2Name} score: {score2}");

        if (score1 > score2)
            Console.WriteLine($"Winner: {player1Name}");
        else if (score2 > score1)
            Console.WriteLine($"Winner: {player2Name}");
        else
            Console.WriteLine("Draw");
    }
}
