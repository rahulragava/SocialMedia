using System.Linq.Expressions;
using SocialMedia.DataSet;
using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;

namespace SocialMedia.Manager
{
    public sealed class UserManager
    {
        private static UserManager _userManager = null;
        private static readonly object _padlock = new object();

        private UserManager()
        {
        }

        public static UserManager Instance
        {
            get
            {
                if (_userManager == null)
                {
                    lock (_padlock)
                    {
                        _userManager ??= new UserManager();
                    }
                }
                return _userManager;
            }
        }

        readonly IUserSet _userSet = new UserSet();
        readonly IFollowerFollowingSet _followerFollowingSet = new FollowerSet();
        readonly PostManager _postManager = PostManager.Instance;

       
        public void AddUser(User user)
        {
            _userSet.AddUser(user);
        }

        public void RemoveUser(UserBObj userBObj)
        {
            _userSet.RemoveUser(userBObj.Id);
        }

        public List<PostBObj> GetUserPostBObjs(string userId)
        {
            List<PostBObj> postBObjs = new();
            var textPosts = _postManager.GetPostBObjs().Where(post => post is TextPostBObj && post.PostedBy == userId).Select(post => post as TextPostBObj).ToList();
            var pollPosts = _postManager.GetPostBObjs().Where(post => post is PollPostBObj && post.PostedBy == userId).Select(post => post as PollPostBObj).ToList();
            foreach (var post in textPosts)
            {
                postBObjs.Add(post);
            }
            foreach (var post in pollPosts)
            {
                postBObjs.Add(post);
            }

            return postBObjs;
        }

        public UserBObj GetUserBObj(string userId)
        {
            var user = _userSet.RetrieveUsers().SingleOrDefault(user => user.Id == userId);
            if(user != null)
            {
                var textPosts = _postManager.GetPostBObjs().Where(post => post is TextPostBObj && post.PostedBy == user.Id).Select(post => post as TextPostBObj).ToList();
                var pollPosts = _postManager.GetPostBObjs().Where(post => post is PollPostBObj && post.PostedBy == user.Id).Select(post => post as PollPostBObj).ToList();
                var followersId = _followerFollowingSet.GetFollowerFollowingList().Where(_followerFollowing => _followerFollowing.FollowingId == userId).Select(_followerFollowing => _followerFollowing.FollowerId).ToList();
                var followingsId = _followerFollowingSet.GetFollowerFollowingList().Where(_followerFollowing => _followerFollowing.FollowerId == userId).Select(_followerFollowing => _followerFollowing.FollowingId).ToList();
                var userBObj = ConvertModelToBObj(user, textPosts, pollPosts, followersId, followingsId);

                return userBObj;
            }
            return null;
        }

        public UserBObj ConvertModelToBObj(User user, List<TextPostBObj> textPosts, List<PollPostBObj> pollPosts, List<string> followersId, List<string> followingsId)
        {
            var userBobj = new UserBObj();
            userBobj.Id = user.Id;
            userBobj.UserName = user.UserName;
            userBobj.FirstName = user.FirstName;
            userBobj.LastName = user.LastName;
            userBobj.Gender = user.Gender;
            userBobj.CreatedAt = user.CreatedAt;
            userBobj.MaritalStatus = user.MaritalStatus;
            userBobj.Occupation = user.Occupation;
            userBobj.Education = user.Education;
            userBobj.Place = user.Place;
            userBobj.TextPosts = textPosts;
            userBobj.PollPosts = pollPosts;
            userBobj.FollowersId = followersId;
            userBobj.FollowingsId = followingsId;

            return userBobj;
        }

        public List<string> GetUserNames()
        {
            return _userSet.RetrieveUsers().Select(user => user.UserName).ToList();
        }
        public UserBObj GetUserBObjWithoutId(string userIdentifyingValue)
        {
            var user = _userSet.RetrieveUsers().SingleOrDefault(user => user.UserName == userIdentifyingValue || user.MailId == userIdentifyingValue || user.PhoneNumber == userIdentifyingValue);
            if(user != null)
            {
                var textPosts = _postManager.GetPostBObjs().Where(post => post is TextPostBObj && post.PostedBy == user.Id).Select(post => post as TextPostBObj).ToList();
                var pollPosts = _postManager.GetPostBObjs().Where(post => post is PollPostBObj && post.PostedBy == user.Id).Select(post => post as PollPostBObj).ToList();
                var followersId = _followerFollowingSet.GetFollowerFollowingList().Where(_followerFollowing => _followerFollowing.FollowingId == user.Id).Select(_followerFollowing => _followerFollowing.FollowerId).ToList();
                var followingsId = _followerFollowingSet.GetFollowerFollowingList().Where(_followerFollowing => _followerFollowing.FollowerId == user.Id).Select(_followerFollowing => _followerFollowing.FollowingId).ToList();
                var userBobj = ConvertModelToBObj(user, textPosts, pollPosts, followersId, followingsId);

                return userBobj;
            }
            return null;

        }

        public void Unfollow(string viewingUserId, string searchedUserId) 
        {
            var followerFollowing = _followerFollowingSet.GetFollowerFollowingList().Single(ff => ff.FollowerId == viewingUserId && ff.FollowingId == searchedUserId);
            _followerFollowingSet.RemoveFollowerFollowing(followerFollowing);

        }

        public void Follow(string viewingUserId, string searchedUserId)
        {
            var followerFollowing = new Follower
            {
                FollowerId = viewingUserId,
                FollowingId = searchedUserId
            };
            _followerFollowingSet.AddFollowerFollowing(followerFollowing);
        }

        public List<UserBObj> GetUserBObjs()
        {
            List<UserBObj> userBobjs = new List<UserBObj>();
            var textPosts = _postManager.GetPostBObjs().OfType<TextPostBObj>().ToList();
            var pollPosts = _postManager.GetPostBObjs().OfType<PollPostBObj>().ToList();
            List<Follower> followerFollowing = _followerFollowingSet.GetFollowerFollowingList();
            List<User> users = _userSet.RetrieveUsers();
            UserBObj userBobj;

            foreach (var user in users)
            {
                userBobj = new UserBObj();
                var userTextPostBobjs = textPosts.Where((textPost) => textPost.PostedBy == user.Id).ToList();
                var userPollPostBobjs = pollPosts.Where((pollPost) => pollPost.PostedBy == user.Id).ToList();
                var followersId = followerFollowing.Where(_followerFollowing => _followerFollowing.FollowingId == user.Id).Select(_followerFollowing => _followerFollowing.FollowerId).ToList();
                var followingsId = followerFollowing.Where(_followerFollowing => _followerFollowing.FollowerId == user.Id).Select(_followerFollowing => _followerFollowing.FollowingId).ToList();

                userBobj.Id = user.Id;
                userBobj.UserName = user.UserName;
                userBobj.FirstName = user.FirstName;
                userBobj.LastName = user.LastName;
                userBobj.Gender = user.Gender;
                userBobj.CreatedAt = user.CreatedAt;
                userBobj.MaritalStatus = user.MaritalStatus;
                userBobj.Occupation = user.Occupation;
                userBobj.Education = user.Education;
                userBobj.Place = user.Place;
                userBobj.TextPosts = userTextPostBobjs;
                userBobj.PollPosts = userPollPostBobjs;
                userBobj.FollowersId = followersId;
                userBobj.FollowingsId = followingsId;

                userBobjs.Add(userBobj);
            }

            return userBobjs;
        }
        
    }
}
