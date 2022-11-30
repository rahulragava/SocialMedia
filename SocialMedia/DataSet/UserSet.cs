using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataSet
{
    public class UserSet : IUserSet
    {
        private static List<User>  _users = new List<User>();
        //private static readonly object padLock = new object();
        //private static IUserSet userSet;

        //private UserSet() { }

        //public static IUserSet GetInstance
        //{
        //    get
        //    {
        //        if (userSet == null)
        //        {
        //            lock (padLock)
        //            {
        //                if (userSet == null)
        //                {
        //                    userSet = new UserSet();
        //                }
        //            }
        //        }
        //        return userSet;
        //    }
        //}

       public List<User> RetrieveUsers()
        {
            return _users.DistinctBy(user => user.Id).ToList();
        }

        public void AddUser(User user)
        {

            if(user != null)
                _users.Add(user);
        }
        public void RemoveUser(string userId)
        {
            var user = _users.FirstOrDefault(user => user.Id == userId);
            if(user != null)
            {
                _users.Remove(user);
            }
        }
    }
}
