using System;

class GuessingGame
{
    static void Main(string[] args)
    {
        Console.WriteLine("NUMBER GUESSING GAME");

        do
        {
            PlayGame();
            Console.Write("\nPlay again? (y/n): ");
        }
        while (Console.ReadLine().Trim().ToLower() == "y");

        Console.WriteLine("\nThanks for playing!");
    }

    static void PlayGame()
    {
        int maxNumber;
        int guessLimit;
        int difficultyMultiplier;

        // Difficulty selection
        Console.WriteLine("\nSelect Difficulty Level:");
        Console.WriteLine("1. Easy (1-10)");
        Console.WriteLine("2. Medium (1-50)");
        Console.WriteLine("3. Hard (1-100)");
        Console.Write("Enter choice (1-3): ");

        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                maxNumber = 10;
                guessLimit = 5;
                difficultyMultiplier = 1;
                break;

            case 2:
                maxNumber = 50;
                guessLimit = 7;
                difficultyMultiplier = 2;
                break;

            case 3:
                maxNumber = 100;
                guessLimit = 10;
                difficultyMultiplier = 3;
                break;

            default:
                Console.WriteLine("Invalid choice. Defaulting to Easy.");
                maxNumber = 10;
                guessLimit = 5;
                difficultyMultiplier = 1;
                break;
        }

        Random random = new Random();
        int secretNumber = random.Next(1, maxNumber + 1);

        int guessCount = 0;
        bool isGuessed = false;
        int score = 0;

        Console.WriteLine($"\n Guess a number between 1 and {maxNumber}");

        while (guessCount < guessLimit && !isGuessed)
        {
            Console.Write("Enter your guess: ");
            int guess = int.Parse(Console.ReadLine());
            guessCount++;

            if (guess == secretNumber)
            {
                isGuessed = true;
                int remainingGuesses = guessLimit - guessCount;
                score = (remainingGuesses + 1) * 10 * difficultyMultiplier;

                Console.WriteLine("\n Correct! You guessed the number!");
                Console.WriteLine($"Attempts Used: {guessCount}");
                Console.WriteLine($"Score: {score}");
            }
            else if (guess < secretNumber)
            {
                Console.WriteLine("Too low!");
            }
            else
            {
                Console.WriteLine("Too high!");
            }
        }

        if (!isGuessed)
        {
            Console.WriteLine("\n Out of guesses!");
            Console.WriteLine($"The correct number was: {secretNumber}");
            Console.WriteLine("Score: 0");
        }
    }
}