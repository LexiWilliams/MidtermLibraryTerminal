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

        }
        public static void FilterBooksByAuthor(List<Books> bookList)
        {
            Console.WriteLine("What author would you like to search for?");
            string input = Console.ReadLine();
            if (Regex.IsMatch(input, @"^[a-zA-Z. ]+$"))
            {
                int counter = 0;
                foreach (Books book in bookList)
                {
                    if (book.Author == input)
                    {
                        counter++;
                        Console.WriteLine($"{counter}. {book.Author}");
                    }
                }
            }
            else
            {
                Console.WriteLine("That is not a valid author.\n");
                Books.FilterBooksByAuthor(bookList);
            }
        }
        public static List<Books> FilterBooksByTitle(List<Books> bookList)
        {
            Console.WriteLine("What title would you like to search for?");
            string input = Console.ReadLine();
            List<Books> bookOptions = new List<Books>();
            if (Regex.IsMatch(input, @"^[a-zA-Z. ]+$"))
            {
                int counter = 0;
                foreach (Books book in bookList)
                {
                    if (book.Title.Contains(input))
                    {
                        counter++;
                        Console.WriteLine($"{counter}. {book.Title}");
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
