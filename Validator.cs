using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MidtermNew
{
    class Validator
    {
        public static bool IsInt(string input)
        {
            return int.TryParse(input, out int variable);
        }

        public static bool IsYesNo(string input)
        {
            if (input == "yes" || input == "y" || input == "no" || input == "n")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsInRange(int input, int min, int max)
        {
            if (input >= min && input <= max)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsDateTime(string userInput)
        {
            bool isValid = false;
            string month = "";
            string day = "";
            string year = "";

            if (Regex.IsMatch(userInput, @"^(0?[1-9]|1[0-2])[\/](0?[1-9]|[12]\d|3[01])[\/](19|20)\d{2}$"))
            {
                //try to split into MM DD and YYYY
                string[] dateArray = userInput.Split('/');
                //validate that each string that comprises the array is an int
                if (dateArray.Length == 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (Validator.IsInt(dateArray[i]))
                        {
                            isValid = true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    if (isValid)
                    {
                        month = dateArray[0];
                        day = dateArray[1];
                        year = dateArray[2];
                    }
                    if (Validator.IsInt(day) && Validator.IsInt(month) && Validator.IsInt(year))
                    {
                        if (day.Length == 2 && month.Length == 2 && year.Length == 4)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}

