using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;

namespace MidtermNew
{
    public class Books : LibraryItems
    {
        public string Author { get; set; }
        public string Medium { get; set; }

        public Books()
        {

        }

        public Books(string barcode, string title, string checkedOut, string genre, string year, string dueDate, string author, string medium)
            : base(barcode, title, checkedOut, genre, year, dueDate)
        {
            Barcode = barcode;
            Title = title;
            CheckedOut = checkedOut;
            Genre = genre;
            Year = year;
            DueDate = dueDate;
            Author = author;
            Medium = medium;
        }
        public static List<Books> FilterBooksByAuthor(List<Books> bookList)
        {
            Console.WriteLine("What author would you like to search for?");
            string input = Console.ReadLine().ToLower();
            List<Books> bookOptions = new List<Books>();
            if (Regex.IsMatch(input, @"^[a-zA-Z. ']+$"))
            {
                foreach (Books book in bookList)
                {
                    if (book.Author.ToLower().Contains(input))
                    {
                        bookOptions.Add(book);
                    }

                }
                return bookOptions;
            }
            else
            {
                Console.WriteLine("That is not a valid author.\n");
                return Books.FilterBooksByAuthor(bookList);
            }
        }
        public static List<Books> FilterBooksByTitle(List<Books> bookList)
        {
            Console.WriteLine("What title would you like to search for?");
            string input = Console.ReadLine().ToLower();
            List<Books> bookOptions = new List<Books>();
            if (Regex.IsMatch(input, @"^[a-zA-Z. ']+$"))
            {
                foreach (Books book in bookList)
                {
                    if (book.Title.ToLower().Contains(input))
                    {
                        bookOptions.Add(book);
                    }
                }
                return bookOptions;
            }
            else
            {
                Console.WriteLine("That is not a valid title.\n");
                return Books.FilterBooksByTitle(bookList);
            }
        }
        public static List<Books> SearchBooksBy(List<Books> books)
        {
            Console.WriteLine("How would you like to choose a book?\n\t1.View a full list of books\n\t2.Search by title" +
                "\n\t3.Search by author");
            string input = Console.ReadLine();
            if (input == "1")
            {
                return books;
            }
            else if (input == "2")
            {
                List<Books> bookOptions = Books.FilterBooksByTitle(books);
                return bookOptions;
            }
            else if (input == "3")
            {
                List<Books> bookOptions = Books.FilterBooksByAuthor(books);
                return bookOptions;
            }
            else
            {
                Console.WriteLine("That isn't an option.\n");
                return SearchBooksBy(books);
            }
        }
        public static void PrintBookList(List<Books> books)
        {
            int count = 0;
            foreach (Books x in books)
            {
                count++;
                Console.WriteLine($"\t{count}. {x.Title}");
            }
        }
        public static void ChooseBookItemReturn(List<Books> bookOptions)
        {
            if (bookOptions.Count > 0)
            {
                Console.WriteLine("Choose a book:");
                Books.PrintBookList(bookOptions);
                string input = Console.ReadLine();
                if (int.TryParse(input, out int num))
                {
                    if (num > 0 && num <= bookOptions.Count)
                    {
                        int index = num - 1;
                        Console.WriteLine($"\tTitle: {bookOptions[index].Title}\n\tAuthor: {bookOptions[index].Author}\n\tGenre: " +
                            $"{bookOptions[index].Genre}\n\tYear Published: {bookOptions[index].Year}\n\tMedium: {bookOptions[index].Medium}" +
                            $"\n\tBarcode: {bookOptions[index].Barcode}\n\t \n\tStatus:{bookOptions[index].CheckedOut}");
                        Program.CheckAvailabilityReturn(bookOptions[index]);
                    }
                }
                else
                {
                    Console.WriteLine("Input invalid.\n");
                    ChooseBookItemReturn(bookOptions);
                }
            }
            else
            {
                Console.WriteLine("Sorry, there are no matching books.\n");
            }
        }
        public static void ChooseBookItemCheckOut(List<Books> bookOptions)
        {
            if (bookOptions.Count > 0)
            {
                Console.WriteLine("Choose a book:");
                Books.PrintBookList(bookOptions);
                string input = Console.ReadLine();
                if (int.TryParse(input, out int num))
                {
                    if (num > 0 && num <= bookOptions.Count)
                    {
                        int index = num - 1;
                        Console.WriteLine($"\tTitle: {bookOptions[index].Title}\n\tAuthor: {bookOptions[index].Author}\n\tGenre: " +
                            $"{bookOptions[index].Genre}\n\tYear Published: {bookOptions[index].Year}\n\tMedium: {bookOptions[index].Medium}" +
                            $"\n\tBarcode: {bookOptions[index].Barcode}\n\t \n\tStatus:{bookOptions[index].CheckedOut}");
                        Program.CheckAvailabilityCheckOut(bookOptions[index]);
                    }
                }
                else
                {
                    Console.WriteLine("Input invalid.\n");
                    ChooseBookItemCheckOut(bookOptions);
                }
            }
            else
            {
                Console.WriteLine("Sorry, there are no matching books.\n");
            }
        }
        public static void ReadFileBooks(List<Books> books)
        {
            StreamReader reader = new StreamReader("../../Books.txt");
            string line = reader.ReadLine();
            
            while (line != null)
            {
                string[] words = line.Split('|');
                books.Add(new Books(words[0], words[1], words[2], words[3], words[4], words[5], words[6], words[7]));
                line = reader.ReadLine();
            }
            reader.Close();
        }
        public static void WriteFileUpdateBooks(List<Books> addBooks)
        {
            StreamWriter writer = new StreamWriter("../../Books.txt");

            foreach (Books book in addBooks)
            {
                writer.WriteLine($"{book.Barcode}|{book.Title}|{book.CheckedOut}|{book.Genre}|" +
                    $"{book.Year}|{book.DueDate}|{book.Author}|{book.Medium}");
            }
            writer.Close();
        }
        public static void AddNewBook(List<Books> books)
        {
            bool go = true;
            Books newBook = new Books();
            while (go)
            {
                Console.WriteLine("What is the book's title?");
                string input = Console.ReadLine();
                if(!string.IsNullOrWhiteSpace(input))
                    {
                    newBook.Title = input;
                    go = false;
                }
                else
                {
                    Console.WriteLine("That is not a valid title.\n");
                    go = true;
                }
            }
            go= true;
            while (go)
            {
                Console.WriteLine("Who is the author?");
                string input = Console.ReadLine();
                if (Regex.IsMatch(input, @"^[a-zA-Z .']+$"))
                {
                    newBook.Author = input;
                    go = false;
                }
                else
                {
                    Console.WriteLine("That is not a valid author.\n");
                    go = true;
                }
            }
            while (go)
            {
                Console.WriteLine("What genre is this book?");
                string input = Console.ReadLine();
                if (Regex.IsMatch(input, @"^[a-zA-Z /-]+$"))
                {
                    newBook.Genre = input;
                    go = false;
                }
                else
                {
                    Console.WriteLine("That is not a valid year.\n");
                    go = true;
                }
            }
           
            while (go)
            {
                Console.WriteLine("What year was it published?");
                string year = Console.ReadLine();
                if (Regex.IsMatch(year, @"^[0-9]{4}$"))
                {
                    newBook.Year = year;
                    go = false;
                }
                else
                {
                    Console.WriteLine("That is not a valid year.\n");
                    go = true;
                }
            }
            go = true;
            while (go)
            {
                Console.WriteLine("Is it hardcover or softcover?");
                string media = Console.ReadLine().ToLower();
                if (media == "softcover" || media == "hardcover")
                {
                    newBook.Medium = media;
                    go = false;
                }
                else
                {
                    Console.WriteLine("Please enter hardcover or softcover.\n");
                    go = true;
                }
            }
            newBook.CheckedOut = "On shelf";
            newBook.DueDate = "Not checked out";
            string random = Program.RandomString(10);
            newBook.Barcode = $"MT{random}";
            books.Add(newBook);
            Console.WriteLine($"{newBook.Title} was successfully added.\n");

        }
    }
}
