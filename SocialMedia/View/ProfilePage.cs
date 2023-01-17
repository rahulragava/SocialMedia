using SocialMedia.Constant;
using SocialMedia.Manager;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;
using System.Text;

namespace SocialMedia.View
{
    public class ProfilePage
    {
        public int InitiateProfilePage(UserBObj searchedUser)
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
            $"      followers count        : {searchedUser.FollowersId.Count}".PrintLine();
            $"      following count        : {searchedUser.FollowingsId.Count}".PrintLine();
            "".PrintLine();
            "".PrintLine();
            InputHelper.ResetConsoleColor();
            "1. View User Text Post".PrintLine();
            "2. View User Poll Post".PrintLine();
            "3. Follow/Unfollow".PrintLine();
            "4. show followers list".PrintLine();
            "5. show following list".PrintLine();
            "6. Exit to searchMenu".PrintLine();

            return InputHelper.UserInputChoice(6);
        }


        public TextPostBObj ViewUserTextPosts(List<TextPostBObj> textPosts)
        {
            InputHelper.ClearConsole();
            var indexCount = 1;
            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (var textPost in textPosts)
            {
                $"{indexCount}. ".PrintLine();
                $"   Title               : {textPost.Title}".PrintLine();
                $"   posted at           : {textPost.CreatedAt}".PrintLine();
                $"   Last modified at    : {textPost.LastModifiedAt}".PrintLine();

                indexCount++;
            }
            InputHelper.ResetConsoleColor();
            "".PrintLine();
            var userChoice = InputHelper.UserInputChoice(textPosts.Count);

            return textPosts[userChoice - 1];
        }

        public (PollPostBObj,int) ViewUserPollPosts(List<PollPostBObj> pollPosts)
        {
            InputHelper.ClearConsole();
            var indexCount = 1;
            Console.ForegroundColor = ConsoleColor.Blue;
            foreach(var pollPost in pollPosts)
            {
                $"{indexCount}.  ".PrintLine();
                $"   Title    : {pollPost.Title}".PrintLine();
                $"   posted at    : {pollPost.CreatedAt}".PrintLine();
                $"   Last modified at    : {pollPost.LastModifiedAt}".PrintLine();

                indexCount++;
            }
            InputHelper.ResetConsoleColor();
            "".PrintLine();
            var userChoice = InputHelper.UserInputChoice(pollPosts.Count);

            return (pollPosts[userChoice - 1], userChoice-1);
        }


        public void ViewUserTextPost(TextPostBObj selectedTextPost)
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
                //var reaction = 0;
                Console.ForegroundColor = ConsoleColor.DarkCyan;

                "+---------------------------------------------------------------------------------------------------".PrintLine();
                "".PrintLine();
                int count = 0;

                foreach (ReactionType reactionType in Enum.GetValues(typeof(ReactionType)))
                {
                    var reaction = GetReactionEmoteIconCount(reactions, reactionType);
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

        public int ViewUserPollPost(PollPostBObj selectedPollPost) 
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
            foreach(var option in selectedPollPost.Choices)
            {
                $"      {index}. {option.Choice}".PrintLine();
                index++;
            }

            "".PrintLine();

            var userChoiceSelection = InputHelper.UserInputChoice(selectedPollPost.Choices.Count);
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

        public int UserTextPostsOptions(TextPostBObj selectedTextPost)
        {
            "1. Comment".PrintLine();
            "2. View comment".PrintLine();
            "3. React".PrintLine();
            var userChoice = InputHelper.UserInputChoice(3);
            return userChoice;
        }


        public int GetUserChoiceOfReaction()
        {
            "1. Add reaction".PrintLine();
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

        public void ViewPollResult(PollPostBObj pollPostBobj)
        {
            var emoteIcons = new List<string>() { "\x263A", ":(", "0_0", "o.0", "\x2665", "thumbs up", "thumbsdown", "\x2665 X" }; // emoji is not supproted in c# console
            Console.OutputEncoding = Encoding.UTF8;
        
            var reactions = pollPostBobj.Reactions;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            "".PrintLine();
            "+-----------------------------------------------------------------------------------------------------------".PrintLine();
            "".PrintLine();
            int count = 0;
            foreach (ReactionType reactionType in Enum.GetValues(typeof(ReactionType)))
            {
                var reaction = GetReactionEmoteIconCount(reactions, reactionType);
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
            foreach (var choice in pollPostBobj.Choices)
            {
                totalVotes += choice.ChoiceSelectedUsers.Count();
            }

            Console.ForegroundColor = ConsoleColor.Green;

            int totalStar = 10;
            foreach (var choice in pollPostBobj.Choices)
            {
                "".PrintLine();
                var selectedPercent = (choice.ChoiceSelectedUsers.Count() * 100) / totalVotes;
                string voted = string.Concat(Enumerable.Repeat("0 ", (int)selectedPercent / 10));
                string unvoted = string.Concat(Enumerable.Repeat("X ", totalStar - (int)selectedPercent / 10));

                $"{choice.Choice}         ===> { voted }{ unvoted } | { selectedPercent } %".PrintLine();
            }
            "".PrintLine();
            InputHelper.ResetConsoleColor();
            $"total vote : {totalVotes}".PrintLine();
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

        public (int,List<string>) CommentView(PostBObj postBobj)
        {
            List<string> commentIds = new List<string>();
            Console.ForegroundColor = ConsoleColor.Cyan;
            
            "                COMMENT SECTION                ".PrintLine();
            "------------------------------------------------".PrintLine();
            var index = 1;
            foreach (var comment in postBobj.Comments)
            {
                $"  {new string(' ', comment.Depth)} {index}. {comment.Content}".PrintLine();
                $"  {new string(' ', comment.Depth + 5)} - ( {UserManager.Instance.GetNonNullUserBObj(comment.CommentedBy).UserName}.  {(DateTime.Now - comment.CommentedAt).Days} days ago )".PrintLine();
                //var IdNumber = int.Parse(comment.Id.Substring(2));
                commentIds.Add(comment.Id);
                index++;
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


        public (string,string) ReplyView(List<string> commentIds)
        {
            "press respective index number to reply to that comment".PrintLine();

            var commentIndex = InputHelper.UserInputChoice(commentIds.Count);
            var commentId = commentIds[commentIndex-1]; //comment id

            "Enter your reply".PrintLine();
            var content = InputHelper.GetText();

            return (content, commentId);
        }

        public string GetCommentId(List<string> commentIds)
        {
            "press comment id number to reply to that comment".PrintLine();

            var commentIndex = InputHelper.UserInputChoice(commentIds.Count);
            var commentId = commentIds[commentIndex-1];

            return commentId;
        }

        internal bool ConfirmationMessageToUnfollow()
        {
            "Are you sure, you want to unfollow this account ?".PrintLine();
            "1. yes".PrintLine();
            "2. no".PrintLine();

            var userChoice = InputHelper.UserInputChoice(2);
            return userChoice == 1 ? true : false;
        }

        internal bool ConfirmationMessageToFollow()
        {
            "Are you sure, you want to follow this account ?".PrintLine();
            "1. yes".PrintLine();
            "2. no".PrintLine();

            var userChoice = InputHelper.UserInputChoice(2);
            return userChoice == 1 ? true : false;
        }

        internal (int, bool) ShowFollowersOrFollowingsList(List<string> userNames)
        {
            var index = 1;
            foreach (var userName in userNames)
            {
                $"{index}. {userName}".PrintLine();
                index++;
            }
            "".PrintLine();
            "".PrintLine();
            "1. select the user to go to their profile".PrintLine();
            "2.Back to searched user profile ".PrintLine();
            var userChoice = InputHelper.UserInputChoice(2);
            switch (userChoice)
            {
                case 1:
                    var selectedUserIndex = InputHelper.UserInputChoice(userNames.Count);
                    return (selectedUserIndex, true);
                case 2:
                    break;
            }
            return (0, false);

        }

        public void UserCantFollowThemselfMessage()
        {
            "you cannot follow/unfollow yourself".PrintLine();
            "press any number to get back".PrintLine();
            var userChoice = InputHelper.GetPositiveInt();
        }

        public void NoFollowingsForUserMessage()
        {
            "There is no followings for the user".PrintLine();
            "press Any number to go back to profile menu".PrintLine();
            InputHelper.GetPositiveInt();
        }

        public void NoFollowersForUserMessage()
        {
            "There is no followings for the user".PrintLine();
            "press Any number to go back to profile menu".PrintLine();
            InputHelper.GetPositiveInt();
        }
    }
}
