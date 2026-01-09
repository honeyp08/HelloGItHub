using System;
using System.Xml.Serialization;

class Calculator
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("=== Advanced Calculator ===");
            Console.WriteLine("1. Addition");
            Console.WriteLine("2. Subtraction");
            Console.WriteLine("3. Multiplication");
            Console.WriteLine("4. Division");
            Console.WriteLine("5. Modulus");
            Console.WriteLine("6. Power");
            Console.WriteLine("7. Square Root");
            Console.WriteLine("8. Factorial");

            Console.Write("Choose an option (1-8): ");
            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice >= 1 && choice <= 6)
            {
                Console.Write("Enter first number: ");
                double a = Convert.ToDouble(Console.ReadLine());

                Console.Write("Enter second number: ");
                double b = Convert.ToDouble(Console.ReadLine());

                Calculate(choice, a, b);
            }
            else if (choice == 7)
            {
                Console.Write("Enter number: ");
                double num = Convert.ToDouble(Console.ReadLine());

                if (num < 0)
                    throw new Exception("Square root of negative number is not allowed.");

                Console.WriteLine($"Result: {Math.Sqrt(num)}");
            }
            else if (choice == 8)
            {
                Console.Write("Enter number: ");
                int num = Convert.ToInt32(Console.ReadLine());

                if (num < 0)
                    throw new Exception("Factorial of negative number is not allowed.");

                Console.WriteLine($"Result: {Factorial(num)}");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input format. Please enter numbers only.");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Cannot divide by zero.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Calculator program ended.");
        }
    }  
    static void Calculate(int choice, double a, double b)
    {
        switch (choice)
        {
            case 1:
                Console.WriteLine($"Result: {a + b}");
                break;
            case 2:
                Console.WriteLine($"Result: {a - b}");
                break;
            case 3:
                Console.WriteLine($"Result: {a * b}");
                break;
            case 4:
                if (b == 0)
                    throw new DivideByZeroException();
                Console.WriteLine($"Result: {a / b}");
                break;
            case 5:
                Console.WriteLine($"Result: {a % b}");
                break;
            case 6:
                Console.WriteLine($"Result: {Math.Pow(a, b)}");
                break;
        }
    }

    static long Factorial(int n)
    {
        long fact = 1;
        for (int i = 1; i <= n; i++)
            fact *= i;
        return fact;
    }
}
