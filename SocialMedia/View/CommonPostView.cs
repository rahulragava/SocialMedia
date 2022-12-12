using SocialMedia.Model.BusinessModel;


namespace SocialMedia.View
{
    public class CommonPostView
    {
        public int InitiatePostPage()
        {
            InputHelper.ClearConsole();
            " -----------  Text Posts -----------".PrintLine();
            "".PrintLine();
            "1. AddPost".PrintLine();
            "2. RemovePost".PrintLine();
            "3. EditPost".PrintLine();
            "4. Exit to Post menu".PrintLine();
            "".PrintLine();
            "Enter your choice".PrintLine();

            return InputHelper.UserInputChoice(4);
        }

        public int SelectPostTypeView(string message)
        {
            InputHelper.ClearConsole();
            "----------Post types---------------".PrintLine();
            "1. Text Post".PrintLine();
            "2. Poll Post".PrintLine();
            "3. Back".PrintLine();
            "".PrintLine();
            $"Select a type of post to { message } the post : ".PrintLine();

            return InputHelper.UserInputChoice(3);
        }

        public string GetPostTitle()
        {
            "Enter the title for the post ...".PrintLine();
            return InputHelper.GetText();
        }

        public PostBObj GetUserSelectedPostToRemove(IEnumerable<PostBObj> posts)
        {
            "Remove a poll post ! ".PrintLine();
            "Select a post by its Id to remove".PrintLine();
            "(Index) -------->  post title".PrintLine();
            var index = 1;
            foreach (var post in posts)
            {
                $"({index})     -------->  {post.Title}".PrintLine();
                index += 1;
            }
            var postList = posts.ToList();
            var userChoice = InputHelper.UserInputChoice(postList.Count);

            return postList[userChoice - 1];
        }

        public void NoPostsAvailableToEditOrDeleteMessage()
        {
            "Create a post to edit/delete it ".PrintLine();
            "press any number to get back to post menu".PrintLine();
            var userChoice = InputHelper.GetPositiveInt();
        }

        public void SuccessfullyWorkDoneMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            $"Poll post is successfully {message} ".PrintLine();
            InputHelper.ResetConsoleColor();
            "".PrintLine();
            "press any number to continue".PrintLine();
            var exitKey = InputHelper.GetPositiveInt();

        }

        public string GetPostContent()
        {
            "Enter your thoughts ... ".PrintLine();
            return InputHelper.GetText();
        }

        public (string, List<String>) GetPostQuestionAndChoices()
        {
            "Enter your question : ".Print();
            var question = InputHelper.GetText();
            "Enter the number of options for your poll : ".PrintLine();
            var pollOptionCount = InputHelper.GetPositiveInt();
            "Enter your poll choices : ".PrintLine();
            var options = new List<string>();
            for (int i = 0; i < pollOptionCount; i++)
            {
                var option = InputHelper.GetText();
                options.Add(option);
            }

            return (question, options);
        }

       
        public PostBObj GetUserSelectedPostToEdit(IEnumerable<PostBObj> posts)
        {
            "Editing a post".PrintLine();
            "Select a post by its Id to edit".PrintLine();
            "(Index) -------->  post title".PrintLine();
            var index = 1;
            foreach (var post in posts)
            {
                $"({index})     -------->  {post.Title}".PrintLine();
                index += 1;
            }
            var postList = posts.ToList();
            var userChoice = InputHelper.UserInputChoice(postList.Count);

            return postList[userChoice - 1];
        }

        public int GetUserChoiceToEditTextPost()
        {
            "Select an option to edit the post".PrintLine();
            "1. Title".PrintLine();
            "2. Content/Question".PrintLine();
            "3. Back to post Menu".PrintLine();
            var userChoice = InputHelper.UserInputChoice(5);

            return userChoice;
            
        }
        public int GetUserChoiceToEditPollPost()
        {
            "Select an option to edit the post".PrintLine();
            "1. Title".PrintLine();
            "2. Question".PrintLine();
            "3. Back to post Menu".PrintLine();
            var userChoice = InputHelper.UserInputChoice(5);

            return userChoice;

        }
        public (string, DateTime) EditPostTitle(PostBObj postBObj)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            $"Original Title => {postBObj.Title}".PrintLine();
            InputHelper.ResetConsoleColor();

            "Edit the title".PrintLine();
            var newTitle = InputHelper.GetText();
            var editedTime = DateTime.Now;

            return (newTitle, editedTime);
        }

        public (string, DateTime) EditPostQuestion(PollPostBObj pollPostBobj)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            $"Original Question => {pollPostBobj.Question}".PrintLine();
            InputHelper.ResetConsoleColor();

            "Edit the Question".PrintLine();
            var newQuestion = InputHelper.GetText();
            var editedTime = DateTime.Now;

            return (newQuestion, editedTime);
        }
        public (string, DateTime) EditPostContent(TextPostBObj textPostBobj)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            $"Original Content => {textPostBobj.Content}".PrintLine();
            InputHelper.ResetConsoleColor();

            "Edit the content".PrintLine();
            var newContent = InputHelper.GetText();
            var editedTime = DateTime.Now;

            return (newContent, editedTime);
        }
    }

}
