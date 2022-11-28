using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.View
{
    public class SearchPage
    {
        public int InitiateSearchPageView()
        {
            InputHelper.ClearConsole();
            "---------- search page ------------".PrintLine();

            "1. Search".PrintLine();
            "2. Back to HomePage menu".PrintLine();
            "".PrintLine();
            "Enter your choice".PrintLine();
            var userChoice = InputHelper.UserInputChoice(2);
            return userChoice;
        }

        public string SearchByName()
        {
            "Enter user name : ".Print();
            
            return InputHelper.GetText();
        }

        public void UserNotFoundMessage()
        {
            "No such user found... ".PrintLine();
            "Enter any key to continue .. ".PrintLine();
            InputHelper.GetPositiveInt(); 
        }

    }

}






















//do
//{
//    "Enter a userName ".PrintLine();
//    searchedUserName = InputHelper.GetText();
//    if (userNames.Contains(searchedUserName))
//    {
//         isValid = true;
//    }
//    else
//    {
//        "No such user found".PrintLine();
//    }
//} while (!isValid);
//return searchedUserName;