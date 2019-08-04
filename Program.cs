using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MidtermNew
{
    class Program
    {

        //return path needs to be switched to ReturnMedia() and return media needs to be fixed starting at check availability
        static void Main(string[] args)
        {
            //pulling info from text files and setting it to lists
            List<Books> books0 = new List<Books>();
            List<Music> music0 = new List<Music>();
            List<Movies> movies0 = new List<Movies>();
            Books.ReadFileBooks(books0);
            Music.ReadFileMusic(music0);
            Movies.ReadFileMovies(movies0);

            //alphebatizes the lists by title
            List<Books> books = books0.OrderBy(x => x.Title).ToList();
            List<Movies> movies = movies0.OrderBy(x => x.Title).ToList();
            List<Music> music = music0.OrderBy(x => x.Title).ToList();


            //main program
            Console.WriteLine("Welcome to the Grand Circus Library!\n");

            bool runProgram = true;
            while (runProgram)
            {
                runProgram = ReturnOrGet(books, music, movies);
            }

            //used to add new stuff to text files
            //List<Books> addBook = new List<Books>();
            //List<Music> addMusic = new List<Music>();
            //List<Movies> addMovies = new List<Movies>();
        }
        #region Methods
        public static bool Continue()
        {
            Console.WriteLine($"Press {'y'} to continue or press any other key to exit.");
            char response = Console.ReadKey(true).KeyChar;
            if (response == 'y')
            {
                return true;
            }
            else
            {

                return false;
            }
        }
        public static bool ReturnOrGet(List<Books> books, List<Music> music, List<Movies> movies)
        {
            Console.WriteLine("Would you like to\n\t1.Check out a new item\n\t2.Return an item");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int userChoice))
            {
                if (userChoice == 1)
                {
                    CheckOutMedia(books, music, movies);
                    return true;
                }
                else if (userChoice == 2)
                {
                    ReturnMedia(books, music, movies);
                    return true;
                }
                else
                {
                    return ReturnOrGet(books, music, movies);
                }
            }
            return ReturnOrGet(books, music, movies);
        }

        public static bool ReturnMedia(List<Books> books, List<Music> music, List<Movies> movies)
        {
            bool go = true;
            while (go)
            {
                Console.WriteLine("What would you like to return?\n\t1.Book\n\t2.Music\n\t3.Movie\n\t4.Exit");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int userChoice))
                {
                    if (userChoice == 1)
                    {
                        List<Books> bookOptions = Books.SearchBooksBy(books);
                        Books.ChooseBookItemReturn(bookOptions);
                        go = false;
                        return true;
                    }
                    else if (userChoice == 2)
                    {
                        List<Music> musicOptions = Music.SearchMusicBy(music);
                        Music.ChooseMusicItemReturn(musicOptions);
                        go = false;
                        return true;
                    }
                    else if (userChoice == 3)
                    {
                        List<Movies> movieOptions = Movies.SearchMoviesBy(movies);
                        Movies.ChooseMoviesItemReturn(movieOptions);
                        go = false;
                        return true;
                    }
                    else if (userChoice == 4)
                    {
                        go = false;
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Sorry, I did not recognize that input. Please try again.\n");
                        go = true;
                        return ReturnMedia(books, music, movies);
                    }
                }
                else
                {
                    Console.WriteLine("Sorry, I did not recognize that input. Please try again.\n");
                    go = true;
                    return ReturnMedia(books, music, movies);
                }
            }
            return CheckOutMedia(books, music, movies);
        }
        public static bool CheckOutMedia(List<Books> books, List<Music> music, List<Movies> movies)
        {
            bool go = true;
            while (go)
            {
                Console.WriteLine("What would you like to check out?\n\t1.Books\n\t2.Music\n\t3.Movies\n\t4.Exit");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int userChoice))
                {
                    if (userChoice == 1)
                    {
                        List<Books> bookOptions = Books.SearchBooksBy(books);
                        Books.ChooseBookItemCheckOut(bookOptions);
                        go = false;
                        return true;
                    }
                    else if (userChoice == 2)
                    {
                        List<Music> musicOptions = Music.SearchMusicBy(music);
                        Music.ChooseMusicItemCheckOut(musicOptions);
                        go = false;
                        return true;
                    }
                    else if (userChoice == 3)
                    {
                        List<Movies> movieOptions = Movies.SearchMoviesBy(movies);
                        Movies.ChooseMoviesItemCheckOut(movieOptions);
                        go = false;
                        return true;
                    }
                    else if (userChoice == 4)
                    {
                        go = false;
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Sorry, I did not recognize that input. Please try again.\n");
                        go = true;
                        return CheckOutMedia(books, music, movies);
                    }
                }
                else
                {
                    Console.WriteLine("Sorry, I did not recognize that input. Please try again.\n");
                    go = true;
                    return CheckOutMedia(books, music, movies);
                }
            }
            return CheckOutMedia(books, music, movies);
        }
        public static LibraryItems ChooseOption(List<LibraryItems> list)
        {
            string input = Console.ReadLine();
            bool isValid = Validator.IsInt(input);
            if (isValid)
            {
                isValid = Validator.IsInRange(int.Parse(input), 1, list.Count);
                if (isValid)
                {
                    return list[(int.Parse(input)) - 1];
                }
                else
                {
                    return ChooseOption(list);
                }
            }
            else
            {
                return ChooseOption(list);
            }
        }
        public void CheckInAndOut(LibraryItems item)
        {

            if (item.CheckedOut == "yes")
            {
                Console.WriteLine("Checked out.");
            }
            else
            {
                Console.WriteLine("Sorry, I did not recognize that input. Please try again\n");
            }
        }
        public static void CheckAvailabilityCheckOut(LibraryItems item)
        {
            if (item.CheckedOut == "Available")
            {
                Console.WriteLine($"{item.Title} is available. Would you like to check it out?");
                string input = Console.ReadLine().ToLower();
                if (Validator.IsYesNo(input))
                {
                    if(input == "yes" || input == "y")
                    {
                        Console.WriteLine($"{item.Title} was checked out.");
                        CheckInCheckOut(item);
                        ChangeDueDate(item);
                    }
                    else if(input == "no" || input == "n")
                    {
                        Console.WriteLine($"{item.Title} was not checked out.\n");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input.\n");
                    CheckAvailabilityCheckOut(item);
                }
            }
            else if (item.CheckedOut == "Not available")
            {
                Console.WriteLine($"{item.Title} is not available.\n");
            }
        }
        public static void CheckAvailabilityReturn(LibraryItems item)
        {
            if (item.CheckedOut == "Available")
            {
                Console.WriteLine("This item has not yet been checked out.");
            }
            else if (item.CheckedOut == "Not available")
            {
                Console.WriteLine($"{item.Title} is due back on {item.DueDate}.\n");
                Console.WriteLine("Would you like to return this item?");
                string input = Console.ReadLine();
                if (Validator.IsYesNo(input))
                {
                    if (input == "yes" || input == "y")
                    {
                        Console.WriteLine($"{item.Title} successfully returned.\n");
                        CheckInCheckOut(item);
                        ChangeDueDate(item);
                    }
                    else if (input == "no" || input == "n")
                    {
                        Console.WriteLine($"{item.Title} was not returned.\n");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input.\n");
                    CheckAvailabilityCheckOut(item);
                }
            }
        }
        public static void ChangeDueDate(LibraryItems item)
        {
            if(item.DueDate == "Not checked out")
            {
                DateTime currentDate = DateTime.Now; 
                double timeToAdd = 14d;
                string isDue = currentDate.AddDays(timeToAdd).ToString("MM/dd/yyyy");
                item.DueDate = isDue;
            }
            else
            {
                item.DueDate = "Not checked out";
            }
        }
        public static void CheckInCheckOut(LibraryItems item)
        {
            if (item.CheckedOut == "Available")
            {
                item.CheckedOut = "Not available";
            }
            else if (item.CheckedOut == "Not available")
            {
                item.CheckedOut = "Available";
            }
        }
        #endregion
    }
}
