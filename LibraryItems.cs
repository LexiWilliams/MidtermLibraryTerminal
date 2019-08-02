using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermNew
{
    public abstract class LibraryItems
    {
        public string Barcode { get; set; }
        public string Title { get; set; }
        public string CheckedOut { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
        public string DueDate { get; set; }


        public LibraryItems()
        {

        }
        public LibraryItems(string barcode, string title, string checkOut, string genre, string year, string dueDate)
        {
            Barcode = barcode;
            Title = title;
            CheckedOut = checkOut;
            Genre = genre;
            Year = year;
            DueDate = dueDate;

        }
        public void CheckInAndOut(LibraryItems item)
        {

            if (item.CheckedOut == "yes")
            {
                Console.WriteLine("Checked out.");
            }
            else
            {
                Console.WriteLine("Sorry, I did not recognize that input. Please try again");
            }
        }

    }
}
