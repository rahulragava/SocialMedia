using SocialMedia.Model.EntityModel;

namespace SocialMedia.DataSet.DataSetInterface
{
    public interface IUserCredentialSet
    {
        List<UserCredential> RetrieveUsersCredential();
        void AddUserCredential(UserCredential userCredential);
        void RemoveUserCredential(UserCredential userCredential);
    }
}