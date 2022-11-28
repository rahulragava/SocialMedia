using SocialMedia.Controller;
using SocialMedia.Manager;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;
using SocialMedia.Model.EntityModel.EnumTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.View
{
    public class ProfilePage
    {
        public int InitiateProfilePage(UserBobj searchedUser)
        {
            InputHelper.ClearConsole();
            Console.ForegroundColor = ConsoleColor.Blue;
            " --------------------------            Profile Page                       --------------------------".PrintLine();
            "".PrintLine();
            "".PrintLine();
            $"      UserName               : { searchedUser.UserName }".PrintLine();
            $"      Full Name              : { searchedUser.FirstName }  { searchedUser.LastName } ".PrintLine();
            $"      Gender                 : { searchedUser.Gender }".PrintLine();
            $"      Marital statud         : { searchedUser.MaritalStatus }".PrintLine();
            $"      Education              : { searchedUser.Education }".PrintLine();
            $"      Occupation             : { searchedUser.Occupation }".PrintLine();
            $"      Place                  : { searchedUser.Place }".PrintLine();
            $"      Account created At     : { searchedUser.CreatedAt }".PrintLine();
            "".PrintLine();
            "".PrintLine();
            InputHelper.ResetConsoleColor();
            "1. View User Text Post".PrintLine();
            "2. View User Poll Post".PrintLine();
            "3. Exit to searchMenu".PrintLine();

            return InputHelper.UserInputChoice(3);
        }


        public TextPostBobj ViewUserTextPosts(List<TextPostBobj> textPosts)
        {
            InputHelper.ClearConsole();
            var indexCount = 1;
            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (var textPost in textPosts)
            {
                $"{indexCount}. ".PrintLine();
                $"   Title    : {textPost.Title}".PrintLine();
                $"   posted at    : {textPost.CreatedAt}".PrintLine();
                indexCount++;
            }
            InputHelper.ResetConsoleColor();
            "".PrintLine();
            var userChoice = InputHelper.UserInputChoice(textPosts.Count);

            return textPosts[userChoice - 1];
        }

        public (PollPostBobj,int) ViewUserPollPosts(List<PollPostBobj> pollPosts)
        {
            InputHelper.ClearConsole();
            var indexCount = 1;
            Console.ForegroundColor = ConsoleColor.Blue;
            foreach(var pollPost in pollPosts)
            {
                $"{indexCount}.  ".PrintLine();
                $"   Title    : {pollPost.Title}".PrintLine();
                $"   posted at    : {pollPost.CreatedAt}".PrintLine();

                indexCount++;
            }
            InputHelper.ResetConsoleColor();
            "".PrintLine();
            var userChoice = InputHelper.UserInputChoice(pollPosts.Count);

            return (pollPosts[userChoice - 1], userChoice-1);
        }


        public void ViewUserTextPost(TextPostBobj selectedTextPost)
        {
            InputHelper.ClearConsole();
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var emoteIcons = new List<string>() { "\x263A", ":(", "0_0", "o.0", "\x2665", "thumbs up", "thumbsdown", "\x2665 X" };

            if (selectedTextPost.Content != null)
            {
                "".PrintLine();
                "".PrintLine();
                "".PrintLine();
                $"                            {selectedTextPost.Title} ".PrintLine();
                "".PrintLine();
                "".PrintLine();
                "".PrintLine();
                
                SplitContentAndShow(selectedTextPost.Content);
                var reactions = selectedTextPost.Reactions;
                var reaction = 0;
                Console.ForegroundColor = ConsoleColor.DarkCyan;

                "+---------------------------------------------------------------------------------------------------".PrintLine();
                "".PrintLine();
                int count = 0;
                foreach (ReactionType reactionType in Enum.GetValues(typeof(ReactionType)))
                {
                    reaction = GetReactionEmoteIconCount(reactions, reactionType);
                    $"| {emoteIcons[count]} : {reaction} |  ".Print();
                    count++;
                }
                count = 0;
                "".PrintLine();
                "+--------------------------------------------------------------------------------------------------".PrintLine();
                InputHelper.ResetConsoleColor();
                "".PrintLine();
            }
        }  

        public int ViewUserPollPost(PollPostBobj selectedPollPost) 
        {
            InputHelper.ClearConsole();
            
            "".PrintLine();
            "".PrintLine();
            "".PrintLine();
            $"                                {selectedPollPost.Title}".PrintLine();
            "".PrintLine();
            "".PrintLine();
            "".PrintLine();
            SplitContentAndShow(selectedPollPost.Question);
            var index = 1;
            foreach(var option in selectedPollPost.choices)
            {
                $"      {index}. {option.Choice}".PrintLine();
                index++;
            }

            "".PrintLine();

            var userChoiceSelection = InputHelper.UserInputChoice(selectedPollPost.choices.Count);
            return userChoiceSelection;

        }
        private void SplitContentAndShow(string content)
        {
            var wordList = content.Split(" ");
            "                 \"".Print();
            if (wordList.Length > 10)
            {
                var subWord = string.Join(" ", wordList, 0, 10);
                Console.WriteLine($" { subWord }");
                var remainingWords = string.Join(" ", wordList, 10, wordList.Length - 10);
                SplitContentAndShow(remainingWords);
            }
            else
            {
                var subWord = string.Join(" ", wordList, 0, wordList.Count());
                Console.WriteLine($"{subWord}\"");
            }
        }

        public int GetReactionEmoteIconCount(List<Reaction> reactions, ReactionType reactionType)
        {
            var reactionCount = reactions.Where(reaction => reaction.reactionType == reactionType).Count();

            return reactionCount;
        }

        public int UserTextPostsOptions(TextPostBobj selectedTextPost)
        {
            "1. Comment".PrintLine();
            "2. View comment".PrintLine();
            "3. React".PrintLine();
            var userChoice = InputHelper.UserInputChoice(3);
            return userChoice;
        }


        public int GetUserChoiceOfReaction()
        {
            "1.Add reaction".PrintLine();
            "2. Remove reaction".PrintLine();
            "3. profile menu".PrintLine();
            var userChoice = InputHelper.UserInputChoice(3);

            return userChoice;
        }

        public ReactionType GetUserReaction()
        {
            foreach (ReactionType reactionType in Enum.GetValues(typeof(ReactionType)))
            {
                $"{(int)reactionType}. {reactionType}".PrintLine();
            }
            "select an reaction for the post/comment".PrintLine();
            var userChoice = InputHelper.UserInputChoice(Enum.GetNames(typeof(ReactionType)).Length);
            return (ReactionType)userChoice;

        }

        
        public void SuccessfullyWorkDoneMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            $" successfully {message} ".PrintLine();
            InputHelper.ResetConsoleColor();
            "".PrintLine();
            "press any number to continue".PrintLine();
            var exitKey = InputHelper.GetPositiveInt();

        }

        public void NeverReactedMessage()
        {
            "you have never reacted for this post".PrintLine();
        }

        public string GetUserComment()
        {
            "type your comment : ".PrintLine();
            var comment = InputHelper.GetText();
            return comment;
        }

        public void ViewPollResult(PollPostBobj pollPostBobj)
        {
            var emoteIcons = new List<string>() { "\x263A", ":(", "0_0", "o.0", "\x2665", "thumbs up", "thumbsdown", "\x2665 X" }; // emoji is not supproted in c# console
            Console.OutputEncoding = Encoding.UTF8;
        
            var reactions = pollPostBobj.Reactions;
            var reaction = 0;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            "".PrintLine();
            "+-----------------------------------------------------------------------------------------------------------".PrintLine();
            "".PrintLine();
            int count = 0;
            foreach (ReactionType reactionType in Enum.GetValues(typeof(ReactionType)))
            {
                reaction = GetReactionEmoteIconCount(reactions, reactionType);
                $"| {emoteIcons[count]} : {reaction} |  ".Print();
                count++;
            }
            count = 0;
            "".PrintLine();
            "+----------------------------------------------------------------------------------------------------------".PrintLine();
            InputHelper.ResetConsoleColor();

            "".PrintLine();
            "".PrintLine();
            "".PrintLine();
            "                      Result of the poll post".PrintLine();
            "".PrintLine();
            "".PrintLine();
            var totalVotes = 0;
            foreach (var choice in pollPostBobj.choices)
            {
                totalVotes += choice.choiceSelectedUsers.Count();
            }

            Console.ForegroundColor = ConsoleColor.Green;

            int totalStar = 10;
            foreach (var choice in pollPostBobj.choices)
            {
                "".PrintLine();
                var selectedPercent = (choice.choiceSelectedUsers.Count() / totalVotes) * 100;
                string voted = string.Concat(Enumerable.Repeat("0 ", (int)selectedPercent / 10));
                string unvoted = string.Concat(Enumerable.Repeat("X ", totalStar - (int)selectedPercent / 10));


                $"{choice.Choice}       ===> { voted }{ unvoted } | { selectedPercent } %".PrintLine();
            }
            "".PrintLine();
            InputHelper.ResetConsoleColor();
            $"total vote : {totalVotes}".PrintLine();
            //InputHelper.ResetConsoleColor();
        }

        public int GetUserChoice()
        {
            "".PrintLine();
            "1. comment to the post".PrintLine();
            "2. Reply to the comments in the Post".PrintLine();
            "3. React to the Post".PrintLine();
            "4. back to profile menu".PrintLine();

            return InputHelper.UserInputChoice(4);
        }

        public (int,List<int>) CommentView(PostBobj postBobj)
        {
            List<int> commentIds = new List<int>();
            Console.ForegroundColor = ConsoleColor.Cyan;
            
            "                COMMENT SECTION                ".PrintLine();
            "------------------------------------------------".PrintLine();

            foreach (var comment in postBobj.Comments)
            {
                $"  {new string(' ', comment.Depth)} {comment.Id}. {comment.Content}".PrintLine();
                $"  {new string(' ', comment.Depth + 5)}- ( {UserManager.GetUserManager().GetUserBobj(comment.CommentedBy).UserName}. date : {comment.CommentedAt} )".PrintLine();

                commentIds.Add(comment.Id);
            }

            "------------------------------------------------".PrintLine();
            "".PrintLine();
            InputHelper.ResetConsoleColor();
            "1. reply ".PrintLine();
            "2. react ".PrintLine();
            "3. exit".PrintLine();
            var userChoice = InputHelper.UserInputChoice(3);

            return (userChoice, commentIds);
        }

        public (string,int) ReplyView(List<int> commentIds)
        {
            "press comment id number to reply to that comment".PrintLine();

            var commentId = InputHelper.GetPositiveInt();
            while (!commentIds.Contains(commentId))
            {
                "no such comment existed, please type a valid commentId".PrintLine();
                commentId = InputHelper.GetPositiveInt();
            }
            "Enter your reply".PrintLine();
            var content = InputHelper.GetText();

            return (content, commentId);
        }

        public int GetCommentId(List<int> commentIds)
        {
            "press comment id number to reply to that comment".PrintLine();

            var commentId = InputHelper.GetPositiveInt();
            while (!commentIds.Contains(commentId))
            {
                "no such comment existed, please type a valid commentId".PrintLine();
                commentId = InputHelper.GetPositiveInt();
            }
            return commentId;
        }
    }
}
