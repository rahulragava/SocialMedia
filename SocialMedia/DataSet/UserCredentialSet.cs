using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.EntityModel;

namespace SocialMedia.DataSet
{
    public class UserCredentialSet : IUserCredentialSet
    {
        private static readonly List<UserCredential> _userCredentials = new();

        public List<UserCredential> RetrieveUsersCredential()
        {
            return _userCredentials.DistinctBy(userCredential => userCredential.UserId).ToList();
        }

        public void AddUserCredential(UserCredential userCredential)
        {
            if (userCredential != null)
            {
                _userCredentials.Add(userCredential);
            }
        }
        public void RemoveUserCredential(UserCredential userCredential)
        {
            if (userCredential != null)
            {
                _userCredentials.Remove(userCredential);
            }
        }
    }
}
