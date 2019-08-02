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
            if (Regex.IsMatch(input, @"^[a-zA-Z. ]+$"))
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

    }
}
