using SocialMedia.Manager;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;
using SocialMedia.View;



namespace SocialMedia.Controller.ControllerHelper
{
    public class Verification
    {
        UserCredentialManager _userCredentialManager = UserCredentialManager.Instance;
        UserManager _userManager = UserManager.Instance;
        
        public UserBObj VerifyUser(string userIdentifyingValue, string userPassword, SignInView signInPage)
        {
            var userCredentials = _userCredentialManager.GetUserCredentials();
            try
            {
                var userToBeVerified = _userManager.GetUserBObjWithoutId(userIdentifyingValue);
                UserCredential userCredential;
                if (userToBeVerified != null)
                    userCredential = _userCredentialManager.GetUserCredential(userToBeVerified.Id);
                else
                {
                    userCredential = null;
                }

                if (userCredential != null && userCredential.Password == userPassword)
                {
                    var user = _userManager.GetUserBObj(userCredential.UserId);
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
