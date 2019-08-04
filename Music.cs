using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace MidtermNew
{
    public class Music : LibraryItems
    {
        public string Artist { get; set; }

        public Music()
        {

        }
        public Music(string barcode, string title, string checkedOut, string genre, string year, string dueDate, string artist)
        : base(barcode, title, checkedOut, genre, year, dueDate)
        {
            Barcode = barcode;
            Title = title;
            CheckedOut = checkedOut;
            Genre = genre;
            Year = year;
            DueDate = dueDate;
            Artist = artist;
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
            if (Regex.IsMatch(input, @"^[a-zA-Z. ']+$"))
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
            else if (input == "3")
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
        public static void PrintMusicList(List<Music> music)
        {
            int count = 0;
            foreach (Music x in music)
            {
                count++;
                Console.WriteLine($"\t{count}. {x.Title}");
            }
        }
        public static void ChooseMusicItemReturn(List<Music> musicOptions)
        {
            if (musicOptions.Count > 0)
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
                        Program.CheckAvailabilityReturn(musicOptions[index]);
                    }
                }
                else
                {
                    Console.WriteLine("Input invalid.\n");
                    ChooseMusicItemReturn(musicOptions);
                }
            }
            else
            {
                Console.WriteLine("Sorry, there are no matching albums.\n");
            }
        }
        public static void ChooseMusicItemCheckOut(List<Music> musicOptions)
        {
            if (musicOptions.Count > 0)
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
                        Program.CheckAvailabilityCheckOut(musicOptions[index]);
                    }
                }
                else
                {
                    Console.WriteLine("Input invalid.\n");
                    ChooseMusicItemCheckOut(musicOptions);
                }
            }
            else
            {
                Console.WriteLine("Sorry, there are no matching albums.\n");
            }
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
    }
}
