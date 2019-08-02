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
        public Movies(string director, string barcode, string title, string checkOut,
            string genre, string year, string dueDate)
        : base(barcode, title, checkOut, genre, year, dueDate)
        {

        }
        public void FilterMoviesByDirector(List<Movies> movieList)
        {
            Console.WriteLine("What director would you like to search for?");
            string input = Console.ReadLine();
            if (Regex.IsMatch(input, @"^[a-zA-Z. ]+$"))
            {
                int counter = 0;
                foreach (Movies movies in movieList)
                {
                    if (movies.Director == input)
                    {
                        counter++;
                        Console.WriteLine($"{counter}. {movies.Director}");
                    }
                }
            }
            else
            {
                Console.WriteLine("That is not a valid director.\n");
                FilterMoviesByDirector(movieList);
            }
        }
        public static void FilterMoviesByTitle(List<Movies> movieList)
        {
            Console.WriteLine("What title would you like to search for?");
            string input = Console.ReadLine();
            if (Regex.IsMatch(input, @"^[a-zA-Z. ]+$"))
            {
                int counter = 0;
                foreach (Movies movies in movieList)
                {
                    counter++;
                    if (movies.Title.Contains(input))
                    {
                        counter++;
                        Console.WriteLine($"{counter}. {movies.Title}");
                    }
                }
            }
            else
            {
                Console.WriteLine("That is not a valid title.");
                FilterMoviesByTitle(movieList);
            }
        }

    }
}
