using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MidtermNew
{
    public class Movies : LibraryItems
    {
        public string Director { get; set; }

        public Movies()
        {

        }
        public Movies(string barcode, string title, string checkOut,
            string genre, string year, string dueDate, string director)
        : base(barcode, title, checkOut, genre, year, dueDate)
        {

        }
        public static List<Movies> FilterMoviesByDirector(List<Movies> movieList)
        {
            Console.Clear();
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
            while (go)
            {
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
