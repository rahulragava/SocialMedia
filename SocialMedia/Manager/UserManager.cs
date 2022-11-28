using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.DataSet;
using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;
using SocialMedia.View;

namespace SocialMedia.Manager
{
    public sealed class UserManager
    {
        private static UserManager? userManager = null;
        private static readonly object padlock = new object();

        UserManager()
        {
        }

        public static UserManager GetUserManager()
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
        
        IUserSet userSet = new UserSet();
        TextPostManager textPostManager = TextPostManager.GetTextPostManager();
        PollPostManager pollPostManager = PollPostManager.GetPollPostManager();

        public List<UserBobj> GetUserBobjs()
        {
            List<UserBobj> userBobjs = new List<UserBobj>();
            List<TextPostBobj> textPosts = textPostManager.GetTextPostBobjs();
            List<PollPostBobj> pollPosts = pollPostManager.GetPollPostBobjs();
            List<User> users = userSet.RetrieveUsers();
            UserBobj userBobj;

            for(int i = 0; i < users.Count; i++)
            {
                userBobj = new UserBobj();
                var userTextPostBobjs = textPosts.Where((textPost) => textPost.PostedBy == users[i].Id).ToList();
                var userPollPostBobjs = pollPosts.Where((pollPost) => pollPost.PostedBy == users[i].Id).ToList();
                
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

                userBobjs.Add(userBobj);
            }

            return userBobjs;
        }
       
        public void AddUser(User user)
        {
            userSet.AddUser(user);
        }

        public void RemoveUser(UserBobj userBobj)
        {
            userSet.RemoveUser(userBobj.Id);
        }

        public UserBobj? GetUserBobj(int userId)
        {
            var user = userSet.RetrieveUsers().SingleOrDefault(user => user.Id == userId);
            if(user != null)
            {
                var textPosts = TextPostManager.GetTextPostManager().GetTextPostBobjs().Where(textPostBobj => textPostBobj.PostedBy == user.Id).ToList();
                var pollPosts = PollPostManager.GetPollPostManager().GetPollPostBobjs().Where(pollPost => pollPost.PostedBy == user.Id).ToList();
                var userBobj = ConvertModelToBobj(user, textPosts, pollPosts);

                return userBobj;
            }
            return null;
        }

        public UserBobj ConvertModelToBobj(User user, List<TextPostBobj> textPosts, List<PollPostBobj> pollPosts)
        {
            var userBobj = new UserBobj();
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

            return userBobj;
        }
        public UserBobj? GetUserBobj(string userName)
        {
            var user = userSet.RetrieveUsers().SingleOrDefault(user => user.UserName == userName);
            if(user != null)
            {
                var textPosts = TextPostManager.GetTextPostManager().GetTextPostBobjs().Where(textPostBobj => textPostBobj.PostedBy == user.Id).ToList();
                var pollPosts = PollPostManager.GetPollPostManager().GetPollPostBobjs().Where(pollPost => pollPost.PostedBy == user.Id).ToList();
                var userBobj = ConvertModelToBobj(user, textPosts, pollPosts);

                return userBobj;
            }
            return null;

        }
    }
}
