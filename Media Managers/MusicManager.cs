using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermNew
{
    class MusicManager
    {
        private const string MUSIC_FILE_PATH = "../../Music.txt";
        public static List<Music> FilterByArtist(List<Music> musicList)
        {
            //prompt the user for a title they want to search
            Console.WriteLine("What artist would you like to search for?");

            //set the input to lower for easier comparison
            string input = Console.ReadLine().ToLower();

            //create, populate, and return a new list to store the filtered music in
            List<Music> filteredMusic = new List<Music>();
            foreach (Music music in musicList)
            {
                if (music.Artist.ToLower().Contains(input))
                {
                    filteredMusic.Add(music);
                }
            }
            return filteredMusic;
        }
        public static List<Music> FilterByTitle(List<Music> musicList)
        {
            //prompt the user for a title they want to search
            Console.WriteLine("What title would you like to search for?");

            //set the input to lower for easier comparison
            string input = Console.ReadLine().ToLower();

            //create and return a new list to store the filtered music in
            List<Music> filteredMusic = new List<Music>();
            foreach (Music music in musicList)
            {
                if (music.Title.ToLower().Contains(input))
                {
                    filteredMusic.Add(music);
                }
            }
            return filteredMusic;
        }

        public static void ReadFile(List<Music> music)
        {
            using (StreamReader reader = new StreamReader(MUSIC_FILE_PATH))
            {
                string line = reader.ReadLine();
                //read each line, split the line into the respective values and assign the properties to the new music object
                while (line != null)
                {
                    //create a new music per line
                    Music newMusic = new Music();
                    newMusic.Deserialize(line);
                    music.Add(newMusic);
                    //move on to the next line
                    line = reader.ReadLine();
                }
            }
        }
        internal static void WriteFile(List<Music> musicList)
        {
            //using statement automatically closes the connection to the txt file at the end of the scope
            //open streamwriter, point it to the music file path, and overwrite what's there
            using (StreamWriter writer = new StreamWriter(MUSIC_FILE_PATH, false))
            {
                //loop over music
                foreach (Music music in musicList)
                {
                    //write each line (music) to the txt file
                    writer.WriteLine(music.Serialize());
                }
            };
        }
        public static List<Music> MusicOptions(List<Music> music)
        {
            List<Music> returnList = new List<Music>();
            Console.WriteLine("How would you like to choose your music?\n\t1.View a full list of music\n\t2.Search by title\n\t3.Search by Artist\n");
            bool isValid = false;
            while (!isValid)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int userChoice))
                {
                    if (userChoice == 1)
                    {
                        returnList = music;
                    }
                    else if (userChoice == 2)
                    {
                        returnList = FilterByTitle(music);
                    }
                    else if (userChoice == 3)
                    {
                        returnList = FilterByArtist(music);
                    }
                    isValid = returnList.Count > 0;
                    if (!isValid)
                    {
                        Console.WriteLine("No music found matching search criteria.\nHow would you like to choose your music?\n\t1.View a full list of music\n\t2.Search by title\n\t3.Search by Artist\n");
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
        internal static void PrintItemList(List<Music> musicOptions)
        {
            int counter = 0;
            foreach (Music music in musicOptions)
            {
                counter++;
                Console.WriteLine($"{counter}. {music.Title}");
            }
        }
    }
}
