using SocialMedia.Controller.ControllerHelper;
using SocialMedia.Model.BusinessModel;
using SocialMedia.View;

namespace SocialMedia.Controller
{
    public sealed class ApplicationController
    {
        private static ApplicationController _applicationInstance = new ApplicationController();
        private readonly static object _padLock = new object();
        readonly WelcomePage _welcomePage = new WelcomePage();
        readonly SignInView _signInPage = new SignInView();
        readonly HomePage _homePage = new HomePage();
        public static UserBObj user;


        HomePageController _homePageController;

        ApplicationController()
        {
        }

        public static ApplicationController Instance
        {
            get
            {
                if(_applicationInstance == null)
                {
                    lock (_padLock)
                    {
                        if(_applicationInstance == null)
                        {
                            _applicationInstance = new ApplicationController();
                        }
                    }
                }
                return _applicationInstance;
            }
        }
        public void StartApplication()
        {
            _welcomePage.InitialPage();            
            Verification verification = new Verification();
            int choice = InputHelper.UserInputChoice(2);
            //UserBObj user;

            switch (choice)
            {
                case 1:  //sign-in
                    (string userIdentityValue, string Password) = _signInPage.SignInPage();
                    user = verification.VerifyUser(userIdentityValue, Password, _signInPage);
                    Action startApplication = StartApplication;
                    if (user != null)
                    {
                        _signInPage.SuccessLoggedInMessage(user.UserName);
                        _homePageController = HomePageController.GetInstance;
                        _homePageController.InitiateHomePageController(startApplication);
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
