public class Library
    {
        private List<Book> books = new();
        private readonly string filePath = Path.Combine("Data", "books.json");

        public void LoadBooks()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                books = JsonSerializer.Deserialize<List<Book>>(json) ?? new();
            }
        }

        public void SaveBooks()
        {
            string json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public void ShowBookList()
        {
            Console.WriteLine("====================================");
            Console.WriteLine("         LIBRARY BOOK LIST");
            Console.WriteLine("====================================");
            Console.WriteLine("ID   TITLE");
            Console.WriteLine("------------------------------------");

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Id,-4} {book.Title}");
            }

            Console.WriteLine("------------------------------------");
        }

        public Book GetBookById(int id)
        {
            return books.FirstOrDefault(b => b.Id == id);
        }

        public void ShowBookDetails(Book book)
        {
            Console.WriteLine("====================================");
            Console.WriteLine("         BOOK DETAILS");
            Console.WriteLine("====================================");
            Console.WriteLine($"Book ID        : {book.Id}");
            Console.WriteLine($"Title          : {book.Title}");
            Console.WriteLine($"Author         : {book.Author}");
            Console.WriteLine($"Category       : {book.Category}");
            Console.WriteLine($"Total Copies   : {book.TotalCopies}");
            Console.WriteLine($"Available Copies: {book.AvailableCopies}");
            Console.WriteLine("====================================");
        }

        public void PurchaseBook(Book book)
        {
            if (book.AvailableCopies > 0)
            {
                book.AvailableCopies--;
                Console.WriteLine(" Book purchased successfully.");
                SaveBooks();
            }
            else
            {
                Console.WriteLine(" No copies available.");
            }
        }

        public void ReturnBook(Book book)
        {
            if (book.AvailableCopies < book.TotalCopies)
            {
                book.AvailableCopies++;
                Console.WriteLine(" Book returned successfully.");
                SaveBooks();
            }
            else
            {
                Console.WriteLine(" All copies already in library.");
            }
        }

        public void ShowFinalStatus(Book book)
        {
            Console.WriteLine("====================================");
            Console.WriteLine("         FINAL BOOK STATUS");
            Console.WriteLine("====================================");
            Console.WriteLine($"Title            : {book.Title}");
            Console.WriteLine($"Available Copies : {book.AvailableCopies}");
            Console.WriteLine($"Total Copies     : {book.TotalCopies}");
            Console.WriteLine("====================================");
        }
    }
