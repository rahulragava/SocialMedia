using SocialMedia.Manager;
using SocialMedia.Model.BusinessModel;
using SocialMedia.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Controller
{
    public class SearchController
    {
        SearchPage searchPage;
        UserBobj _user;
        ProfileController _profileController;

        Action _BackToHomePageController;
        

        public SearchController(UserBobj user, Action BackToHomePageController)
        {
            _user = user;
            _BackToHomePageController = BackToHomePageController;
            searchPage = new SearchPage();
        }

        public void InitiateSearchController()
        {
            var userChoice = searchPage.InitiateSearchPageView();
            
            switch (userChoice)
            {
                case 1 : //Search By UserName
                    Search();
                    break;

                case 2: // back to HomePage
                    _BackToHomePageController?.Invoke();
                    break;
            }
            
        }

        public void Search()
        {
            var userManager = UserManager.GetUserManager(); 
            var userName = searchPage.SearchByName();
            var searchedUser = userManager.GetUserBobj(userName);
            Action initiateSearchController = InitiateSearchController;
            if(searchedUser != null)
            {
                _profileController = new ProfileController(initiateSearchController, searchedUser, _user);
                _profileController.InitiateProfileController();
            }
            else
            {
                searchPage.UserNotFoundMessage();
                InitiateSearchController();
            }
        }
    }
}
