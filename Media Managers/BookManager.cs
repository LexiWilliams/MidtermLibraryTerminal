using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MidtermNew
{
    class BookManager
    {
        //create a constant, unchangeable string to represent the file path. To be used in both read and write
        private const string BOOK_FILE_PATH = "../../Books.txt";

        public static List<Book> FilterByTitle(List<Book> bookList)
        {
            //ask the user what book they're looking for.
            Console.WriteLine("What title would you like to search for?");
            
            //set the user's input to lower for easier matching
            string input = Console.ReadLine().ToLower();
            
            //create a new list for the filtered books to be assigned to
            List<Book> filteredBooks = new List<Book>();
            
            //go through each book and add it to the the list if the title contains the searched for string
                    foreach (Book book in bookList)
                    {
                        if (book.Title.ToLower().Contains(input))
                        {
                            filteredBooks.Add(book);
                        }
                    }
                    return filteredBooks;   
        }

        public static List<Book> FilterByAuthor(List<Book> bookList)
        {
            //ask the user what author they're looking for.
            Console.WriteLine("What author would you like to search for?");

            //set the user's input to lower for easier matching
            string input = Console.ReadLine().ToLower();

            //create a new list for the filtered books to be assigned to
            List<Book> filteredBooks = new List<Book>();
                foreach (Book book in bookList)
                {
                    if (book.Author.ToLower().Contains(input))
                    {
                        filteredBooks.Add(book);
                    }
                }
                return filteredBooks;
        }
        public static void ReadFile(List<Book> books)
        {


            using (StreamReader reader = new StreamReader(BOOK_FILE_PATH))
            {
                string line = reader.ReadLine();
                //read each line, split the line into the respective values and assign the properties to the new book object
                while (line != null)
                {
                    //create a new book per line
                    Book newBook = new Book();
                    newBook.Deserialize(line);
                    books.Add(newBook);
                    //move on to the next line
                    line = reader.ReadLine();
                }
            }
        }
        internal static void PrintItemList(List<Book> bookOptions)
        {
            int counter = 0;
            foreach(Book book in bookOptions)
            {
                counter++;
                Console.WriteLine($"{counter}. {book.Title}");
            }
        }
        internal static void WriteFile(List<Book> books)
        {
            //using statement automatically closes the connection to the txt file at the end of the scope
            //open streamwriter, point it to the book file path, and overwrite what's there
            using (StreamWriter writer = new StreamWriter(BOOK_FILE_PATH, false))
            {
                //loop over books
                foreach (Book book in books)
                {
                    //write each line (book) to the txt file
                    writer.WriteLine(book.Serialize());
                }
            };
        }
        public static List<Book> BookOptions(List<Book> books)
        {
            List<Book> returnList = new List<Book>();
            Console.WriteLine("How would you like to choose a book?\n1.View a full list of books\n2.Search by title\n3.Search by author");
            bool isValid = false;
            while (!isValid)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int userChoice))
                {
                    if (userChoice == 1)
                    {
                        returnList = books;
                    }
                    else if (userChoice == 2)
                    {
                        returnList = FilterByTitle(books);
                    }
                    else if (userChoice == 3)
                    {
                        returnList = FilterByAuthor(books);
                    }
                    isValid = returnList.Count > 0;
                    if (!isValid)
                    {
                        Console.WriteLine("No books found matching search criteria.\nHow would you like to choose a book?\n1.View a full list of books\n2.Search by title\n3.Search by author");
                    }
                }
                else
                {
                    Console.WriteLine("Not a valid option. Please use a number between 1 and 3.");
                    isValid = false;
                }
            }
            return returnList;
        }
        
    }
}
