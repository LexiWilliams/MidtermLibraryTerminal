using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace MidtermNew
{
    public class Book : LibraryItem
    {

        public string Author { get; set; }
        public string Medium { get; set; }


        public Book()
        {

        }

        public Book(string barcode, string title, string checkedOut, string genre, string year, string dueDate, string author, string medium)
            : base(barcode, title, checkedOut, genre, year, dueDate)
        {
            Barcode = barcode;
            Title = title;
            CheckedOut = checkedOut;
            Genre = genre;
            Year = year;
            DueDate = dueDate;
            Author = author;
            Medium = medium;
        }

        public string Serialize()
        {
            return $"{Barcode}|{Title}|{CheckedOut}|{Genre}|{Year}|{DueDate}|{Author}|{Medium}";
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
            Author = properties[6];
            Medium = properties[7];
        }
        public string BookDetails()
        {
            return $"\tTitle: {Title}\n\tAuthor: {Author}\n\tGenre: {Genre}\n\tYear Published: {Year}\n\tMedium: {Medium}" +
                            $"\n\tBarcode: {Barcode}\n\t \n\tStatus:{CheckedOut}\n";
        }

    }
}
