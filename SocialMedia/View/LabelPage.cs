using SocialMedia.Controller;
using SocialMedia.Manager;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;


namespace SocialMedia.View
{
    public class LabelPage
    {
        public int InitiateLabelPage()
        {
            InputHelper.ClearConsole();
            "".PrintLine();
            "".PrintLine();
            "".PrintLine();
            "Label page view".PrintLine();
            
            "1. create a label ".PrintLine();
            "2. Remove a label".PrintLine();
            "3. back to post controller".PrintLine();
            "press an option".PrintLine();

            return InputHelper.UserInputChoice(3);
        }

        public (string, string) CreateLabelPage(List<string> postTitles,List<string>labelNames)
        {
            "choose a label to Add".PrintLine();
            var index = 1;
            foreach (var uniqueName in labelNames)
            {
                $"{index} {uniqueName}".PrintLine();
                index++;
            }
            $"{index} create custome label".PrintLine();

            var userChoice = InputHelper.UserInputChoice(labelNames.Count+1);
            string userChoiceForLabelName = string.Empty;
            if(userChoice == index)
            {
                "Enter the name for the label : ".PrintLine();
                userChoiceForLabelName = InputHelper.GetText();
            }
            else
            {
                userChoiceForLabelName = labelNames[userChoice - 1];
            }
            "----------User's post titles----------".PrintLine();
            index = 1;
            foreach (var postTitle in postTitles)
            {
                $"{index}. {postTitle}".PrintLine();
                index++;
            }
            "press shown index to select a post for the label".PrintLine();
            var userSelectedPostTitle = InputHelper.UserInputChoice(postTitles.Count);

            var lowerCasedUserChoiceForLabelName = userChoiceForLabelName.ToLower();

            return (lowerCasedUserChoiceForLabelName, postTitles[userSelectedPostTitle-1]);
        }

        internal Label RemoveLabel(List<Label>labels)   
        {
            List<string> uniqueLabelNames = labels.DistinctBy(label => label.Name).Select(label => label.Name).ToList();
            List<List<Label>> uniqueLabelList = new List<List<Label>>();

            "choose a label to remove".PrintLine();
            var index = 1;
            foreach (var uniqueName in uniqueLabelNames)
            {
                var uniqueLabels = labels.Where(label => label.Name == uniqueName).ToList();
                uniqueLabelList.Add(uniqueLabels);
                $"{index} {uniqueName}".PrintLine();
                index++;
            }
            var userChoice = InputHelper.UserInputChoice(uniqueLabelNames.Count);
            var userSelectedLabel = uniqueLabelList[userChoice - 1];
            List<PostBObj> posts = UserManager.Instance.GetUserPostBObjs(ApplicationController.Instance.User.Id);
            List<Label> userLabel = new List<Label>();
            for(int i=0; i < posts.Count; i++)
            {

                var userLabels = userSelectedLabel.Where(l => l.PostId == posts[i].Id).ToList();
                foreach (var label in userLabels)
                {
                    userLabel.Add(label);
                }
            }

            List<string> postTitles = posts.Select(posts => posts.Title).ToList();
            index = 97;

            foreach (var label in userLabel)
            {
                var title = posts.Single(post => post.Id == label.PostId).Title;
                $"({(char)index}) {title}".PrintLine();
                index++;
            }
            var userInputChoice = InputHelper.UserInputChoice(97, index);
            var labelToBeRemoved = userSelectedLabel[(int)userInputChoice - 97];

            return labelToBeRemoved;
        }

        internal void NoLabelAvailable()
        {
            "Create a label to delete it ".PrintLine();
            "press any number to get back to post menu".PrintLine();
            var userChoice = InputHelper.GetPositiveInt();
            
        }

        internal int LabelAlreadyExist()
        {
            "post is already exist in the label".PrintLine();
            "1. add different post to the label".PrintLine();
            "2. back".PrintLine();
            return InputHelper.UserInputChoice(2);
        }
    }
}
