using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Books
{
    internal class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static List<Book> books = new List<Book>();
        static int initialBookQuantity;
        static void Main(string[] args)
        {
            log.Info("Program has been started");
            LoadBooksFromCSV("ListOfBooks.csv");
            initialBookQuantity = books.Count;
            FetchBooksFromUser();
            PrintToCSVDialogue();
            SaveBooksToCSV("ListOfBooks.csv");
            Console.ReadLine();
            log.Info("Program has been shut down");
        }

        static void FetchBooksFromUser()
        {
            bool continueLoop = true;

            while (continueLoop == true)
            {
                try
                {
                    Console.WriteLine("Name");
                    string bookName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(bookName) == true)
                        break;

                    Console.WriteLine("Publication Year");
                    int bookYear = int.Parse(Console.ReadLine());
                    Console.WriteLine("Pages");
                    int bookPages = int.Parse(Console.ReadLine());

                    Book book = new Book(bookName, bookPages, bookYear);
                    books.Add(book);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Input not valid. Try Again.");
                }
            }
        }

        static void PrintToCSVDialogue()
        {
            Console.WriteLine("What information will be printed? (everything or title)");
            string response = Console.ReadLine().ToLower();

            switch (response)
            {
                case "everything":
                    PrintToConsole(books);
                    break;
                case "title":
                    string[] titles = new string[books.Count];
                    int index = 0;
                    foreach (Book book in books)
                    {
                        titles[index] = book.Name;
                        index++;
                    }
                    PrintToConsole(titles);
                    break;
                default:
                    Console.WriteLine("Selection not valid");
                    break;
            }
        }

        static void PrintToConsole(List<Book> booksList)
        {
            foreach (Book book in booksList)
            {
                Console.WriteLine($"{book.Name}, {book.Pages} pages, {book.PublicationYear}");
            }
        }

        static void PrintToConsole(string[] titles)
        {
            foreach (string title in titles)
            {
                Console.WriteLine(title);
            }
        }

        static void LoadBooksFromCSV(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] records = line.Split(',');
                        Book book = new Book(records[0], int.Parse(records[1]), int.Parse(records[2]));
                        books.Add(book);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("No file loaded. File could not be found.");
            }
        }

        static void SaveBooksToCSV(string filePath)
        {
            try
            {
                StringBuilder csvContent = new StringBuilder();

                for (int index = initialBookQuantity; index < books.Count(); index++)
                {
                    string line = string.Join(",", books[index].Name, books[index].Pages, books[index].PublicationYear);
                    csvContent.AppendLine(line);
                }

                File.AppendAllText(filePath, csvContent.ToString());
                Console.WriteLine("\nFile has been saved. Press enter to close.");
                log.Info("Books have been succesfully saved to file");
            }
            catch (Exception e)
            {
                log.Error("Error: " + e);
                Console.WriteLine("File not saved. File could not be found.");
            }
        }
    }
}
