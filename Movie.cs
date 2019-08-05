using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MidtermNew
{
    public class Movie : LibraryItem
    {
        public string Director { get; set; }

        public Movie()
        {

        }
        public Movie(string barcode, string title, string checkedOut,
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
        public string Serialize()
        {
            return $"{Barcode}|{Title}|{CheckedOut}|{Genre}|{Year}|{DueDate}|{Director}";
        }
        public void Deserialize(string input)
        {
            string[] properties = input.Split('|');
            Barcode = properties[0];
            Title = properties[1];
            CheckedOut = properties[2];
            Genre = properties[3];
            Year = properties[4];
            DueDate = properties[5];
            Director = properties[6];
        }
        public string MovieDetails()
        {
            return $"\tTitle: {Title}\n\tDirector: {Director}\n\tGenre: {Genre}\n\tYear Published: {Year}\n\tBarcode: {Barcode}\n\t \n\tStatus:{CheckedOut}\n";
        }
    }
}
