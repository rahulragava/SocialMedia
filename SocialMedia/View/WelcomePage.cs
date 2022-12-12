
namespace SocialMedia.View
{
    public class WelcomePage
    {
        public void InitialPage()
        {
            InputHelper.ClearConsole();
            Console.ForegroundColor = ConsoleColor.Green;
            "".PrintLine();
            "                  *********  Welcome to Z-media  *********".PrintLine();
            InputHelper.ResetConsoleColor();
            "1. Sign in".PrintLine();
            "2. Close the application ".PrintLine();
            "press(1/2)".PrintLine();
            
        }
    }
}
