using SocialMedia.DataSet;
using SocialMedia.Manager;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;
using SocialMedia.Model.EntityModel.EnumTypes;
using SocialMedia.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Controller
{
    public class ProfileController
    {
        UserBobj _searchedUser;
        UserBobj _viewingUser;
        ProfilePage _profilePage;
        Action _BackToSearchController;
        ReactionManager _reactionManager = ReactionManager.GetReactionManager();
        CommentManager _commentManager = CommentManager.GetCommentManager();
        
        public ProfileController(Action BackToSearchController, UserBobj searchedUser, UserBobj viewingUser)
        {
            _BackToSearchController = BackToSearchController;
            _searchedUser = searchedUser;
            _viewingUser = viewingUser;
            _profilePage = new ProfilePage();
        }

        public void InitiateProfileController() 
        {
            var userChoice = _profilePage.InitiateProfilePage(_searchedUser);

            switch (userChoice)
            {
                case 1: // view user's text post
                    UserTextPostComponent();
                    break;
                case 2: // view user's poll post
                    UserPollPostComponent();
                    break;
                case 3: //back to search menu 
                    BackToSearchController();
                    break;
            }

        }


        private void UserTextPostComponent()
        {
            if (_searchedUser.TextPosts != null)
            {
                var selectedTextPost = _profilePage.ViewUserTextPosts(_searchedUser.TextPosts);
                _profilePage.ViewUserTextPost(selectedTextPost);
                var selectedOption = _profilePage.UserTextPostsOptions(selectedTextPost);

                switch (selectedOption) 
                {
                    case 1: // Parent comment

                        CommentBobj comment = new CommentBobj();
                        var commentContent = _profilePage.GetUserComment();
                        var lastPostId = _commentManager.GetLastCommentId();

                        comment.PostId = selectedTextPost.Id;
                        comment.ParentCommentId = null;
                        comment.CommentedOn = CommentedOnType.TextPost;
                        comment.CommentedBy = _viewingUser.Id;
                        comment.CommentedAt = DateTime.Now;
                        comment.Content = commentContent;
                        comment.Id = lastPostId + 1;

                        selectedTextPost.Comments.Add(comment);
                        _commentManager.AddComment(comment);
                        InitiateProfileController();
                        break;
                    case 2: // view comment and reply to comments
                        CommentComponent(selectedTextPost);
                        InitiateProfileController();

                        break;
                    case 3: // react to post
                        ReactionComponent(selectedTextPost);
                        InitiateProfileController();

                        break;
                    case 4:
                        InitiateProfileController();
                        break;
                }
            }
        }

        private void UserPollPostComponent()
        {
            if (_searchedUser.PollPosts != null)
            {
                
                (PollPostBobj selectedPollPost,int pollPostIndex) pollPostBobj = _profilePage.ViewUserPollPosts(_searchedUser.PollPosts);
                var isUserYetToSelect = true;
                foreach(var choice in pollPostBobj.selectedPollPost.choices)
                {
                    var userAlreadySelected = choice.choiceSelectedUsers.SingleOrDefault(choice => choice.SelectedBy == _viewingUser.Id);
                    if(userAlreadySelected != null)
                    {
                        isUserYetToSelect = false;
                        break;
                    }
                    
                }
                if (isUserYetToSelect)
                {
                    var userSelectedOptionId = _profilePage.ViewUserPollPost(pollPostBobj.selectedPollPost);
                    var selectedOption = pollPostBobj.selectedPollPost.choices[userSelectedOptionId-1];
                    var userSelectionPollChoice = new UserPollChoiceSelection();
                    userSelectionPollChoice.SelectedBy = _viewingUser.Id;
                    userSelectionPollChoice.ChoiceId = selectedOption.Id;

                    _searchedUser.PollPosts[pollPostBobj.pollPostIndex].choices.Single(choice => choice.Id == selectedOption.Id).choiceSelectedUsers.Add(userSelectionPollChoice);
                    PollChoiceManager.GetPollPostManager().AddChoiceSelectedUser(userSelectionPollChoice);

                    _profilePage.ViewPollResult(pollPostBobj.selectedPollPost);

                }
                else
                {
                    //_profilePage.ViewReactions();
                    _profilePage.ViewPollResult(pollPostBobj.selectedPollPost);

                }

                var userChoice = _profilePage.GetUserChoice();

                switch (userChoice)
                {
                    case 1:
                        CommentBobj comment = new CommentBobj();
                        var commentContent = _profilePage.GetUserComment();
                        var lastPostId = _commentManager.GetLastCommentId();

                        comment.PostId = pollPostBobj.selectedPollPost.Id;
                        comment.ParentCommentId = null;
                        comment.CommentedOn = CommentedOnType.TextPost;
                        comment.CommentedBy = _viewingUser.Id;
                        comment.CommentedAt = DateTime.Now;
                        comment.Content = commentContent;
                        comment.Id = lastPostId + 1;

                        pollPostBobj.selectedPollPost.Comments.Add(comment);
                        _commentManager.AddComment(comment);
                        InitiateProfileController();
                        break;
                    case 2: // view comment and reply to comments
                        CommentComponent(pollPostBobj.selectedPollPost);
                        InitiateProfileController();

                        break;
                    case 3: // react to post
                        ReactionComponent(pollPostBobj.selectedPollPost);
                        InitiateProfileController();
                        break;
                    case 4: // back to profile menu
                        InitiateProfileController();
                        break;
                }
            }
        }

        private void CommentComponent(PostBobj selectedPostBobj)
        {
            (int userChoice, List<int> commentIds) = _profilePage.CommentView(selectedPostBobj);
            CommentBobj commentBobj = new CommentBobj();
            switch (userChoice)
            {
                case 1: // reply 
                    (string content, int parentCommentId) = _profilePage.ReplyView(commentIds);
                    var commentId = _commentManager.GetLastCommentId();
                    commentBobj.Id = commentId + 1;
                    commentBobj.ParentCommentId = parentCommentId;
                    commentBobj.CommentedBy = _viewingUser.Id;
                    commentBobj.CommentedAt = DateTime.Now;
                    commentBobj.PostId = selectedPostBobj.Id;
                    commentBobj.Content = content;
                    var parentCommentAt = selectedPostBobj.Comments.FindIndex(comment => comment.Id == commentBobj.ParentCommentId);
                    commentBobj.Depth = selectedPostBobj.Comments[parentCommentAt].Depth + 1;

                    if (selectedPostBobj is TextPostBobj)
                    {

                        commentBobj.CommentedOn = CommentedOnType.TextPost;
                    }
                    else if (selectedPostBobj is PollPostBobj)
                    {
                        commentBobj.CommentedOn = CommentedOnType.PollPost;
                    }
                    selectedPostBobj.Comments.Insert(parentCommentAt + 1, commentBobj);
                    _commentManager.AddComment(commentBobj);

                    break;
                case 2: // react
                    var userSelectedCommentId = _profilePage.GetCommentId(commentIds);
                    var reactionComment = selectedPostBobj.Comments.Single(comment => comment.Id == userSelectedCommentId);
                    CommentReactionComponent(reactionComment);
                    break;
                case 3: //react to comment
                    break;
            }
        }

        private void ReactionComponent(PostBobj postBobj)
        {
            var userChoice = _profilePage.GetUserChoiceOfReaction();
            switch (userChoice)
            {
                case 1: //add reaction
                    AddReaction(postBobj);
                    break;

                case 2: // remove reaction
                    RemoveReaction(postBobj);
                    break;
                case 3:
                    //InitiateProfileController();
                    break;
            }
        }

        private void RemoveReaction(PostBobj postBobj)
        {
            var isViewingUserReacted = postBobj.Reactions.Exists(reaction => reaction.ReactedBy == _viewingUser.Id);
            if (isViewingUserReacted)
            {
                var userReaction = postBobj.Reactions.Single(reaction => reaction.ReactedBy == _viewingUser.Id);
                _reactionManager.RemoveReaction(userReaction);
                postBobj.Reactions.Remove(userReaction);
                _profilePage.SuccessfullyWorkDoneMessage("Removed");
            }
            else
            {
                _profilePage.NeverReactedMessage();
            }
            //InitiateProfileController();


        }

        private void AddReaction(PostBobj postBobj)
        {
            var userReaction = _profilePage.GetUserReaction();
            var lastPostId = _reactionManager.GetLastReactionId();
            var reaction = new Reaction();
            reaction.Id = lastPostId + 1;
            reaction.ReactedBy = _viewingUser.Id;
            reaction.ReactionOnId = postBobj.Id;
            reaction.reactionType = userReaction;
            
            if(postBobj is TextPostBobj)
            {
                reaction.ReactionOnType = ReactedOnType.TextPost;
            }
            else
            {
                reaction.ReactionOnType = ReactedOnType.PollPost;
            }
            postBobj.Reactions.Add(reaction);

            _reactionManager.AddReaction(reaction);
            _profilePage.SuccessfullyWorkDoneMessage("Added");
            //InitiateProfileController();
        }

        private void CommentReactionComponent(CommentBobj commentBobj)
        {
            var userChoice = _profilePage.GetUserChoiceOfReaction();
            switch (userChoice)
            {
                case 1: //add reaction
                    AddCommentReaction(commentBobj);
                    break;

                case 2: // remove reaction
                    RemoveCommentReaction(commentBobj);
                    break;
                case 3:
                    //InitiateProfileController();
                    break;
            }
        }

        private void RemoveCommentReaction(CommentBobj commentBobj)
        {
            var isViewingUserReacted = commentBobj.Reactions.Exists(reaction => reaction.ReactedBy == _viewingUser.Id);
            if (isViewingUserReacted)
            {
                var userReaction = commentBobj.Reactions.Single(reaction => reaction.ReactedBy == _viewingUser.Id);
                _reactionManager.RemoveReaction(userReaction);
                commentBobj.Reactions.Remove(userReaction);
                _profilePage.SuccessfullyWorkDoneMessage("Removed");
            }
            else
            {
                _profilePage.NeverReactedMessage();
            }
            //InitiateProfileController();

        }

        private void AddCommentReaction(CommentBobj commentBobj)
        {
            var userReaction = _profilePage.GetUserReaction();
            var lastPostId = _reactionManager.GetLastReactionId();
            var reaction = new Reaction();
            reaction.Id = lastPostId + 1;
            reaction.ReactedBy = _viewingUser.Id;
            reaction.ReactionOnId = commentBobj.Id;
            reaction.reactionType = userReaction;
            reaction.ReactionOnType = ReactedOnType.Comment;
            if(commentBobj.Reactions == null )
            {
                commentBobj.Reactions = new List<Reaction>();   
            }
            commentBobj.Reactions.Add(reaction);
            _reactionManager.AddReaction(reaction);
            _profilePage.SuccessfullyWorkDoneMessage("Added");
            //InitiateProfileController();

        }

        private void BackToSearchController()
        {
            _BackToSearchController?.Invoke();
        }

        
    }
}
