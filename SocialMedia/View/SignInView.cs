using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.View
{
    public class SignInView
    {
        public (string, string) SignInPage()
        {
            "Enter your (User Name/Mail Id/Phone Number) : ".Print();
            var UserIdentityValue = InputHelper.GetText();
            "Enter your Password : ".Print();
            var Password = InputHelper.GetText();
            "".PrintLine(); 

            return (UserIdentityValue, Password);
        }
       
        public (string?, string?, bool) InvalidUser()
        {
            "User Identification is wrong, try again !".PrintLine();
            bool GoBack = false;
            "press -1 to go back or press any other number to log in ".PrintLine();
            var goBackChoice = InputHelper.GetInt();
            if(goBackChoice != -1)
            {
                "Enter userName : ".Print();
                var userIdentifyingValue = InputHelper.GetText();
                "Enter userPassword : ".Print();
                var userPassword = InputHelper.GetText();

                return (userIdentifyingValue, userPassword, GoBack);

            }
            return (null, null, !GoBack);

        }

        public void SuccessLoggedInMessage(string userName)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"successfully logged in as { userName }");
            InputHelper.ResetConsoleColor();
            "press any number to go to HomePage : ".Print();
            InputHelper.GetPositiveInt();
        }
    }
}
