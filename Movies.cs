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
        }

    }
}
