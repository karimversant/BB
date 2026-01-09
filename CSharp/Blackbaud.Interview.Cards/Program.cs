namespace Blackbaud.Interview.Cards;

public static class Program
{
    static void Main()
    {
        // Part 1 Create a new deck and deal out all the cards

        Console.WriteLine("**********");
        Console.WriteLine("Part 1 - Create a new deck, shuffle, then deal out all the cards");

        // Create a new deck (depend on the abstraction)
        IDeck deck = Deck.NewDeck();

        // TODO: shuffle the deck
        // C#
        Console.Write("Enter number of times to shuffle: ");
        if (int.TryParse(Console.ReadLine(), out int shuffleCount) && shuffleCount > 0)
        {
            deck.Shuffle(shuffleCount);
        }
        else
        {
            Console.WriteLine("Invalid input. Deck will not be shuffled.");
        }
        //deck.Shuffle(3); // Shuffle 3 times (or any number you want)
        Console.WriteLine("Shuffling...");

        // Prompt to draw N cards
        Console.Write("Enter number of cards to draw now (0 to skip): ");
        if (int.TryParse(Console.ReadLine(), out int drawCount) && drawCount > 0)
        {
            var drawnCards = deck.Draw(drawCount);
            Console.WriteLine($"Drew {drawnCards.Count} card(s):");
            foreach (var card in drawnCards)
            {
                Console.WriteLine($"{card.ToShortString()} - {card}");
            }
        }
        else
        {
            Console.WriteLine("No cards drawn.");
        }

        // Deal all the cards (presentation in Program; deck only provides data)
        var dealt = deck.TakeAll();
        foreach (var card in dealt)
        {
            Console.WriteLine($"{card.ToShortString()} - {card}");
        }

        // Reset the deck (requested call at line 51)
        deck.Reset();

        // Ask the user if they want to sort the deck; if Y then sort
        Console.Write("Do you want to sort the deck by rank then suit? (Y/N): ");
        var sortResp = Console.ReadLine();
        if (!string.IsNullOrEmpty(sortResp) && sortResp.Trim().Equals("Y", StringComparison.OrdinalIgnoreCase))
        {
            deck.SortByRankThenSuit();
            Console.WriteLine("Deck sorted by rank then suit.");
        }
        else
        {
            Console.WriteLine("Deck not sorted.");
        }

        // Print all the cards at the end
        var final = deck.TakeAll();
        foreach (var card in final)
        {
            Console.WriteLine($"{card.ToShortString()} - {card}");
        }

        Console.WriteLine();

        // --- Two player game integration ---
        Console.Write("Do you want to play a 2-player game? (Y/N): ");
        var playResp = Console.ReadLine();
        if (!string.IsNullOrEmpty(playResp) && playResp.Trim().Equals("Y", System.StringComparison.OrdinalIgnoreCase))
        {
            Console.Write("Player 1 name (press Enter for default): ");
            var player1 = Console.ReadLine();
            Console.Write("Player 2 name (press Enter for default): ");
            var player2 = Console.ReadLine();

            Console.Write("Shuffle times before dealing (0 for none): ");
            int shuffleTimes = 0;
            if (int.TryParse(Console.ReadLine(), out var st) && st > 0)
                shuffleTimes = st;

            var gameDeck = Deck.NewDeck();
            if (shuffleTimes > 0)
                gameDeck.Shuffle(shuffleTimes);

            TwoPlayerGame.Play(player1, player2, gameDeck);
        }

        Console.WriteLine("**********");
        Console.WriteLine();
    }
}
