using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class MenuActions
    {
        private readonly SqlCrud _sql;

        public MenuActions()
        {
            SqlCrud sql = new SqlCrud(GetConnectionString());
            _sql = sql;
        }

        public void RunProgram()
        {
            bool toContinue = true;

            while (toContinue)
            {
                ConsoleHelpers.DisplayMenuOptions();

                int choice = ConsoleHelpers.GetInt("Enter the number of what you would like to do: ", true, 1, 5);

                Console.Clear();

                switch (choice)
                {
                    case 1:
                        ViewBooks();
                        ConsoleHelpers.Return();
                        break;
                    case 2:
                        AddBook();
                        ConsoleHelpers.Return();
                        break;
                    case 3:
                        UpdateBook();
                        ConsoleHelpers.Return();
                        break;
                    case 4:
                        RemoveBook();
                        ConsoleHelpers.Return();
                        break;
                    case 5:
                        toContinue = false;
                        break;
                    default:
                        throw new ArgumentException();
                }

            }
            Console.WriteLine("You have exited the Library Management App.");
        }

        public void RemoveBook()
        {
            int id = ConsoleHelpers.GetInt("Enter the Id of the book: ", false);

            var book = _sql.GetBookById(id);

            if (book == null)
            {
                Console.WriteLine("\nThere is no book by this Id.");
            }
            else
            {
                Console.WriteLine($"\nThe selected book: {book.Author}: {book.Title}");

                string validation = "";

                while (validation != "y" && validation != "n")
                {
                    validation = ConsoleHelpers.GetString("\nAre your sure you want to remove this book (y/n): ").ToLower();

                    if (validation == "y")
                    {
                        _sql.RemoveBook(id);
                        Console.WriteLine("\nThe book has been removed.");
                    }
                    else if (validation == "n")
                    {
                        return;
                    } 
                }
            }
        }

        public void UpdateBook()
        {
            int id = ConsoleHelpers.GetInt("Enter the Id of the book: ", false);

            var book = _sql.GetBookById(id);

            if (book == null)
            {
                Console.WriteLine("\nThere is no book by this Id.");
            }
            else
            {
                string validation = "";

                Console.WriteLine($"\nThe selected book: {book.Author}: {book.Title}, {book.PublishedYear}, {book.Genre}");

                while (validation != "y" && validation != "n")
                {
                    validation = ConsoleHelpers.GetString("\nIs this the right book (y/n): ").ToLower();

                    if (validation == "y")
                    {
                        ConsoleHelpers.DisplayUpdateOptions();
                        int fieldNumber = ConsoleHelpers.GetInt("Enter the number: ", true, 1, 4);
                        string fieldToUpdate = ConsoleHelpers.GetFieldToUpdate(fieldNumber);

                        var newValue = ConsoleHelpers.GetUpdatedValue(fieldToUpdate, "Enter the new value: ");

                        _sql.UpdateBook(fieldToUpdate, newValue, id);

                        Console.WriteLine("\nThe book has been updated.");
                    }
                    else if (validation == "n")
                    {
                        return;
                    }
                }
            }
        }

        public void ViewBooks()
        {
            var books = _sql.GetAllBooks();

            Console.WriteLine($"Number of books: {books.Count}");
            Console.WriteLine();

            foreach (var b in books)
            {
                Console.WriteLine($"{b.Id} - {b.Author}: {b.Title}, {b.PublishedYear}, {b.Genre}");
            }
        }

        public void AddBook()
        {
            BookModel book = new BookModel
            {
                Title = ConsoleHelpers.GetString("Enter the title of the book: "),
                Author = ConsoleHelpers.GetString("Enter the author of the book: "),
                PublishedYear = ConsoleHelpers.GetInt("Enter the year it was published: ", true, 0, 2035),
                Genre = ConsoleHelpers.GetString("Enter the genre of the book: ")
            };

            _sql.CreateBook(book);

            Console.WriteLine("\nThe book has been added.");
        }

        private static string GetConnectionString(string connectionString = "Default")
        {
            string output = "";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            output = config.GetConnectionString(connectionString);

            return output;
        }

    }
}
