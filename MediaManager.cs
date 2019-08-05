using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermNew
{
    class MediaManager
    {
        public List<Book> Books { get; set; }
        public List<Movie> Movies { get; set; }
        public List<Music> Music { get; set; }

        public MediaManager()
        {
            //Creating 3 lists. One for books to be placed, one for music, and one for movies
            Books = new List<Book>();
            Music = new List<Music>();
            Movies = new List<Movie>();

            //populating the lists with the text files
            BookManager.ReadFile(Books);
            MusicManager.ReadFile(Music);
            MovieManager.ReadFile(Movies);
        }

        internal void Save()
        {
            //write everything back to the text files
            BookManager.WriteFile(Books);
            MovieManager.WriteFile(Movies);
            MusicManager.WriteFile(Music);
        }

        public bool ChooseMedia()
        {
            //prompt the user to 
            Console.WriteLine("What would you like to check out?\n\t1. Books\n\t2. Music\n\t3. Movies\n\t4. Exit");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int userChoice))
            {
                if (userChoice == 1)
                {
                    List<Book> bookOptions = BookManager.BookOptions(Books);
                    ChooseBook(bookOptions);
                    return true;
                }
                else if (userChoice == 2)
                {
                    List<Music> musicOptions = MusicManager.MusicOptions(Music);
                    ChooseMusic(musicOptions);
                    return true;
                }
                else if (userChoice == 3)
                {
                    List<Movie> movieOptions = MovieManager.MovieOptions(Movies);
                    ChooseMovie(movieOptions);
                    return true;
                }
                else if (userChoice == 4)
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Sorry, I did not recognize that input. Please try again.\n");
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Sorry, I did not recognize that input. Please try again.\n");
                return true;
            }

        }
        
 
        public static List<Movie> MovieOptions(List<Movie> movies)
        {
            Console.WriteLine("How would you like to choose a movie?\n\t1.View a full list of movies\n\t2.Search by title" +
                "\n3.Search by director");
            string input = Console.ReadLine();
            if (input == "1")
            {
                return movies;
            }
            else if (input == "2")
            {
                List<Movie> movieOptions = MovieManager.FilterByTitle(movies);
                return movieOptions;
            }
            else if (input == "3")
            {
                List<Movie> movieOptions = MovieManager.FilterByDirector(movies);
                return movieOptions;
            }
            else
            {
                Console.WriteLine("That isn't an option.\n");
                return MovieOptions(movies);
            }
        }
        public void DetermineCheckoutBook(LibraryItem item)
        {
            if (item.CheckedOut == "On Shelf")
            {
                Console.WriteLine($"would you like to\n1. check this item out\n2. select a different item?");
            }
            else
            {
                Console.WriteLine($"would you like to\n1. check this item in\n2. select a different item?");
            }
                string response = Console.ReadLine();

            if (response == "1")
            {
                item.CheckInAndOut();
                if (item.CheckedOut == "On Shelf")
                {
                    item.DueDate = "No Due Date";
                    Console.WriteLine($"{item.Title} sucessfully checked back in.");
                }
                else
                {
                    DateTime dueDate = DateTime.Now.AddDays(14);
                    item.DueDate = dueDate.ToShortDateString();
                    Console.WriteLine($"{item.Title} sucessfully checked out. it will be due back on {item.DueDate}");
                }
            }
            else if (response == "2")
            {
                ChooseBook(Books);
            }
        }
        public void DetermineCheckoutMovie(LibraryItem item)
        {
            if (item.CheckedOut == "On Shelf")
            {
                Console.WriteLine($"would you like to\n1. check this item out\n2. select a different item?");
            }
            else
            {
                Console.WriteLine($"would you like to\n1. check this item in\n2. select a different item?");
            }
            string response = Console.ReadLine();

            if (response == "1")
            {
                item.CheckInAndOut();
                if (item.CheckedOut == "On Shelf")
                {
                    item.DueDate = "No Due Date";
                    Console.WriteLine($"{item.Title} sucessfully checked back in.");
                }
                else
                {
                    DateTime dueDate = DateTime.Now.AddDays(14);
                    item.DueDate = dueDate.ToShortDateString();
                    Console.WriteLine($"{item.Title} sucessfully checked out. it will be due back on {item.DueDate}");
                }
            }
            else if (response == "2")
            {
                ChooseMovie(Movies);
            }
        }
        public void DetermineCheckoutMusic(LibraryItem item)
        {
            if (item.CheckedOut == "On Shelf")
            {
                Console.WriteLine($"would you like to\n1. check this item out\n2. select a different item?");
            }
            else
            {
                Console.WriteLine($"would you like to\n1. check this item in\n2. select a different item?");
            }
            string response = Console.ReadLine();

            if (response == "1")
            {
                item.CheckInAndOut();
                if (item.CheckedOut == "On Shelf")
                {
                    item.DueDate = "No Due Date";
                    Console.WriteLine($"{item.Title} sucessfully checked back in.");
                }
                else
                {
                    DateTime dueDate = DateTime.Now.AddDays(14);
                    item.DueDate = dueDate.ToShortDateString();
                    Console.WriteLine($"{item.Title} sucessfully checked out. it will be due back on {item.DueDate}");
                }
            }
            else if (response == "2")
            {
                ChooseMusic(Music);
            }
        }
        public void ChooseBook(List<Book> bookOptions)
        {
            Console.WriteLine("Choose a book:");
            bool isValid = false;
            while (!isValid)
            {
                BookManager.PrintItemList(bookOptions);
                string input = Console.ReadLine();
                if (int.TryParse(input, out int num))
                {
                    if (num > 0 && num <= bookOptions.Count)
                    {
                        int index = num - 1;
                        Book matchedBook = null;

                        //loop through the original list of books created at the start of the program
                        foreach (Book book in Books)
                        {
                            //if the book's barcode in the filtered list matches a barcode in the original list,
                            if (book.Barcode == bookOptions[index].Barcode)
                            {
                                //the matched book becomes a reference to the original book.
                                matchedBook = book;
                            }
                        }
                        isValid = true;
                        Console.WriteLine(matchedBook.BookDetails());
                        DetermineCheckoutBook(matchedBook);
                    }
                    else
                    {
                        isValid = false;
                        Console.WriteLine($"Entry not understood. Please enter a number, 1 - {bookOptions.Count}");
                    }
                }
                else
                {
                    Console.WriteLine($"Entry not understood. Please enter a number, 1 - {bookOptions.Count}");
                    isValid = false;
                }
            }
        }
        public void ChooseMusic(List<Music> musicOptions)
        {
            Console.WriteLine("Choose your music:");
            bool isValid = false;
            while (!isValid)
            {
                MusicManager.PrintItemList(musicOptions);
                string input = Console.ReadLine();
                if (int.TryParse(input, out int num))
                {
                    if (num > 0 && num <= musicOptions.Count)
                    {
                        int index = num - 1;
                        Music matchedMusic = null;

                        //loop through the original list of music created at the start of the program
                        foreach (Music music in Music)
                        {
                            //if the book's barcode in the filtered list matches a barcode in the original list,
                            if (music.Barcode == musicOptions[index].Barcode)
                            {
                                //the matched book becomes a reference to the original book.
                                matchedMusic = music;
                            }
                        }
                        isValid = true;
                        Console.WriteLine(matchedMusic.MusicDetails());
                        DetermineCheckoutMusic(matchedMusic);
                    }
                    else
                    {
                        isValid = false;
                        Console.WriteLine($"Entry not understood. Please enter a number, 1 - {musicOptions.Count}");
                    }
                }
                else
                {
                    Console.WriteLine($"Entry not understood. Please enter a number, 1 - {musicOptions.Count}");
                    isValid = false;
                }
            }
        }
        public void ChooseMovie(List<Movie> movieOptions)
        {
            Console.WriteLine("Choose a movie:");
            bool isValid = false;
            while (!isValid)
            {
                MovieManager.PrintItemList(movieOptions);
                string input = Console.ReadLine();
                if (int.TryParse(input, out int num))
                {
                    if (num > 0 && num <= movieOptions.Count)
                    {
                        int index = num - 1;
                        Movie matchedMovie = null;

                        //loop through the original list of movies created at the start of the program
                        foreach (Movie movie in Movies)
                        {
                            //if the book's barcode in the filtered list matches a barcode in the original list,
                            if (movie.Barcode == movieOptions[index].Barcode)
                            {
                                //the matched movie becomes a reference to the original book.
                                matchedMovie = movie;
                            }
                        }
                        isValid = true;
                        Console.WriteLine(matchedMovie.MovieDetails());
                        DetermineCheckoutMovie(matchedMovie);
                    }
                    else
                    {
                        isValid = false;
                        Console.WriteLine($"Entry not understood. Please enter a number, 1 - {movieOptions.Count}");
                    }
                }
                else
                {
                    Console.WriteLine($"Entry not understood. Please enter a number, 1 - {movieOptions.Count}");
                    isValid = false;
                }
            }
        }
    }
}
