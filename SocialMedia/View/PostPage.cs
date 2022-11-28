using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.View
{
    public class PostPage
    {
        public int InitiatePostPage()
        {
            InputHelper.ClearConsole();
            "-----------  Post Page ---------------".PrintLine();

            "1. Poll Post".PrintLine();
            "2. Text Post".PrintLine();
            "3. Back to Home page menu".PrintLine();

            var userChoice = InputHelper.UserInputChoice(3);
            return userChoice;
        }
    }
}
