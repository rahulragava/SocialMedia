using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.DataSet;
using SocialMedia.Model.EntityModel;

namespace SocialMedia.Manager
{
    public sealed class UserCredentialManager
    {
        private static UserCredentialManager _userCredentialManager ;
        private static readonly object _padlock = new object();

        UserCredentialManager()
        {
        }

        public static UserCredentialManager Instance
        {
            get
            {
                if (_userCredentialManager == null)
                {
                    lock (_padlock)
                    {
                        _userCredentialManager ??= new UserCredentialManager();
                    }
                }
                return _userCredentialManager;
            }
        }

        readonly IUserCredentialSet _userCredentialSet = new UserCredentialSet();

        public List<UserCredential> GetUserCredentials()
        {
            return _userCredentialSet.RetrieveUsersCredential();
        } 

        public void AddUserCredential(UserCredential userCredential)
        {
            _userCredentialSet.AddUserCredential(userCredential);
        }

        public void RemoveUserCredential(UserCredential userCredential)
        {
            _userCredentialSet.RemoveUserCredential(userCredential);
        }

        internal UserCredential GetUserCredential(string userId)
        {
            return _userCredentialSet.RetrieveUsersCredential().Single(credential => credential.UserId == userId);
        }
    }
}
