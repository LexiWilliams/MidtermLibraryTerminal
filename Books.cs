using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
            if (Regex.IsMatch(input, @"^[a-zA-Z. ]+$"))
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
            if (Regex.IsMatch(input, @"^[a-zA-Z. ]+$"))
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

    }
}
