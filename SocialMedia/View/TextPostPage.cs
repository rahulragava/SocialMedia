using SocialMedia.Model.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.View
{
    public class TextPostPage
    {
        public int InitiateTextPostPage()
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

        public (string,string, DateTime) CreatePostView()
        {
            "Creating a text post".PrintLine();
            "".PrintLine();
            "Enter title for your post".Print();
            var title = InputHelper.GetText();
            "Enter your thoughts ... ".Print();
            var content = InputHelper.GetText();
            var createdAt = DateTime.Now;

            return (title, content, createdAt);
            
        }

        public TextPostBObj GetUserSelectedPostToEdit(List<TextPostBObj> textPosts)
        {
            "Editing a text post".PrintLine();
            "Select a post by its Id to edit".PrintLine();
            "index  post Id --> post title".PrintLine();
            var index = 1;
            foreach (var post in textPosts)
            {
                $"{index}. {post.Id} --> {post.Title}".PrintLine();
                index += 1;
            }
            var userChoice = InputHelper.UserInputChoice(textPosts.Count);

            return textPosts[userChoice - 1];
        }

        public int GetUserChoiceToEdit()
        {
            "Select an option to edit the post".PrintLine();
            "1. Title".PrintLine();
            "2. Content".PrintLine();
            "3. Back to text post Menu".PrintLine();
            var userChoice = InputHelper.UserInputChoice(5);

            return userChoice;
        }

        public (string, DateTime) EditPostTitle(TextPostBObj textPostBobj)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            $"Original Title => {textPostBobj.Title}".PrintLine();
            InputHelper.ResetConsoleColor();

            "Edit the title".PrintLine();
            var newTitle = InputHelper.GetText();
            var editedTime = DateTime.Now;

            return (newTitle, editedTime);
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

        public void NoPostsAvailableToEditMessage()
        {
            "Create a post to edit it ".PrintLine();
            "press any number to go back".PrintLine();
            var userChoice = InputHelper.GetPositiveInt();
        }

        

        public TextPostBObj GetUserSelectedPostToRemove(List<TextPostBObj> textPosts)
        {
            "Remove a poll post ! ".PrintLine();
            "Select a post by its Id to remove".PrintLine();
            "index  post Id --> post title".PrintLine();
            var index = 1;
            foreach (var post in textPosts)
            {
                $"{index}. {post.Id} --> {post.Title}".PrintLine();
                index += 1;
            }
            var userChoice = InputHelper.UserInputChoice(textPosts.Count);

            return textPosts[userChoice - 1];
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

    }
}
