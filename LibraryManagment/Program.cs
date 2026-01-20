class Program
{
    static void Main()
    {
        Library library = new Library();
        library.LoadBooks();

        library.ShowBookList();

        Console.Write("Enter Book ID to view details: ");
        int id = int.Parse(Console.ReadLine());

        var book = library.GetBookById(id);

        if (book == null)
        {
            Console.WriteLine(" Invalid Book ID");
            return;
        }

        library.ShowBookDetails(book);

        Console.WriteLine("1. Purchase Book");
        Console.WriteLine("2. Return Book");
        Console.WriteLine("3. Exit");
        Console.Write("Choose option: ");

        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                library.PurchaseBook(book);
                break;
            case 2:
                library.ReturnBook(book);
                break;
            case 3:
                return;
            default:
                Console.WriteLine("Invalid option");
                break;
        }

        library.ShowFinalStatus(book);
    }
}