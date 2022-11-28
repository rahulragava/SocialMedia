using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;
using SocialMedia.View;

namespace SocialMedia.Controller
{
    public class HomePageController
    {
        SearchController searchController;
        PostController postController;
        UserBobj _user;
        private HomePage _homePage;
        Action _BackToApplicationController;

        public HomePageController(HomePage homePage, UserBobj user, Action BackToApplicationController)
        {
            _homePage = homePage;
            _user = user;
            _BackToApplicationController = BackToApplicationController;
        }
        public void InitiateHomePageController()
        {
            var userHomePageChoice = _homePage.InitiateHomePage();

            Action initiateHomePageController = InitiateHomePageController;

            switch (userHomePageChoice)
            {
                case 1: // search
                    searchController = new SearchController(_user, initiateHomePageController);
                    searchController.InitiateSearchController();
                    break;
                case 2: //Post
                    postController = new PostController(_user, initiateHomePageController);
                    postController.InitiatePostController();
                    break;
                case 3: // exit
                    _BackToApplicationController(); // use delegate
                    break;
            }
        }
        
    }
}
