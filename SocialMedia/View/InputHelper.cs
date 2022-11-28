using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SocialMedia.View
{
    public class InputHelper
    {
        public static int UserInputChoice(int endIndex)
        {
            int index;
            "Enter  Corresponding  Index : ".Print();

            try
            {
                index = GetInt();
                if (!(index <= endIndex && index > 0)) throw new Exception();
            }
            catch (Exception)
            {
                "please enter correct Value in range  ".PrintLine();
                return UserInputChoice(endIndex);
            }

            return index ;
        }

        public static DateTime GetUserDateTimeInformation()
        {
            DateTime date;
            "".PrintLine();
            "Enter Date (YYYY/MM/dd): ".Print();
            "".PrintLine();
            try
            {
                var input = Console.ReadLine() ?? "";
                date = DateTime.Parse(input.Trim());
            }
            catch (Exception)
            {
                "Please Enter Date In Specified Format  (dd/MM/YYYY)".PrintLine();
                return GetUserDateTimeInformation();
            }

            return date;
        }

        //public static IEnumerable<int> GetCorrespondingUserIndexes(int endIndex)
        //{
        //    "".PrintLine();
        //    var userInput = "";
        //    "Enter Users Index in Corresponding Format :".PrintLine();
        //    " num/num/num/".PrintLine();
        //    var userInputIndexes = new List<int>();
        //    try
        //    {
        //        userInput = Console.ReadLine() ?? "";
        //        var indexes = userInput.Split('/', StringSplitOptions.TrimEntries);
        //        foreach (var index in indexes)
        //        {
        //            var num = Convert.ToInt32(index);
        //            if (num > endIndex || num <= 0)
        //            {
        //                throw new FormatException();
        //            }
        //            userInputIndexes.Add(num - 1);
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        "".PrintLine();
        //        "Please Enter values Within the range and in Correct Format ".PrintLine();
        //        return GetCorrespondingUserIndexes(endIndex);

        //    }

        //    return userInputIndexes;

        //}

        public static string GetText()
        {
            try
            {
                var word = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(word) is not true)
                {
                    return word;
                }

                throw new FormatException();
            }
            catch (Exception)
            {
                ErrorConsoleColor();
                "Text Should  not be empty".PrintLine();
                ResetConsoleColor();
                return GetText();

            }
        }
        

        public static int GetInt()
        {
            string numberAsString;
            int validNumber = 0;
            bool isValidNumber = false;
            numberAsString = GetText();
            isValidNumber = int.TryParse(numberAsString, out validNumber);

            if (isValidNumber) return validNumber;
            else
            {
                ErrorConsoleColor();
                "Enter a valid Number !".PrintLine();
                ResetConsoleColor();
                return GetInt();
            }
        }

        public static int GetPositiveInt()
        {
            string numberAsString;
            int validNumber = 0;
            bool isValidNumber = false;
            numberAsString = GetText();
            isValidNumber = int.TryParse(numberAsString, out validNumber);

            if (isValidNumber && validNumber > 0) return validNumber;
            else
            {
                ErrorConsoleColor();
                "Enter a positive valid Number !".PrintLine();
                ResetConsoleColor();
                return GetPositiveInt();
            }
        }

        public static Char GetChar()
        {
            char ValidChar;
            bool isValidChar = false;
            string CharAsString = GetText();
            isValidChar = char.TryParse(CharAsString, out ValidChar);
            if(isValidChar) return ValidChar;
            else
            {
                ErrorConsoleColor();
                "Enter a valid string literal!".PrintLine();
                ResetConsoleColor();
                return GetChar();
            }

        }
        public static Double GetDouble()
        {
            string numberAsString;
            double validNumber = 0;
            bool isValidNumber = false;
            numberAsString = GetText();
            isValidNumber = double.TryParse(numberAsString, out validNumber);

            if (isValidNumber) return validNumber;
            else 
            {
                ErrorConsoleColor();
                "Enter a valid decimal Number !".PrintLine();
                ResetConsoleColor();
                return GetDouble();
            }
        }

        
        public static void ClearConsole()
        {
            Console.Clear();
        }

        public static void ResetConsoleColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ErrorConsoleColor()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }

        public static DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }


}

