using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MidtermNew
{
    public class Music : LibraryItems
    {
        public string Artist { get; set; }

        public Music()
        {

        }
        public Music(string barcode, string title, string checkOut, string genre, string year, string dueDate, string artist)
        : base(barcode, title, checkOut, genre, year, dueDate)
        {

        }
        public void DisplayMusicMenu()
        {

        }
        public static List<Music> FilterMusicByArtist(List<Music> musicList)
        {
            Console.WriteLine("What artist would you like to search for?");
            string input = Console.ReadLine().ToLower();
            List<Music> musicOptions = new List<Music>();
            if (Regex.IsMatch(input, @"^[a-zA-Z. ]+$"))
            {
                foreach (Music music in musicList)
                {
                    if (music.Artist.ToLower().Contains(input))
                    {
                        musicOptions.Add(music);
                    }
                }
                return musicOptions;
            }
            else
            {
                Console.WriteLine("That is not a valid artist.\n");
                return FilterMusicByArtist(musicList);
            }

        }
        public static List<Music> FilterMusicByTitle(List<Music> musicList)
        {
            Console.WriteLine("What title would you like to search for?");
            string input = Console.ReadLine().ToLower();
            List<Music> musicOptions = new List<Music>();
            if (Regex.IsMatch(input, @"^[a-zA-Z. ]+$"))
            {
                foreach (Music music in musicList)
                {
                    if (music.Title.ToLower().Contains(input))
                    {
                        musicOptions.Add(music);
                    }
                }
                return musicOptions;
            }
            else
            {
                Console.WriteLine("That is not a valid title.");
               return FilterMusicByTitle(musicList);
            }


        }
    }
}
