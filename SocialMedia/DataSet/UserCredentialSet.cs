using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataSet
{
    public class UserCredentialSet : IUserCredentialSet
    {
        private static List<UserCredential> _userCredentials = new List<UserCredential>() { };

        public List<UserCredential> RetrieveUsersCredential()
        {
            return _userCredentials.DistinctBy(userCredential => userCredential.UserId).ToList();
        }

        public void AddUserCredential(UserCredential userCredential)
        {
            if (userCredential != null)
                _userCredentials.Add(userCredential);
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
