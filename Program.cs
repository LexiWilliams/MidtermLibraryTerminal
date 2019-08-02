using System;
using System.Collections.Generic;
using System.IO;

namespace MidtermNew
{
    class Program
    {
        static void Main(string[] args)
        {
            //pulling info from text files and setting it to lists
            List<Books> books = new List<Books>();
            List<Music> music = new List<Music>();
            List<Movies> movies = new List<Movies>();
            Books.ReadFileBooks(books);
            Music.ReadFileMusic(music);
            Movies.ReadFileMovies(movies);

            //used to add new stuff to text files
            List<Books> addBook = new List<Books>();
            List<Music> addMusic = new List<Music>();
            List<Movies> addMovies = new List<Movies>();

            //main program
            Console.WriteLine("Welcome to the Grand Circus Library!");

            bool runProgram = true;
            while (runProgram)
            {
                runProgram = ChooseMedia(books, music, movies);
            }
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
        public static bool ChooseMedia(List<Books> books, List<Music> music, List<Movies> movies)
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
                        ChooseBookItem(bookOptions);
                        go = false;
                        return true;
                    }
                    else if (userChoice == 2)
                    {
                        List<Music> musicOptions = Music.SearchMusicBy(music);
                        ChooseMusicItem(musicOptions);
                        go = false;
                        return true;
                    }
                    else if (userChoice == 3)
                    {
                        List<Movies> movieOptions = Movies.SearchMoviesBy(movies);
                        ChooseMoviesItem(movieOptions);
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
                        return ChooseMedia(books, music, movies);
                    }
                }
                else
                {
                    Console.WriteLine("Sorry, I did not recognize that input. Please try again.\n");
                    go = true;
                    return ChooseMedia(books, music, movies);
                }
            }
            return ChooseMedia(books, music, movies);
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
        public static void ChooseBookItem(List<Books> bookOptions)
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
                    CheckAvailability(bookOptions[index]);
                }
            }
            else
            {
                Console.WriteLine("Input invalid.\n");
                ChooseBookItem(bookOptions);
            }
        }
        public static void ChooseMusicItem(List<Music> musicOptions)
        {
            Console.WriteLine("Choose an album:");
            Music.PrintMusicList(musicOptions);
            string input = Console.ReadLine();
            if (int.TryParse(input, out int num))
            {
                if (num > 0 && num <= musicOptions.Count)
                {
                    int index = num - 1;
                    Console.WriteLine($"\tTitle: {musicOptions[index].Title}\n\tArtist: {musicOptions[index].Artist}\n\tGenre: " +
                        $"{musicOptions[index].Genre}\n\tYear Published: {musicOptions[index].Year}" +
                        $"\n\tBarcode: {musicOptions[index].Barcode}\n\t \n\tStatus:{musicOptions[index].CheckedOut}");
                    CheckAvailability(musicOptions[index]);
                }
            }
            else
            {
                Console.WriteLine("Input invalid.\n");
                ChooseMusicItem(musicOptions);
            }
        }
        public static void ChooseMoviesItem(List<Movies> movieOptions)
        {
            Console.WriteLine("Choose a movie:");
            Movies.PrintMoviesList(movieOptions);
            string input = Console.ReadLine();
            if (int.TryParse(input, out int num))
            {
                if (num > 0 && num <= movieOptions.Count)
                {
                    int index = num - 1;
                    Console.WriteLine($"\tTitle: {movieOptions[index].Title}\n\tArtist: {movieOptions[index].Director}\n\tGenre: " +
                        $"{movieOptions[index].Genre}\n\tYear Published: {movieOptions[index].Year}" +
                        $"\n\tBarcode: {movieOptions[index].Barcode}\n\t \n\tStatus:{movieOptions[index].CheckedOut}");
                    CheckAvailability(movieOptions[index]);
                }
            }
            else
            {
                Console.WriteLine("Input invalid.\n");
                ChooseMoviesItem(movieOptions);
            }
        }
        public static void CheckAvailability(LibraryItems item)
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
                    CheckAvailability(item);
                }
            }
            else if (item.CheckedOut == "Not available")
            {
                Console.WriteLine($"{item.Title} is not available.\n");
            }
        }
        public static void ChangeDueDate(LibraryItems item)
        {
            if(item.DueDate == "Not checked out")
            {
                DateTime currentDate = DateTime.Now; 
                double timeToAdd = 14d;
                string isDue = currentDate.AddDays(timeToAdd).ToString("dd/MM/yyyy");
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
