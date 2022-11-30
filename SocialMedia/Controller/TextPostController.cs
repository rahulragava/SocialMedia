using SocialMedia.Manager;
using SocialMedia.Model.BusinessModel;
using SocialMedia.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Controller
{
    public class TextPostController
    {
        private static readonly object _lock = new object();
        private static TextPostController _instance;

        TextPostPage _textPostPage;
        UserBObj _user;
        Action _BackToPostController;

        TextPostManager _textPostManager = TextPostManager.Instance;

       

        private TextPostController()
        {

        }

        public static TextPostController GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new TextPostController();
                        }
                    }
                }
                return _instance;
            }
        }

        public void Initialize(Action BackToPostController)
        {
            _BackToPostController = BackToPostController;
            _textPostPage= new TextPostPage();
            InitiateTextPostController();

        }

        public void InitiateTextPostController()
        {
            _user = ApplicationController.user;
            var userChoice = _textPostPage.InitiateTextPostPage();

            switch (userChoice)
            {
                case 1:
                    CreatePost();
                    break;
                case 2:
                    RemovePost();
                    break;
                case 3:
                    EditPost();
                    break;
                case 4:
                    BackToPostController();
                    break;
            }
        }

        public void CreatePost()
        {
            (string title, string content, DateTime createdAt) postContent = _textPostPage.CreatePostView();
            TextPostBObj textPost = new TextPostBObj();
            textPost.Title = postContent.title;
            textPost.Content = postContent.content;
            textPost.CreatedAt = postContent.createdAt;
            textPost.PostedBy = _user.Id;

            _textPostManager.AddTextPost(textPost);
            if(_user.TextPosts != null)
                _user.TextPosts.Add(textPost);
            _textPostPage.SuccessfullyWorkDoneMessage("Added");
            BackToPostController();
        }
        public void RemovePost()
        {
            if (_user.TextPosts != null && _user.TextPosts.Count > 0)
            {
                var selectedTextPost = _textPostPage.GetUserSelectedPostToRemove(_user.TextPosts);
                _textPostManager.RemoveTextPost(selectedTextPost);
                _textPostPage.SuccessfullyWorkDoneMessage("Removed");
                var removedPostIndex = _user.TextPosts.FindIndex(textPost => textPost.Id == selectedTextPost.Id);
                _user.TextPosts.RemoveAt(removedPostIndex);
                BackToPostController();
            }
            else
            {
                _textPostPage.NoPostsAvailableToEditMessage();
                this.InitiateTextPostController();
            }

        }
        public void EditPost()
        {
            if (_user.TextPosts != null && _user.TextPosts.Count > 0)
            {
                var selectedTextPost = _textPostPage.GetUserSelectedPostToEdit(_user.TextPosts);
                var userChoice = _textPostPage.GetUserChoiceToEdit();

                switch (userChoice)
                {
                    case 1: // edit poll title
                        (string newTitle, DateTime editedTime) editTitle = _textPostPage.EditPostTitle(selectedTextPost);
                        selectedTextPost.Title = editTitle.newTitle;
                        selectedTextPost.LastModifiedAt = editTitle.editedTime;
                        _textPostManager.EditTextPost(selectedTextPost);
                        var textPostIndex = _user.TextPosts.FindIndex(textPost => textPost.Id == selectedTextPost.Id);
                        _user.TextPosts[textPostIndex].Title = editTitle.newTitle;
                        _user.TextPosts[textPostIndex].LastModifiedAt = editTitle.editedTime;
                        _textPostPage.SuccessfullyWorkDoneMessage("Edited");
                        break;

                    case 2: //edit poll Content
                        (string newContent, DateTime editedTime) editContent = _textPostPage.EditPostContent(selectedTextPost);
                        selectedTextPost.Content = editContent.newContent;
                        selectedTextPost.LastModifiedAt = editContent.editedTime;
                        _textPostManager.EditTextPost(selectedTextPost);
                        textPostIndex = _user.TextPosts.FindIndex(textPost => textPost.Id == selectedTextPost.Id);
                        _user.TextPosts[textPostIndex].Content = editContent.newContent;
                        _user.TextPosts[textPostIndex].LastModifiedAt = editContent.editedTime;
                        _textPostPage.SuccessfullyWorkDoneMessage("Edited");
                        break;

                    case 3: // 
                        break;
                }
            }
            else
            {
                _textPostPage.NoPostsAvailableToEditMessage();
            }
            InitiateTextPostController();


        }

        public void BackToPostController()
        {
            _BackToPostController?.Invoke();
        }
    }
}
