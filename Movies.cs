using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace MidtermNew
{
    public class Movies : LibraryItems
    {
        public string Director { get; set; }

        public Movies()
        {

        }
        public Movies(string barcode, string title, string checkedOut,
            string genre, string year, string dueDate, string director)
        : base(barcode, title, checkedOut, genre, year, dueDate)
        {
            Barcode = barcode;
            Title = title;
            CheckedOut = checkedOut;
            Genre = genre;
            Year = year;
            DueDate = dueDate;
            Director = director;
        }
        public static List<Movies> FilterMoviesByDirector(List<Movies> movieList)
        {
            Console.WriteLine("What director would you like to search for?");
            string input = Console.ReadLine().ToLower();
            List<Movies> movieOptions = new List<Movies>();
            if (Regex.IsMatch(input, @"^[a-zA-Z. ]+$"))
            {
                foreach (Movies movies in movieList)
                {
                    if (movies.Director.ToLower().Contains(input))
                    {
                        movieOptions.Add(movies);
                    }
                }
                return movieOptions;
            }
            else
            {
                Console.WriteLine("That is not a valid director.\n");
                return FilterMoviesByDirector(movieList);
            }
        }
        public static List<Movies> FilterMoviesByTitle(List<Movies> movieList)
        {
            Console.WriteLine("What title would you like to search for?");
            string input = Console.ReadLine().ToLower();
            List<Movies> movieOptions = new List<Movies>();
            if (Regex.IsMatch(input, @"^[a-zA-Z. '?!]+$"))
            {
                foreach (Movies movies in movieList)
                {
                    if (movies.Title.ToLower().Contains(input))
                    {
                        movieOptions.Add(movies);
                    }
                }
                return movieOptions;
            }
            else
            {
                Console.WriteLine("That is not a valid title.");
                return FilterMoviesByTitle(movieList);
            }
        }
        public static List<Movies> SearchMoviesBy(List<Movies> movies)
        {
            Console.WriteLine("How would you like to choose a movie?\n\t1.View a full list of movies\n\t2.Search by title" +
                "\n\t3.Search by director");
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
            else if (input == "3")
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
        public static void PrintMoviesList(List<Movies> movies)
        {
            int count = 0;
            foreach (Movies x in movies)
            {
                count++;
                Console.WriteLine($"\t{count}. {x.Title}");
            }
        }
        public static void ChooseMoviesItemReturn(List<Movies> movieOptions)
        {
            if (movieOptions.Count > 0)
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
                        Program.CheckAvailabilityReturn(movieOptions[index]);
                    }
                }
                else
                {
                    Console.WriteLine("Input invalid.\n");
                    ChooseMoviesItemReturn(movieOptions);
                }
            }
            else
            {
                Console.WriteLine("Sorry, there are no matching movies.");
            }
        }
        public static void ChooseMoviesItemCheckOut(List<Movies> movieOptions)
        {
            if (movieOptions.Count > 0)
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
                        Program.CheckAvailabilityCheckOut(movieOptions[index]);
                    }
                }
                else
                {
                    Console.WriteLine("Input invalid.\n");
                    ChooseMoviesItemCheckOut(movieOptions);
                }
            }
            else
            {
                Console.WriteLine("Sorry, there are no matching movies.");
            }
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
        public static void WriteFileNewMovies(List<Movies> addMovies)
        {
            StreamWriter writer = new StreamWriter("../../Movies.txt");

            foreach (Movies movies in addMovies)
            {
                File.AppendAllText("../../movies.txt", string.Format($"\n{movies.Barcode}|{movies.Title}|{movies.CheckedOut}|{movies.Genre}|" +
                    $"{movies.Year}|{movies.DueDate}|{movies.Director}"));
            }
            writer.Close();
        }
        public static void WriteFileUpdateMovies(List<Movies> movies)
        {
            StreamWriter writer = new StreamWriter("../../Movies.txt");

            foreach (Movies movie in movies)
            {
                writer.WriteLine(string.Format($"{movie.Barcode}|{movie.Title}|{movie.CheckedOut}|{movie.Genre}|" +
                    $"{movie.Year}|{movie.DueDate}|{movie.Director}"));
            }
            writer.Close();
        }
        public static void AddNewMovie(List<Movies> movies)
        {
            bool go = true;
            Movies newMovie = new Movies();

            while (go)
            {ear
                Console.WriteLine("What is the movies's title?");
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    newMovie.Title = input;
                    go = false;
                }
                else
                {
                    Console.WriteLine("That is not a valid title.\n");
                    go = true;
                }
            }
            go = true;
            while (go)
            {
                Console.WriteLine("Who is the director?");
                string input = Console.ReadLine();
                if (Regex.IsMatch(input, @"^[a-zA-Z .']+$"))
                {
                    newMovie.Director = input;
                    go = false;
                }
                else
                {
                    Console.WriteLine("That is not a valid author.\n");
                    go = true;
                }
            }
            go = true;
            while (go)
            {
                Console.WriteLine("What genre is this movie?");
                string input = Console.ReadLine();
                if (Regex.IsMatch(input, @"^[a-zA-Z /-]+$"))
                {
                    newMovie.Genre = input;
                    go = false;
                }
                else
                {
                    Console.WriteLine("That is not a valid year.\n");
                    go = true;
                }
            }
            go = true;
            Console.WriteLine("What year was it published?");
            string year = Console.ReadLine();
            while (go)
            {
                if (Regex.IsMatch(year, @"^[0-9]{4}$"))
                {
                    newMovie.Year = year;
                    go = false;
                }
                else
                {
                    Console.WriteLine("That is not a valid year.\n");
                    go = true;
                }
            }
           
            newMovie.CheckedOut = "On shelf";
            newMovie.DueDate = "Not checked out";
            string random = Program.RandomString(10);
            newMovie.Barcode = $"MT{random}";
            movies.Add(newMovie);
            movies.OrderBy(x => x.Title).ToList();
            Console.WriteLine($"{newMovie.Title} was successfully added.\n");

        }
    }
}

