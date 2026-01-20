class Program
{
    static void Main()
    {
        Library library = new Library();
        library.LoadBooks();

        library.ShowBookList();
        library.ShowBookDetailsByUserInput();

        Console.WriteLine("\n Press any key to exit...");
        Console.ReadKey();
    }

}
