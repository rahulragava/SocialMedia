using SocialMedia.DataSet;
using SocialMedia.Manager;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;
using SocialMedia.View;

namespace SocialMedia.Controller
{
    public class LabelController
    {
        private static LabelController _labelController;
        private static readonly object _padLock = new();

        Action _backToPostController;
        LabelPage _labelPage;

        LabelController() { }

        public static LabelController Instance
        {
            get
            {
                if(_labelController == null)
                {
                    lock (_padLock)
                    {
                        if(_labelController == null)
                        {
                            _labelController = new LabelController();
                        }
                    }
                }
                return _labelController;
            }
        }

        public void Initialize(Action InitiatePostController)
        {
            _backToPostController = InitiatePostController;
            _labelPage = new LabelPage();
            InitiateLabelController();
        }

        public void InitiateLabelController()
        {
            var userChoice = _labelPage.InitiateLabelPage();
            switch (userChoice)
            {
                case 1: // create a label
                    CreateLabel();
                    InitiateLabelController();
                    break;
                case 2:
                    RemoveLabel();
                    InitiateLabelController();
                    break;
                case 3:
                    _backToPostController?.Invoke();
                    break;
            }
        }

        private void RemoveLabel()
        {
            List<Label> labels = LabelManager.Instance.GetUserLabels(ApplicationController.Instance.User.Id);
            if (labels.Any())
            {
                var label = _labelPage.RemoveLabel(labels);
                LabelManager.Instance.RemoveLabel(label);
            }
            else
            {
                _labelPage.NoLabelAvailable();
            }
        }

        private void CreateLabel()
        {
            Label label = new Label();
            List<PostBObj> posts = UserManager.Instance.GetUserPostBObjs(ApplicationController.Instance.User.Id);
            List<string> labelNames = LabelManager.Instance.GetUserLabels(ApplicationController.Instance.User.Id).DistinctBy(label => label.Name).Select(label => label.Name).ToList();

            List<string> postTitles = posts.Select(posts => posts.Title).ToList();

            (string name, string postTitle) = _labelPage.CreateLabelPage(postTitles, labelNames);
            var postId = posts.Single(post => post.Title == postTitle).Id;
            var alreadyExistedLabel = LabelManager.Instance.GetLabels().Where(l => l.Name == name && l.PostId == postId);
            if (!alreadyExistedLabel.Any())
            {
                label.Name = name;
                label.PostId = posts.Single(post => post.Title == postTitle).Id;
                LabelManager.Instance.AddLabel(label);
            }
            else
            {
                var userChoice = _labelPage.LabelAlreadyExist();
                switch (userChoice)
                {
                    case 1:
                        CreateLabel();
                        break;
                    case 2:
                        break;
                }
            }
        }
    }
}
