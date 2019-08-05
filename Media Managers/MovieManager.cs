using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermNew
{
    class MovieManager
    {
        private const string MOVIE_FILE_PATH = "../../Movies.txt";
        public static List<Movie> FilterByDirector(List<Movie> movieList)
        {
            Console.WriteLine("What director would you like to search for?");
            string input = Console.ReadLine().ToLower();
            List<Movie> movieOptions = new List<Movie>();
            foreach (Movie movies in movieList)
            {
                if (movies.Director.ToLower().Contains(input))
                {
                    movieOptions.Add(movies);
                }
            }
            return movieOptions;
        }

        public static List<Movie> FilterByTitle(List<Movie> movieList)
        {
            Console.WriteLine("What title would you like to search for?");
            string input = Console.ReadLine().ToLower();
            List<Movie> movieOptions = new List<Movie>();

            foreach (Movie movies in movieList)
            {
                if (movies.Title.ToLower().Contains(input))
                {
                    movieOptions.Add(movies);
                }
            }
            return movieOptions;

        }
        public static void ReadFile(List<Movie> movies)
        {


            using (StreamReader reader = new StreamReader(MOVIE_FILE_PATH))
            {
                string line = reader.ReadLine();
                //read each line, split the line into the respective values and assign the properties to the new movie object
                while (line != null)
                {
                    //create a new movie per line
                    Movie newMovie = new Movie();
                    newMovie.Deserialize(line);
                    movies.Add(newMovie);
                    //move on to the next line
                    line = reader.ReadLine();
                }
            }
        }
        internal static void WriteFile(List<Movie> movies)
        {
            //using statement automatically closes the connection to the txt file at the end of the scope
            //open streamwriter, point it to the movie file path, and overwrite what's there
            using (StreamWriter writer = new StreamWriter(MOVIE_FILE_PATH, false))
            {
                //loop over movies
                foreach (Movie movie in movies)
                {
                    //write each line (movie) to the txt file
                    writer.WriteLine(movie.Serialize());
                }
            };
        }
        public static List<Movie> MovieOptions(List<Movie> movie)
        {
            List<Movie> returnList = new List<Movie>();
            Console.WriteLine("How would you like to choose your movie?\n\t1.View a full list of movies\n\t2.Search by title\n\t3.Search by Director\n");
            bool isValid = false;
            while (!isValid)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int userChoice))
                {
                    if (userChoice == 1)
                    {
                        returnList = movie;
                    }
                    else if (userChoice == 2)
                    {
                        returnList = FilterByTitle(movie);
                    }
                    else if (userChoice == 3)
                    {
                        returnList = FilterByDirector(movie);
                    }
                    isValid = returnList.Count > 0;
                    if (!isValid)
                    {
                        Console.WriteLine("No movies found matching search criteria.\nHow would you like to choose your movie?\n\t1.View a full list of movies\n\t2.Search by title\n\t3.Search by Director\n");
                    }
                }
                else
                {
                    Console.WriteLine("Not a valid option. Please use a number between 1 and 3.");
                    isValid = false;
                }
            }
            return returnList;
        }
        internal static void PrintItemList(List<Movie> movieOptions)
        {
            int counter = 0;
            foreach (Movie movie in movieOptions)
            {
                counter++;
                Console.WriteLine($"{counter}. {movie.Title}");
            }
        }
    }
}
