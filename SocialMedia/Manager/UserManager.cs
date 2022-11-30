using SocialMedia.DataSet;
using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;

namespace SocialMedia.Manager
{
    public sealed class UserManager
    {
        private static UserManager userManager = null;
        private static readonly object padlock = new object();

        private UserManager()
        {
        }

        public static UserManager Instance
        {
            get
            {
                if (userManager == null)
                {
                    lock (padlock)
                    {
                        if (userManager == null)
                        {
                            userManager = new UserManager();
                        }
                    }
                }
                return userManager;
            }
        }

        IUserSet userSet = new UserSet();
        IFollowerFollowingSet followerFollowingSet = new FollowerFollowingSet();
        TextPostManager textPostManager = TextPostManager.Instance;
        PollPostManager pollPostManager = PollPostManager.Instance;


        public List<UserBObj> GetUserBObjs()
        {
            List<UserBObj> userBobjs = new List<UserBObj>();
            List<TextPostBObj> textPosts = textPostManager.GetTextPostBobjs();
            List<PollPostBObj> pollPosts = pollPostManager.GetPollPostBobjs();
            List<FollowerFollowing> followerFollowing = followerFollowingSet.GetFollowerFollowingList();
            List<User> users = userSet.RetrieveUsers();
            UserBObj userBobj;

            for(int i = 0; i < users.Count; i++)
            {
                userBobj = new UserBObj();
                var userTextPostBobjs = textPosts.Where((textPost) => textPost.PostedBy == users[i].Id).ToList();
                var userPollPostBobjs = pollPosts.Where((pollPost) => pollPost.PostedBy == users[i].Id).ToList();
                var followersId = followerFollowing.Where(_followerFollowing => _followerFollowing.FollowingId == users[i].Id).Select(_followerFollowing => _followerFollowing.FollowerId).ToList();
                var followingsId = followerFollowing.Where(_followerFollowing => _followerFollowing.FollowerId == users[i].Id).Select(_followerFollowing => _followerFollowing.FollowingId).ToList();

                
                userBobj.Id = users[i].Id;
                userBobj.UserName = users[i].UserName;
                userBobj.FirstName = users[i].FirstName;
                userBobj.LastName = users[i].LastName;
                userBobj.Gender = users[i].Gender;
                userBobj.CreatedAt = users[i].CreatedAt;
                userBobj.MaritalStatus = users[i].MaritalStatus;
                userBobj.Occupation = users[i].Occupation;
                userBobj.Education = users[i].Education;
                userBobj.Place = users[i].Place;
                userBobj.TextPosts = userTextPostBobjs;
                userBobj.PollPosts = userPollPostBobjs;
                userBobj.FollowersId = followersId;
                userBobj.FollowingsId = followingsId;

                userBobjs.Add(userBobj);
            }

            return userBobjs;
        }
       
        public void AddUser(User user)
        {
            userSet.AddUser(user);
        }

        public void RemoveUser(UserBObj userBObj)
        {
            userSet.RemoveUser(userBObj.Id);
        }

        public UserBObj? GetUserBobj(string userId)
        {
            var user = userSet.RetrieveUsers().SingleOrDefault(user => user.Id == userId);
            if(user != null)
            {
                var textPosts = TextPostManager.Instance.GetTextPostBobjs().Where(textPostBObj => textPostBObj.PostedBy == user.Id).ToList();
                var pollPosts = PollPostManager.Instance.GetPollPostBobjs().Where(pollPost => pollPost.PostedBy == user.Id).ToList();
                var followersId = followerFollowingSet.GetFollowerFollowingList().Where(_followerFollowing => _followerFollowing.FollowingId == userId).Select(_followerFollowing => _followerFollowing.FollowerId).ToList();
                var followingsId = followerFollowingSet.GetFollowerFollowingList().Where(_followerFollowing => _followerFollowing.FollowerId == userId).Select(_followerFollowing => _followerFollowing.FollowingId).ToList();
                var userBobj = ConvertModelToBObj(user, textPosts, pollPosts, followersId, followingsId);

                return userBobj;
            }
            return null;
        }

        public UserBObj GetNonNullUserBObj(string userId)
        {
            var user = userSet.RetrieveUsers().Single(user => user.Id == userId);
            var textPosts = TextPostManager.Instance.GetTextPostBobjs().Where(textPostBobj => textPostBobj.PostedBy == user.Id).ToList();
            var pollPosts = PollPostManager.Instance.GetPollPostBobjs().Where(pollPost => pollPost.PostedBy == user.Id).ToList();
            var followersId = followerFollowingSet.GetFollowerFollowingList().Where(_followerFollowing => _followerFollowing.FollowingId == userId).Select(_followerFollowing => _followerFollowing.FollowerId).ToList();
            var followingsId = followerFollowingSet.GetFollowerFollowingList().Where(_followerFollowing => _followerFollowing.FollowerId == userId).Select(_followerFollowing => _followerFollowing.FollowingId).ToList();
            var userBobj = ConvertModelToBObj(user, textPosts, pollPosts, followersId, followingsId);

            return userBobj;
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
        public UserBObj? GetUserBObjWithoutId(string userIdentifyingValue)
        {
            var user = userSet.RetrieveUsers().SingleOrDefault(user => user.UserName == userIdentifyingValue || user.MailId == userIdentifyingValue || user.PhoneNumber == userIdentifyingValue);
            if(user != null)
            {
                var textPosts = TextPostManager.Instance.GetTextPostBobjs().Where(textPostBobj => textPostBobj.PostedBy == user.Id).ToList();
                var pollPosts = PollPostManager.Instance.GetPollPostBobjs().Where(pollPost => pollPost.PostedBy == user.Id).ToList();
                var followersId = followerFollowingSet.GetFollowerFollowingList().Where(_followerFollowing => _followerFollowing.FollowingId == user.Id).Select(_followerFollowing => _followerFollowing.FollowerId).ToList();
                var followingsId = followerFollowingSet.GetFollowerFollowingList().Where(_followerFollowing => _followerFollowing.FollowerId == user.Id).Select(_followerFollowing => _followerFollowing.FollowingId).ToList();
                var userBobj = ConvertModelToBObj(user, textPosts, pollPosts, followersId, followingsId);

                return userBobj;
            }
            return null;

        }

        public void Unfollow(string viewingUserId, string searchedUserId) 
        {
            var followerFollowing = followerFollowingSet.GetFollowerFollowingList().Single(ff => ff.FollowerId == viewingUserId && ff.FollowingId == searchedUserId);
            followerFollowingSet.RemoveFollowerFollowing(followerFollowing);

        }

        public void Follow(string viewingUserId, string searchedUserId)
        {
            var followerFollowing = new FollowerFollowing();
            followerFollowing.FollowerId = viewingUserId;
            followerFollowing.FollowingId = searchedUserId;
            followerFollowingSet.AddFollowerFollowing(followerFollowing);
        }
    }
}
