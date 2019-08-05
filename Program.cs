using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MidtermNew
{
    class Program
    {
        private static Random random = new Random();
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
            List < Movies> movies = movies0.OrderBy(x => x.Title).ToList();
            List<Music> music = music0.OrderBy(x => x.Title).ToList();

            //main program
            Console.WriteLine("Welcome to the Grand Circus Library!\n");

            bool runProgram = true;
            while (runProgram)
            {
                books = books.OrderBy(x => x.Title).ToList();
                movies = movies.OrderBy(x => x.Title).ToList();
                music = music.OrderBy(x => x.Title).ToList();

                runProgram = ReturnOrGet(books, music, movies);
                
            }
            Books.WriteFileUpdateBooks(books); 
            Movies.WriteFileUpdateMovies(movies);
            Music.WriteFileUpdateMusic(music);
        }
        #region Methods
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static void AddNewMedia(List<Books> books, List<Music> music, List<Movies> movies)
        {
            bool go = true;
            while (go)
            {
                Console.WriteLine("Would you like to add\n\t1.New book\n\t2.New Music\n\t3.New Movie\n\t4.Return to main menu\n");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    Books.AddNewBook(books);
                    go = true;
                }
                else if (input == "2")
                {
                    Music.AddNewMusic(music);
                    go = true;
                }
                else if (input == "3")
                {
                    Movies.AddNewMovie(movies);
                    go = true;
                }
                else if (input == "4")
                {
                    go = false;
                }
                else
                {
                    AddNewMedia(books, music, movies);
                }
            }
        }
        public static bool ReturnOrGet(List<Books> books, List<Music> music, List<Movies> movies)
        {
            Console.WriteLine("Would you like to\n\t1.Check out a new item\n\t2.Return an item\n\t3.Add a new item\n\t4.Exit");
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
                else if (userChoice == 3)
                {
                    AddNewMedia(books, music, movies);
                    return true;
                }
                else if (userChoice == 4)
                {
                    Console.WriteLine("Goodbye.");
                    return false;
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
            if (item.CheckedOut == "On shelf")
            {
                Console.WriteLine($"{item.Title} is available. Would you like to check it out?");
                string input = Console.ReadLine().ToLower();
                if (Validator.IsYesNo(input))
                {
                    if (input == "yes" || input == "y")
                    {
                        CheckInCheckOut(item);
                        ChangeDueDate(item);
                        Console.WriteLine($"{item.Title} was checked out. Please return by {item.DueDate}.\n");
                      

                    }
                    else if (input == "no" || input == "n")
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
            else if (item.CheckedOut == "Checked out")
            {
                Console.WriteLine($"{item.Title} is not available.\n");
            }
        }
        public static void CheckAvailabilityReturn(LibraryItems item)
        {
            if (item.CheckedOut == "On shelf")
            {
                Console.WriteLine("This item has not yet been checked out.");
            }
            else if (item.CheckedOut == "Checked out")
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
            if (item.DueDate == "Not checked out")
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
            if (item.CheckedOut == "On shelf")
            {
                item.CheckedOut = "Checked out";
            }
            else if (item.CheckedOut == "Checked out")
            {
                item.CheckedOut = "On shelf";
            }
        }
        #endregion
    }
}
