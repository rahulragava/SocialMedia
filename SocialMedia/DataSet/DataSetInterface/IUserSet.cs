using SocialMedia.Model.EntityModel;

namespace SocialMedia.DataSet.DataSetInterface
{
    public interface IUserSet
    {
        void AddUser(User user);
        void RemoveUser(string userId);
        List<User> RetrieveUsers();
    }
}
