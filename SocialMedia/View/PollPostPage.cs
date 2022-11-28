using SocialMedia.Model.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.View
{
    public class PollPostPage
    {
        public int InitiatePollPostPage()
        {
            InputHelper.ClearConsole();
            " --------- Poll Post -----------".PrintLine();
            "".PrintLine();
            "1. Add Post".PrintLine();
            "2. Remove Post".PrintLine();
            "3. Edit post".PrintLine();
            "4. Exit to post menu".PrintLine();

            return InputHelper.UserInputChoice(4);
        }


        public (string, string, DateTime, List<String>) CreatePostView()
        {
            "Creating a poll post".PrintLine();
            "".PrintLine();
            "Enter title for your post : ".Print();
            var title = InputHelper.GetText();
            "Enter your question : ".Print();
            var question = InputHelper.GetText();
            "Enter the number of options for your poll : ".PrintLine();
            var pollOptionCount = InputHelper.GetPositiveInt();
            "Enter your poll choices : ".PrintLine();
            var options = new List<string>();
            for(int i = 0; i < pollOptionCount; i++)
            {
                var option = InputHelper.GetText();
                options.Add(option);
            }
            var createdAt = DateTime.Now;

            return (title, question, createdAt, options);

        }

        public PollPostBobj GetUserSelectedPostToEdit(List<PollPostBobj> pollPosts)
        {
            "Editing a poll post".PrintLine();
            "Select a post by its Id to edit".PrintLine();
            "index  post Id --> post title".PrintLine();
            var index = 1;
            foreach(var post in pollPosts)
            {
                $"{index}. {post.Id} --> {post.Title}".PrintLine();
                index += 1;
            }
            var userChoice = InputHelper.UserInputChoice(pollPosts.Count);

            return pollPosts[userChoice - 1];
        }

        public void NoPostsAvailableToEditOrDeleteMessage()
        {
            "Create a post to edit/delete it ".PrintLine();
            "press any number to get back to post menu".PrintLine();
            var userChoice = InputHelper.GetPositiveInt();
        }

        public int GetUserChoiceToEdit()
        {
            "Select an option to edit the post".PrintLine();
            "1. Question".PrintLine();
            "2. Title".PrintLine();
            "3. Back to poll post Menu".PrintLine();
            //"4. Post Choice".PrintLine();
            var userChoice = InputHelper.UserInputChoice(3);

            return userChoice;
        }

        public (string,DateTime) EditPostQuestion(PollPostBobj pollPostBobj)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            $"Original Question => {pollPostBobj.Question}".PrintLine();
            InputHelper.ResetConsoleColor();

            "Edit the Question".PrintLine();
            var newQuestion = InputHelper.GetText();
            var editedTime = DateTime.Now;
           
            return (newQuestion, editedTime);
        }


        public (string, DateTime) EditPostTitle(PollPostBobj pollPostBobj)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            $"Original Title => {pollPostBobj.Title}".PrintLine();
            InputHelper.ResetConsoleColor();

            "Edit the title".PrintLine();
            var newTitle = InputHelper.GetText();
            var editedTime = DateTime.Now;

            return (newTitle, editedTime);
        }

        public void EditPostChoice(List<String> choices)
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            $"Original Choice".PrintLine();
        }

        public PollPostBobj GetUserSelectedPostToRemove(List<PollPostBobj> pollPosts)
        {
            "Remove a poll post ! ".PrintLine();
            "Select a post by its Id to remove".PrintLine();
            "index  post Id --> post title".PrintLine();
            var index = 1;
            foreach (var post in pollPosts)
            {
                $"{index}. {post.Id} --> {post.Title}".PrintLine();
                index += 1;
            }
            var userChoice = InputHelper.UserInputChoice(pollPosts.Count);

            return pollPosts[userChoice  - 1];
        }

        public void SuccessfullyWorkDoneMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            $"Poll post is successfully { message } ".PrintLine();
            InputHelper.ResetConsoleColor();
            "".PrintLine();
            "press any number to continue".PrintLine();
            var exitKey = InputHelper.GetPositiveInt();
            
        }
    }
}
