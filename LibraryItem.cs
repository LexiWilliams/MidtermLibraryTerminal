using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermNew
{
    public class LibraryItem
    {
        public string Barcode {get; set;}
        public string Title { get; set; }
        public string CheckedOut { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
        public string DueDate { get; set; }

        public LibraryItem()
        {

        }
        public LibraryItem(string barcode, string title, string checkedOut, string genre, string year, string dueDate)
        {
            Barcode = barcode;
            Title = title;
            CheckedOut = checkedOut;
            Genre = genre;
            Year = year;
            DueDate = dueDate;
        }
        public void CheckInAndOut()
        {
            if (CheckedOut == "On Shelf")
            {
                CheckedOut = "Checked Out";
            }
            else
            {
                CheckedOut = "On Shelf";
            }
        }
    }
}
