using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.EntityModel;

namespace SocialMedia.DataSet
{
    public class UserSet : IUserSet
    {
       private static readonly List<User>  _users = new();
        
       public List<User> RetrieveUsers()
        {
            return _users.DistinctBy(user => user.Id).ToList();
        }

        public void AddUser(User user)
        {

            if(user != null)
            {
                _users.Add(user);
            }
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
