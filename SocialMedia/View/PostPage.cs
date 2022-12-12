
namespace SocialMedia.View
{
    public class PostPage
    {
        public int InitiatePostPage()
        {
            InputHelper.ClearConsole();
            "-----------  Post Page ---------------".PrintLine();

            "1. Post operation".PrintLine();
            "2. Label".PrintLine();
            "3. Back to Home page menu".PrintLine();

            var userChoice = InputHelper.UserInputChoice(4);
            return userChoice;
        }
    }
}
