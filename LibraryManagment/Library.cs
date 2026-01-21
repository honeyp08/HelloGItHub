using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class Library
{
    private List<Book> books = new();
    private readonly string filePath =
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Books.json");

    // Load books from JSON
    public void LoadBooks()
    {
        if (!File.Exists(filePath))
        {
            books = new List<Book>();
            Console.WriteLine(" books.json not found.");
            return;
        }

        string json = File.ReadAllText(filePath);

        books = JsonSerializer.Deserialize<List<Book>>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Book>();

        Console.WriteLine($" Loaded {books.Count} book(s) successfully.");
    }

    // Save books to JSON
    private void SaveBooks()
    {
        string json = JsonSerializer.Serialize(
            books,
            new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(filePath, json);
    }

    // Show short book list
    public void ShowBookList()
    {
        if (!books.Any())
        {
            Console.WriteLine("\n No books available in the library.\n");
            return;
        }

        Console.WriteLine("\n==============================================================");
        Console.WriteLine("                         LIBRARY BOOK LIST");
        Console.WriteLine("==============================================================");
        Console.WriteLine($"{"ID",-5} {"TITLE",-30} {"AUTHOR",-20}");
        Console.WriteLine("--------------------------------------------------------------");

        foreach (var book in books)
        {
            Console.WriteLine($"{book.Id,-5} {book.Title,-30} {book.Author,-20}");
        }

        Console.WriteLine("==============================================================\n");
    }

    // Get book by ID
    public Book? GetBookById(int id)
    {
        return books.FirstOrDefault(b => b.Id == id);
    }

    // Show full book details
    public void ShowBookDetails(Book? book)
    {
        if (book == null)
        {
            Console.WriteLine(" Book not found.");
            return;
        }

        Console.WriteLine("\n==============================================================");
        Console.WriteLine("                         BOOK DETAILS");
        Console.WriteLine("==============================================================");
        Console.WriteLine($"Book ID          : {book.Id}");
        Console.WriteLine($"Title            : {book.Title}");
        Console.WriteLine($"Author           : {book.Author}");
        Console.WriteLine($"Category         : {book.Category}");
        Console.WriteLine($"Total Copies     : {book.TotalCopies}");
        Console.WriteLine($"Available Copies : {book.AvailableCopies}");
        Console.WriteLine("==============================================================\n");
    }

    // Purchase book
    public void PurchaseBook(Book? book)
    {
        if (book == null)
        {
            Console.WriteLine(" Invalid book selection.");
            return;
        }

        if (book.AvailableCopies <= 0)
        {
            Console.WriteLine(" Book is currently unavailable.");
            return;
        }

        book.AvailableCopies--;
        SaveBooks();

        Console.WriteLine(" Book purchased successfully.");
    }

    // Return book
    public void ReturnBook(Book? book)
    {
        if (book == null)
        {
            Console.WriteLine(" Invalid book selection.");
            return;
        }

        if (book.AvailableCopies >= book.TotalCopies)
        {
            Console.WriteLine(" All copies are already in the library.");
            return;
        }

        book.AvailableCopies++;
        SaveBooks();

        Console.WriteLine(" Book returned successfully.");
    }

    // Final status display
    public void ShowFinalStatus(Book? book)
    {
        if (book == null) return;

        Console.WriteLine("\n==============================================================");
        Console.WriteLine("                     FINAL BOOK STATUS");
        Console.WriteLine("==============================================================");
        Console.WriteLine($"Title            : {book.Title}");
        Console.WriteLine($"Available Copies : {book.AvailableCopies}");
        Console.WriteLine($"Total Copies     : {book.TotalCopies}");
        Console.WriteLine("==============================================================\n");
    }
}