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
                index = GetPositiveInt();
                if (!(index <= endIndex)) throw new Exception();
            }
            catch (Exception)
            {
                "please enter correct Value in range  ".PrintLine();
                return UserInputChoice(endIndex);
            }

            return index ;
        }

        public static char UserInputChoice(int startIndex, int endIndex)
        {
            char index;
            "Enter corresponding index ".PrintLine();

            try
            {
                index = GetChar();
                var indexNumber = (int)index;
                if (!(index < endIndex && index >= startIndex)) throw new Exception();

            }
            catch (Exception)
            {
                "please enter correct value in range ".PrintLine();
                return UserInputChoice(startIndex,endIndex);
            }
            return index;
        }

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

