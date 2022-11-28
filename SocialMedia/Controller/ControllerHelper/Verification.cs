using SocialMedia.Manager;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;
using SocialMedia.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SocialMedia.Controller.ControllerHelper
{
    public class Verification
    {
        UserCredentialManager _userCredentialManager = UserCredentialManager.GetUserCredentialManager();
        UserManager _userManager = UserManager.GetUserManager();
        
        public UserBobj VerifyUser(string userIdentifyingValue, string userPassword, SignInView signInPage)
        {
            var userCredentials = _userCredentialManager.GetUserCredentials();
            try
            {
                var userCredential = userCredentials.FirstOrDefault(userCredential => (userCredential.UserName == userIdentifyingValue) || (userCredential.MailId == userIdentifyingValue) || (userCredential.PhoneNumber == userIdentifyingValue));
                if (userCredential != null && userCredential.Password == userPassword)
                {
                    var user = _userManager.GetUserBobj(userCredential.UserId);
                    if (user != null)
                        return user;
                }
                throw new Exception();
            }
            catch (Exception)
            {
                (userIdentifyingValue, userPassword, bool GetBack) = signInPage.InvalidUser();
                if (!GetBack && userIdentifyingValue != null && userPassword != null)
                {
                    return VerifyUser(userIdentifyingValue, userPassword, signInPage);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
