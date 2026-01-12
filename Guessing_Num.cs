using System;
class Guessing_num
{
    static void Main(string[] args)
    {
        int Guessing_Num = 666;
        int guess = 0;
        int guess_limit = 3;
        int guess_count = 0;
        bool outofguesses = false;

        while(guess != Guessing_Num && !outofguesses)
        {
            if(guess_count < guess_limit)
            {
                Console.Write("Enter your guessing number: ");
                guess = Convert.ToInt32(Console.ReadLine());
                guess_count++;
            }
            else
            {
                outofguesses = true;
            }
        }
        if (outofguesses)
        {
                    Console.WriteLine("Number Is Wrong");

        }
        else {
                Console.WriteLine("Your Number Is Right , You Are Win");

        }
        Console.ReadLine();
    }
}