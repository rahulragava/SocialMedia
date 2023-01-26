using SocialMedia.Manager;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;
using SocialMedia.View;

namespace SocialMedia.Controller
{
    public class ProfileController
    {
        private static readonly object _padlock = new object();
        private static ProfileController _instance;

        string _searchedUserId;
        UserBObj _searchedUser;
        UserBObj _viewingUser;

        ProfilePage _profilePage;
        Action _backToSearchController;
        readonly ReactionManager _reactionManager = ReactionManager.Instance;
        readonly CommentManager _commentManager = CommentManager.Instance;


        ProfileController()
        {
        }

        public static ProfileController GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_padlock)
                    {
                        _instance ??= new ProfileController();
                    }
                }
                return _instance;
            }
        }

        public void Initialize(Action backToSearchController, string searchedUserId) 
        {
            _backToSearchController = backToSearchController;
            _searchedUserId = searchedUserId;
            _profilePage = new ProfilePage();
            InitiateProfileController();
        }

        public void InitiateProfileController() 
        {
            _searchedUser = UserManager.Instance.GetUserBObj(_searchedUserId);
            _viewingUser = ApplicationController.Instance.User;

            var userChoice = _profilePage.InitiateProfilePage(_searchedUser);

            

            switch (userChoice)
            {
                case 1: // view User's text post
                    UserTextPostComponent();
                    break;
                case 2: // view User's poll post
                    UserPollPostComponent();
                    break;
                case 3: //follow/unfollow
                    CheckAndUpdateFollowing();
                    break;
                case 4: //followers list
                    ShowFollowersList();
                    break;
                case 5: //following list
                    ShowFollowingList();
                    break;
                case 6: //back to search menu 
                    BackToSearchController();
                    break;
            }
        }

        private void ShowFollowingList()
        {
            var userManager = UserManager.Instance;
            var followings = new List<UserBObj>();

            if (!_searchedUser.FollowingsId.Any())
            {
                _profilePage.NoFollowingsForUserMessage();
                InitiateProfileController();
            }
            else
            {
                foreach (var followingId in _searchedUser.FollowingsId)
                {
                    followings.Add(userManager.GetUserBObj(followingId));
                }
                var userNames = followings.Select(following => following.UserName).ToList();
                (int userSelectedIndex, bool isSelectedUserProfile) = _profilePage.ShowFollowersOrFollowingsList(userNames);
                if (isSelectedUserProfile)
                {
                    Initialize(_backToSearchController, followings[userSelectedIndex - 1].Id);
                }
                else
                {
                    InitiateProfileController();
                }
            }
        }

        private void ShowFollowersList()
        {
            var userManager = UserManager.Instance;
            var followers = new List<UserBObj>();


            if (!_searchedUser.FollowersId.Any())
            {
                _profilePage.NoFollowersForUserMessage();
                InitiateProfileController();
            }
            else
            {
                foreach (var followerId in _searchedUser.FollowersId)
                {
                    followers.Add(userManager.GetUserBObj(followerId));
                }
                var userNames = followers.Select(follower => follower.UserName).ToList();
                (int userSelectedIndex, bool isSelectedUserProfile) = _profilePage.ShowFollowersOrFollowingsList(userNames);
                if (isSelectedUserProfile)
                {
                    Initialize(_backToSearchController, followers[userSelectedIndex - 1].Id);
                }
                else
                {
                    InitiateProfileController();
                }
            }
        }

        private void CheckAndUpdateFollowing() 
        {
            var isFollowed = _searchedUser.FollowersId.Exists(followerId => followerId == _viewingUser.Id);
            if (isFollowed) //already following, now will check for unfollowing
            {

                if (_searchedUser.Id == _viewingUser.Id)
                {
                    _profilePage.UserCantFollowThemselfMessage();
                }
                else
                {
                    var isConfirmed = _profilePage.ConfirmationMessageToUnfollow();

                    if (isConfirmed)
                    {

                        UserManager.Instance.Unfollow(_viewingUser.Id, _searchedUser.Id);
                        _viewingUser.FollowingsId.Remove(_searchedUserId);
                        _searchedUser.FollowersId.Remove(_viewingUser.Id);
                    }
                }
                
            }
            else // not followed yet, 
            {
                if (_searchedUser.Id == _viewingUser.Id)
                {
                    _profilePage.UserCantFollowThemselfMessage();
                }
                else
                {
                    var isConfirmed = _profilePage.ConfirmationMessageToFollow();
                    if (isConfirmed)
                    {
                        UserManager.Instance.Follow(_viewingUser.Id, _searchedUser.Id);
                        _viewingUser.FollowingsId.Add(_searchedUserId);
                        _searchedUser.FollowersId.Add(_viewingUser.Id);
                    }
                }
               
            }
            InitiateProfileController();
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

                        CommentBObj comment = new CommentBObj();
                        var commentContent = _profilePage.GetUserComment();

                        comment.PostId = selectedTextPost.Id;
                        comment.ParentCommentId = null;
                        comment.CommentedBy = _viewingUser.Id;
                        comment.CommentedAt = DateTime.Now;
                        comment.Content = commentContent;
                        //comment.Id = newCommentId;

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
                
                (PollPostBObj selectedPollPost,int pollPostIndex) pollPostBobj = _profilePage.ViewUserPollPosts(_searchedUser.PollPosts);
                var isUserYetToSelect = true;
                foreach(var choice in pollPostBobj.selectedPollPost.Choices)
                {
                    var userAlreadySelected = choice.ChoiceSelectedUsers.SingleOrDefault(choice => choice.SelectedBy == _viewingUser.Id);
                    if(userAlreadySelected != null)
                    {
                        isUserYetToSelect = false;
                        break;
                    }
                    
                }
                if (isUserYetToSelect)
                {
                    var userSelectedOptionId = _profilePage.ViewUserPollPost(pollPostBobj.selectedPollPost);
                    var selectedOption = pollPostBobj.selectedPollPost.Choices[userSelectedOptionId-1];
                    var userSelectionPollChoice = new UserPollChoiceSelection(selectedOption.Id, _viewingUser.Id);

                    _searchedUser.PollPosts[pollPostBobj.pollPostIndex].Choices.Single(choice => choice.Id == selectedOption.Id).ChoiceSelectedUsers.Add(userSelectionPollChoice);
                    PollChoiceManager.Instance.AddChoiceSelectedUser(userSelectionPollChoice);

                    _profilePage.ViewPollResult(pollPostBobj.selectedPollPost);

                }
                else
                {
                    _profilePage.ViewPollResult(pollPostBobj.selectedPollPost);
                }

                var userChoice = _profilePage.GetUserChoice();

                switch (userChoice)
                {
                    case 1:
                        CommentBObj comment = new CommentBObj();
                        var commentContent = _profilePage.GetUserComment();

                        comment.PostId = pollPostBobj.selectedPollPost.Id;
                        comment.ParentCommentId = null;
                        comment.CommentedBy = _viewingUser.Id;
                        comment.CommentedAt = DateTime.Now;
                        comment.Content = commentContent;

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

        private void CommentComponent(PostBObj selectedPostBobj)
        {
            (int userChoice, List<string> commentIds) = _profilePage.CommentView(selectedPostBobj);
            CommentBObj commentBobj = new CommentBObj();
            switch (userChoice)
            {
                case 1: // reply 
                    (string content, string parentCommentId) = _profilePage.ReplyView(commentIds);
                    commentBobj.ParentCommentId = parentCommentId;
                    commentBobj.CommentedBy = _viewingUser.Id;
                    commentBobj.CommentedAt = DateTime.Now;
                    commentBobj.PostId = selectedPostBobj.Id;
                    commentBobj.Content = content;
                    var parentCommentAt = selectedPostBobj.Comments.FindIndex(comment => comment.Id == commentBobj.ParentCommentId);
                    commentBobj.Depth = selectedPostBobj.Comments[parentCommentAt].Depth + 1;

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

        private void ReactionComponent(PostBObj postBobj)
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
                    break;
            }
        }

        private void RemoveReaction(PostBObj postBobj)
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


        }

        private void AddReaction(PostBObj postBobj)
        {
            var userReaction = _profilePage.GetUserReaction();
            var reaction = new Reaction
            {
                ReactedBy = _viewingUser.Id,
                ReactionOnId = postBobj.Id,
                ReactionType = userReaction
            };
            var isViewingUserReacted = postBobj.Reactions.Exists(reaction => reaction.ReactedBy == _viewingUser.Id);
            if (isViewingUserReacted)
            {
                var alreadyReacted = postBobj.Reactions.Single(reaction => reaction.ReactedBy == _viewingUser.Id);
                _reactionManager.RemoveReaction(alreadyReacted);
                postBobj.Reactions.Remove(alreadyReacted);

            }
            
            postBobj.Reactions.Add(reaction);

            _reactionManager.AddReaction(reaction);
            _profilePage.SuccessfullyWorkDoneMessage("Added");
            //InitiateProfileController();
        }

        private void CommentReactionComponent(CommentBObj commentBobj)
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
                    break;
            }
        }

        private void RemoveCommentReaction(CommentBObj commentBobj)
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
        }

        private void AddCommentReaction(CommentBObj commentBobj)
        {
            var userReaction = _profilePage.GetUserReaction();
            var reaction = new Reaction
            {
                ReactedBy = _viewingUser.Id,
                ReactionOnId = commentBobj.Id,
                ReactionType = userReaction
            };
            commentBobj.Reactions ??= new List<Reaction>();
            //commentBobj.Reactions.Add(reaction);
            _reactionManager.AddReaction(reaction);
            _profilePage.SuccessfullyWorkDoneMessage("Added");

        }

        private void BackToSearchController()
        {
            _backToSearchController?.Invoke();
        }
    }
}
