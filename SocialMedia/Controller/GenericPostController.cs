using SocialMedia.Manager;
using SocialMedia.Model.BusinessModel;
using SocialMedia.View;


namespace SocialMedia.Controller
{
    public sealed class GenericPostController
    {
        private static GenericPostController _instance;
        private static readonly object _padLock = new ();

        Action _backToPostController;
        CommonPostView _commonPostView;
        readonly UserBObj _user = ApplicationController.Instance.User;
        readonly PostManager _postManager = PostManager.Instance;


        GenericPostController() { }

        public void Initialize(Action backToPostController)
        {
            _backToPostController = backToPostController;
            _commonPostView = new CommonPostView();
            InitiateGenericPostController();
        }

        public static GenericPostController GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_padLock)
                    {
                        //if instance is null instantiate happens, else skips the next line ..
                        _instance ??= new GenericPostController();
                    }
                }
                return _instance;
            }
        }

        public void InitiateGenericPostController()
        {
            var userChoice = _commonPostView.InitiatePostPage();

            switch (userChoice)
            {
                case 1: // create and add post
                    CreatePost();
                    break;
                case 2:
                    RemovePost();
                    break;
                case 3:
                    EditPost();
                    break;
                case 4:
                    _backToPostController();
                    break;
            }
        }

        public void CreatePost()
        {
            PostBObj postBObj;
            var userChoice = _commonPostView.SelectPostTypeView("Add");
            switch (userChoice)
            {
                case 1:
                    postBObj = new TextPostBObj();
                    break;
                case 2:
                    postBObj = new PollPostBObj();
                    break;
                case 3:
                    _backToPostController();
                    return;
                default:
                    return;
            }
            postBObj.PostedBy = _user.Id;
            postBObj.CreatedAt = DateTime.Now;
            postBObj.LastModifiedAt = postBObj.CreatedAt;
            postBObj.Title = _commonPostView.GetPostTitle();

            switch (userChoice)
            {
                case 1: // textPost
                    var textPostBObj = postBObj as TextPostBObj;
                    if (textPostBObj != null)
                    {
                        textPostBObj.Content = _commonPostView.GetPostContent();
                        _postManager.AddPost(postBObj);
                        if (_user.TextPosts != null)
                        {
                            _user.TextPosts.Add(textPostBObj);
                        }
                        else
                        {
                            _user.TextPosts = new List<TextPostBObj>
                            {
                                textPostBObj
                            };
                        }
                    }

                    break;
                case 2:
                    (string question, List<string> options) = _commonPostView.GetPostQuestionAndChoices();
                    var pollPostBObj = postBObj as PollPostBObj;
                    if (pollPostBObj != null)
                    {
                        pollPostBObj.Question = question;

                        var pollChoiceBobjList = options.Select(option => new PollChoiceBObj { PostId = pollPostBObj.Id, Choice = option }).ToList();

                        pollPostBObj.Choices = pollChoiceBobjList;
                        _postManager.AddPost(pollPostBObj);
                        if (_user.PollPosts != null)
                        {
                            _user.PollPosts.Add(pollPostBObj);
                        }
                        else
                        {
                            _user.PollPosts = new List<PollPostBObj>
                            {
                                pollPostBObj
                            };
                        }
                    }

                    break;
            }
            _commonPostView.SuccessfullyWorkDoneMessage("Added");
            InitiateGenericPostController();
        }

        private void EditPost()
        {
            var userPostChoice = _commonPostView.SelectPostTypeView("Edit");

            switch (userPostChoice)
            {
                case 1:
                    EditTextPost();
                    break;
                case 2:
                    EditPollPost();
                    break;
                case 3:
                    _backToPostController();
                    return;
            }

        }

        private void EditTextPost()
        {
            if (_user.TextPosts != null && _user.TextPosts.Any())
            {
                var selectedTextPost = _commonPostView.GetUserSelectedPostToEdit(_user.TextPosts) as TextPostBObj;

                var userChoice = _commonPostView.GetUserChoiceToEditTextPost();

                switch (userChoice)
                {
                    case 1: // edit text post title
                        EditTitle(selectedTextPost);
                        break;

                    case 2: //edit post Content
                        (string newContent, DateTime editedTime) = _commonPostView.EditPostContent(selectedTextPost);
                        selectedTextPost.Content = newContent;
                        selectedTextPost.LastModifiedAt = editedTime;
                        _postManager.EditPost(selectedTextPost);
                        var textPostIndex = _user.TextPosts.FindIndex(textPost => textPost.Id == selectedTextPost.Id);
                        _user.TextPosts[textPostIndex].Content = newContent;
                        _user.TextPosts[textPostIndex].LastModifiedAt = editedTime;
                        _commonPostView.SuccessfullyWorkDoneMessage("Edited");
                        break;

                    case 3: // exit
                        break;
                }
            }
            else
            {
                _commonPostView.NoPostsAvailableToEditOrDeleteMessage();
            }
            InitiateGenericPostController();
        }

        private void EditPollPost()
        {
            if (_user.PollPosts != null && _user.PollPosts.Any())
            {
                var selectedPollPost = _commonPostView.GetUserSelectedPostToEdit(_user.PollPosts) as PollPostBObj;
                var userChoice = _commonPostView.GetUserChoiceToEditPollPost();

                switch (userChoice)
                {
                    case 1: // edit poll title
                        EditTitle(selectedPollPost);
                        break;

                    case 2: //edit poll question
                        (string newQuestion, DateTime editedTime) = _commonPostView.EditPostQuestion(selectedPollPost);
                        selectedPollPost.Question = newQuestion;
                        selectedPollPost.LastModifiedAt = editedTime;
                        _postManager.EditPost(selectedPollPost);
                        var pollPostIndex = _user.PollPosts.FindIndex(pollPost => pollPost.Id == selectedPollPost.Id);
                        _user.PollPosts[pollPostIndex].Question = newQuestion;
                        _commonPostView.SuccessfullyWorkDoneMessage("Edited");
                        break;

                    case 3:
                        break;
                }
            }
            else
            {
                _commonPostView.NoPostsAvailableToEditOrDeleteMessage();
            }
            InitiateGenericPostController();
        }

        private void EditTitle(PostBObj selectedPost)
        {
            (string newTitle, DateTime editedTime) = _commonPostView.EditPostTitle(selectedPost);
            selectedPost.Title = newTitle;
            selectedPost.LastModifiedAt = editedTime;
            _postManager.EditPost(selectedPost);

            if(selectedPost is TextPostBObj)
            {
                var textPostIndex = _user.TextPosts.FindIndex(textPost => textPost.Id == selectedPost.Id);
                _user.TextPosts[textPostIndex].Title = newTitle;
                _user.TextPosts[textPostIndex].LastModifiedAt = editedTime;

            }
            else if (selectedPost is PollPostBObj)
            {
                var pollPostIndex = _user.PollPosts.FindIndex(pollPost => pollPost.Id == selectedPost.Id);
                _user.PollPosts[pollPostIndex].Title = newTitle;
                _user.PollPosts[pollPostIndex].LastModifiedAt = editedTime;
            }
            _commonPostView.SuccessfullyWorkDoneMessage("Edited");

        }


        private void RemovePost()
        {
            var userChoice = _commonPostView.SelectPostTypeView("Remove");
            switch (userChoice)
            {
                case 1:
                    RemoveTextPost();
                    break;
                case 2:
                    RemovePollPost();
                    break;
                case 3:
                    _backToPostController();
                    return;
                default:
                    return;
            }
        }

        private void RemoveTextPost()
        {
            if (_user.TextPosts != null && _user.TextPosts.Any())
            {
                var selectedTextPost = _commonPostView.GetUserSelectedPostToRemove(_user.TextPosts);
                _postManager.RemovePost(selectedTextPost);
                _commonPostView.SuccessfullyWorkDoneMessage("Removed");
                LabelManager.Instance.RemoveLabelByPostId(selectedTextPost.Id);
                var removedPostIndex = _user.TextPosts.FindIndex(textPost => textPost.Id == selectedTextPost.Id);
                _user.TextPosts.RemoveAt(removedPostIndex);
                _backToPostController();
            }
            else
            {   
                _commonPostView.NoPostsAvailableToEditOrDeleteMessage();
                InitiateGenericPostController();
            }
        }
        private void RemovePollPost()
        {
            if (_user.PollPosts != null && _user.PollPosts.Any())
            {
                var selectedPollPost = _commonPostView.GetUserSelectedPostToRemove(_user.PollPosts);
                _postManager.RemovePost(selectedPollPost);
                LabelManager.Instance.RemoveLabelByPostId(selectedPollPost.Id);
                var removedPostIndex = _user.PollPosts.FindIndex(pollPost => pollPost.Id == selectedPollPost.Id);
                _user.PollPosts.RemoveAt(removedPostIndex);
                _commonPostView.SuccessfullyWorkDoneMessage("Removed");
                _backToPostController();
            }
            else
            {
                _commonPostView.NoPostsAvailableToEditOrDeleteMessage();
                InitiateGenericPostController();
            }
        }
    }
}
