using SocialMedia.Controller.ControllerHelper;
using SocialMedia.Model.BusinessModel;
using SocialMedia.View;

namespace SocialMedia.Controller
{
    public class ApplicationController
    {
        readonly WelcomePage _welcomePage = new WelcomePage();
        readonly SignInView _signInPage = new SignInView();
        readonly HomePage _homePage = new HomePage();

        HomePageController _homePageController;
        
        public void StartApplication()
        {
            _welcomePage.InitialPage();            
            Verification verification = new Verification();
            int choice = InputHelper.UserInputChoice(2);
            UserBobj user;
            switch (choice)
            {
                case 1:  //sign-in
                    (string userIdentityValue, string Password) = _signInPage.SignInPage();
                    user = verification.VerifyUser(userIdentityValue, Password, _signInPage);
                    Action startApplication = StartApplication;
                    if (user != null)
                    {
                        if (user.UserName != null)
                        {
                            _signInPage.SuccessLoggedInMessage(user.UserName);
                        }
                        _homePageController = new HomePageController(_homePage, user, startApplication);
                        _homePageController.InitiateHomePageController();
                    }
                    else
                    {
                        StartApplication();
                    }
                    break;
                case 2:  //exit
                    break;
            }

        }
    }
}
