
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

        public string SearchByName(List<string> userNames)
        {
            "Enter user name : ".Print();
            var userName = InputHelper.GetText().ToLower();

            List<string> containedUserNames = userNames.Select(u => u.ToLower()).Where(u => u.Contains(userName)).ToList();
            int index = 1;
            foreach (var containedUserName in containedUserNames)
            {
                //$"{index}. {containedUserName}".PrintLine();
                $"{index}. {userNames.First(u => string.Equals(u, containedUserName, StringComparison.OrdinalIgnoreCase))}".PrintLine();
                index++;
            }
            int userChoice;
            if (containedUserNames.Any())
            {
                "Enter the index for select the user".PrintLine();
                userChoice = InputHelper.UserInputChoice(containedUserNames.Count);

                return userNames.First(u => string.Equals(u, containedUserNames[userChoice-1], StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                UserNotFoundMessage();

                return SearchByName(userNames);
            }
        }

        public void UserNotFoundMessage()
        {
            "No such user found... ".PrintLine();
            "Enter any key to continue .. ".PrintLine();
            InputHelper.GetPositiveInt(); 
        }
    }
}






















