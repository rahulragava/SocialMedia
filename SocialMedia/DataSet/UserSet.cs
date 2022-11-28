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
        private static List<User>  _users = new List<User>() { };

       public List<User> RetrieveUsers()
        {
            return _users.DistinctBy(user => user.Id).ToList();
        }

        public void AddUser(User user)
        {

            if(user != null)
                _users.Add(user);
        }
        public void RemoveUser(int userId)
        {
            var user = _users.FirstOrDefault(user => user.Id == userId);
            if(user != null)
            {
                _users.Remove(user);
            }
        }
    }
}
