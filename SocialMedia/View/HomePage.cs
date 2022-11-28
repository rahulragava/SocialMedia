using SocialMedia.Model.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.View
{
    public class HomePage
    {
        public int InitiateHomePage()
        {
            InputHelper.ClearConsole();
            "-------------  Home Page ----------------------".PrintLine();

            "1. Search".PrintLine();
            "2. Post".PrintLine();
            "3. sign out".PrintLine();
            "".PrintLine();
            "Enter your choice".PrintLine();
            var userChoice = InputHelper.UserInputChoice(3);
            return userChoice;
        }
    }
}
