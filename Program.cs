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
            ReadFileBooks(books);
            ReadFileMusic(music);
            ReadFileMovies(movies);

            //used to add new stuff to text files
            List<Books> addBook = new List<Books>();
            List<Music> addMusic = new List<Music>();
            List<Movies> addMovies = new List<Movies>();

            //main program
            Console.WriteLine("Welcome to the Grand Circus Library!");

            bool runProgram = true;
            while (runProgram)
            {
                ChooseMedia(books, music, movies);
                books[12].Title = "Fckthis";
                
            }

        }
        #region Read/Write Methods

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
        public static void ReadFileMusic(List<Music> music)
        {
            StreamReader reader = new StreamReader("../../Music.txt");
            string line = reader.ReadLine();
            while (line != null)
            {
                string[] words = line.Split('|');
                music.Add(new Music(words[0], words[1], words[2], words[3], words[4], words[5], words[6]));
                line = reader.ReadLine();
            }
            reader.Close();
        }
        public static void ReadFileMovies(List<Movies> movies)
        {
            StreamReader reader = new StreamReader("../../Movies.txt");
            string line = reader.ReadLine();
            while (line != null)
            {
                string[] words = line.Split('|');
                movies.Add(new Movies(words[0], words[1], words[2], words[3], words[4], words[5], words[6]));
                line = reader.ReadLine();
            }
            reader.Close();
        }
        public static void WriteFileBooks(List<Books> addBooks)
        {
            StreamWriter writer = new StreamWriter("../../Books.txt");

            foreach (Books book in addBooks)
            {
                File.AppendAllText("../../Books.txt", string.Format($"\n{book.Barcode}|{book.Title}|{book.CheckedOut}|{book.Genre}|" +
                    $"{book.Year}|{book.DueDate}|{book.Author}|{book.Medium}"));
            }
            writer.Close();
        }
        public static void WriteFileMusic(List<Music> addMusic)
        {
            StreamWriter writer = new StreamWriter("../../Music.txt");

            foreach (Music music in addMusic)
            {
                File.AppendAllText("../../Music.txt", string.Format($"\n{music.Barcode}|{music.Title}|{music.CheckedOut}|{music.Genre}|" +
                    $"{music.Year}|{music.DueDate}|{music.Artist}"));
            }
            writer.Close();
        }
        public static void WriteFileMovies(List<Movies> addMovies)
        {
            StreamWriter writer = new StreamWriter("../../Movies.txt");

            foreach (Movies movies in addMovies)
            {
                File.AppendAllText("../../movies.txt", string.Format($"\n{movies.Barcode}|{movies.Title}|{movies.CheckedOut}|{movies.Genre}|" +
                    $"{movies.Year}|{movies.DueDate}|{movies.Director}"));
            }
            writer.Close();
        }
        #endregion
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
        public static void ChooseMedia(List<Books> books, List<Music> music, List<Movies> movies)
        {
            bool go = true;
            while (go)
            {
                Console.WriteLine("What would you like to check out?\n\t1.Books\n\t2.Music\n\t3.Movies");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int userChoice))
                {
                    if (userChoice == 1)
                    {
                        List<Books> bookOptions = SearchBooksBy(books);
                        ChooseBookItem(bookOptions);
                        go = false;
                    }
                    else if (userChoice == 2)
                    {
                        List<Music> musicOptions = SearchMusicBy(music);
                        go = false;
                    }
                    else if (userChoice == 3)
                    {
                       List<Movies> movieOptions = SearchMoviesBy(movies);
                        go = false;
                    }
                    else
                    {
                        Console.WriteLine("Sorry, I did not recognize that input. Please try again.\n");
                        go = true;
                    }
                }
                else
                {
                    Console.WriteLine("Sorry, I did not recognize that input. Please try again.\n");
                    go = true;
                }
            }
        }
        public static LibraryItems ChooseOption(List <LibraryItems> list)
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
            PrintBookList(bookOptions);
            string input = Console.ReadLine();
            if(int.TryParse(input, out int num))
            {
                if(num > 0 && num<= bookOptions.Count)
                {
                    int index = num - 1;
                    Console.WriteLine($"\tTitle: {bookOptions[index].Title}\n\tAuthor: {bookOptions[index].Author}\n\tGenre: " +
                        $"{bookOptions[index].Genre}\n\tYear Published: {bookOptions[index].Year}\n\tMedium: {bookOptions[index].Medium}" +
                        $"\n\tBarcode: {bookOptions[index].Barcode}\n\t \n\tStatus:{bookOptions[index].CheckedOut}");
                }
            }
            else
            {
                ChooseBookItem(bookOptions);
            }



        }
        #endregion
        #region Print Methods
        public static void PrintBookList(List<Books> books)
        {
            int count = 0;
            foreach (Books x in books)
            {
                count++;
                Console.WriteLine($"\t{count}. {x.Title}");
            }
        }
        public static void PrintMusicList(List<Music> music)
        {
            int count = 0;
            foreach (Music x in music)
            {
                count++;
                Console.WriteLine($"\t{count}. {x.Title}");
            }
        }
        public static void PrintMoviesList(List<Movies> movies)
        {
            int count = 0;
            foreach (Movies x in movies)
            {
                count++;
                Console.WriteLine($"\t{count}. {x.Title}");
            }
        }
        #endregion
        #region Search Methods
      
        public static  List<Books> SearchBooksBy(List<Books> books)
        {
            Console.WriteLine("How would you like to choose a book?\n1.View a full list of books\n2.Search by title\n3.Search by author");
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
        public static List<Music> SearchMusicBy(List<Music> music)
        {
            Console.WriteLine("How would you like to choose your music?\n\t1.View a full list of music" +
                "\n\t2.Search by title\n\t3.Search by Artist\n");


            string input = Console.ReadLine();
            if (input == "1")
            {
                return music;
            }
            else if (input == "2")
            {
                List<Music> musicOptions = Music.FilterMusicByTitle(music);
                return musicOptions;
            }
            else if(input == "3")
            {
                List<Music> musicOptions = Music.FilterMusicByArtist(music);
                return musicOptions;
            }
            else
            {
                Console.WriteLine("That isn't an option.\n");
                return SearchMusicBy(music);
            }
        }
        public static List<Movies> SearchMoviesBy(List<Movies> movies)
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
                List<Movies> movieOptions = Movies.FilterMoviesByTitle(movies);
                return movieOptions;
            }
            else if(input == "3")
            {
                List<Movies> movieOptions = Movies.FilterMoviesByDirector(movies);
                return movieOptions;
            }
            else
            {
                Console.WriteLine("That isn't an option.\n");
                return SearchMoviesBy(movies);
            }
        }
        #endregion
    }
}
