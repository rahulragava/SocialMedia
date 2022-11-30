using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.DataSet;
using SocialMedia.Model.EntityModel;

namespace SocialMedia.Manager
{
    public sealed class UserCredentialManager
    {
        private static UserCredentialManager? userCredentialManager ;
        private static readonly object padlock = new object();

        UserCredentialManager()
        {
        }

        public static UserCredentialManager Instance
        {
            get
            {
                if (userCredentialManager == null)
                {
                    lock (padlock)
                    {
                        if (userCredentialManager == null)
                        {
                            userCredentialManager = new UserCredentialManager();
                        }
                    }
                }
                return userCredentialManager;
            }
        }

        IUserCredentialSet userCredentialSet = new UserCredentialSet();

        public List<UserCredential> GetUserCredentials()
        {
            return userCredentialSet.RetrieveUsersCredential();
        } 

        public void AddUserCredential(UserCredential userCredential)
        {
            userCredentialSet.AddUserCredential(userCredential);
        }

        public void RemoveUserCredential(UserCredential userCredential)
        {
            userCredentialSet.RemoveUserCredential(userCredential);
        }

        internal UserCredential GetUserCredential(string userId)
        {
            return userCredentialSet.RetrieveUsersCredential().Single(credential => credential.UserId == userId);
        }
    }
}
