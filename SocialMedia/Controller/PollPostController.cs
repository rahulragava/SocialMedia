using SocialMedia.DataSet;
using SocialMedia.Manager;
using SocialMedia.Model.BusinessModel;
using SocialMedia.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Controller
{
    //pollChoice editing  is not yet done ...
    public class PollPostController
    {
        PollPostPage _pollPostPage;
        UserBobj _user;
        Action _BackToPostController;

        PollPostManager _pollPostManager = PollPostManager.GetPollPostManager();
        PollChoiceManager _pollChoiceManager = PollChoiceManager.GetPollPostManager();

        public PollPostController(Action BackToPostController, UserBobj user)
        {
            _BackToPostController = BackToPostController;
            _user = user;
            _pollPostPage = new PollPostPage();
        }

        public void InitiatePollPostController()
        {
            var userChoice = _pollPostPage.InitiatePollPostPage();
            
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
            var lastPostId = _pollPostManager.GetLastPostId();
            (string title, string Question, DateTime createdAt, List<string> options) postContent = _pollPostPage.CreatePostView();
            PollPostBobj pollPost = new PollPostBobj();
            pollPost.Id = lastPostId + 1;
            pollPost.Title = postContent.title;
            pollPost.Question = postContent.Question;
            pollPost.CreatedAt = postContent.createdAt;
            pollPost.PostedBy = _user.Id;

            var lastPollChoiceId = _pollChoiceManager.GetLastPollChoiceId();
            List<PollChoiceBobj> pollChoiceBobjList = new List<PollChoiceBobj>();
            for(int i = 0; i < postContent.options.Count; i++)
            {
                PollChoiceBobj pollChoice = new PollChoiceBobj();

                pollChoice.Id = lastPollChoiceId + 1;
                pollChoice.PostId = pollPost.Id;
                pollChoice.Choice = postContent.options[i];
                pollChoiceBobjList.Add(pollChoice);

            }
            pollPost.choices = pollChoiceBobjList;
            //_pollChoiceManager.AddPollChoices(pollChoiceBobjList);
            _pollPostManager.AddPollPost(pollPost);
            if(_user.PollPosts != null)
                _user.PollPosts.Add(pollPost);
            _pollPostPage.SuccessfullyWorkDoneMessage("Added");

            BackToPostController();
        }

        public void EditPost()
        {
            if(_user.PollPosts != null && _user.PollPosts.Count > 0)
            {
                var selectedPollPost = _pollPostPage.GetUserSelectedPostToEdit(_user.PollPosts);
                var userChoice = _pollPostPage.GetUserChoiceToEdit();
                
                switch (userChoice)
                {
                    case 1: //edit poll question
                       (string newQuestion, DateTime editedTime) editQuestion = _pollPostPage.EditPostQuestion(selectedPollPost);
                        selectedPollPost.Question = editQuestion.newQuestion;
                        selectedPollPost.LastModifiedAt = editQuestion.editedTime;
                        _pollPostManager.EditPollPost(selectedPollPost);
                        var pollPostIndex = _user.PollPosts.FindIndex(pollPost => pollPost.Id == selectedPollPost.Id);
                        _user.PollPosts[pollPostIndex].Question = editQuestion.newQuestion;
                        _pollPostPage.SuccessfullyWorkDoneMessage("Edited");
                        break;

                    case 2: // edit poll title
                        (string newTitle, DateTime editedTime) editTitle = _pollPostPage.EditPostTitle(selectedPollPost);
                        selectedPollPost.Title = editTitle.newTitle;
                        selectedPollPost.LastModifiedAt = editTitle.editedTime;
                        _pollPostManager.EditPollPost(selectedPollPost);
                        pollPostIndex = _user.PollPosts.FindIndex(pollPost => pollPost.Id == selectedPollPost.Id);
                        _user.PollPosts[pollPostIndex].Title = editTitle.newTitle;
                        _user.PollPosts[pollPostIndex].LastModifiedAt = editTitle.editedTime;
                        _pollPostPage.SuccessfullyWorkDoneMessage("Edited");
                        break;

                    case 3:
                        break;
                }
            }
            else
            {
                _pollPostPage.NoPostsAvailableToEditOrDeleteMessage();
            }
            InitiatePollPostController();
        }

        public void RemovePost()
        {
            if(_user.PollPosts != null && _user.PollPosts.Count > 0)
            {
                var selectedPollPost = _pollPostPage.GetUserSelectedPostToRemove(_user.PollPosts);
                _pollPostManager.RemovePollPost(selectedPollPost);
                var removedPostIndex = _user.PollPosts.FindIndex(pollPost => pollPost.Id == selectedPollPost.Id);
                _user.PollPosts.RemoveAt(removedPostIndex);
                _pollPostPage.SuccessfullyWorkDoneMessage("Removed");
                BackToPostController();

            }
            else
            {
                _pollPostPage.NoPostsAvailableToEditOrDeleteMessage();
                this.InitiatePollPostController();
            }

        }

        public void BackToPostController()
        {
            _BackToPostController?.Invoke();
        }

    }
}
