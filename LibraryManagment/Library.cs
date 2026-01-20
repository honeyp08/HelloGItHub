using System.Text.Json;
    public class Library
    {
        private List<Book> books = new();
        private readonly string filePath = "books.json";

        public void LoadBooks()
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("books.json not found!");
            Console.WriteLine($"Reading JSON from: {Path.GetFullPath(filePath)}");
            return;
        }

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        string json = File.ReadAllText(filePath);
        books = JsonSerializer.Deserialize<List<Book>>(json, options) ?? new();
    }


    // SHOW BOOK LIST (CLEAN FORMAT)
    public void ShowBookList()
    {
        Console.WriteLine("\n====================================");
        Console.WriteLine("LIBRARY BOOK LIST");
        Console.WriteLine("====================================");
        Console.WriteLine("ID   TITLE");
        Console.WriteLine("------------------------------------");

        foreach (var book in books)
        {
            Console.WriteLine($"{book.Id,-4} {book.Title}");
        }

        Console.WriteLine("------------------------------------");
    }

    // USER SELECTS BOOK
    public void ShowBookDetailsByUserInput()
    {
        Console.Write("\nEnter Book ID to view details: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine(" Invalid input!");
            return;
        }

        var book = books.FirstOrDefault(b => b.Id == id);

        if (book == null)
        {
            Console.WriteLine(" Book not found!");
            return;
        }

        DisplayBookDetails(book);
    }

    // DISPLAY DETAILS (PROFESSIONAL FORMAT)
    private void DisplayBookDetails(Book book)
    {
        Console.WriteLine("\n====================================");
        Console.WriteLine("         BOOK DETAILS");
        Console.WriteLine("====================================");
        Console.WriteLine($"Book ID         : {book.Id}");
        Console.WriteLine($"Title           : {book.Title}");
        Console.WriteLine($"Author          : {book.Author}");
        Console.WriteLine($"Category        : {book.Category}");
        Console.WriteLine($"Total Copies    : {book.TotalCopies}");
        Console.WriteLine($"Available Copies: {book.AvailableCopies}");
        Console.WriteLine("====================================");
    }
}
