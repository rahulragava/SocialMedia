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
        private static readonly object padlock = new object();
        private static SearchController instance;
        
        SearchPage searchPage;
        ProfileController _profileController;
        UserManager _userManager = UserManager.Instance;

        Action _BackToHomePageController;

        SearchController()
        {
        }

        public static SearchController GetInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new SearchController();
                        }
                    }
                }
                return instance;
            }
        }

        public void InitiateSearchController(Action BackToHomePageController)
        {
            _BackToHomePageController = BackToHomePageController;
            searchPage = new SearchPage();
            SearchControllerInteraction();
        }

        public void SearchControllerInteraction()
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
            var userNames = _userManager.GetUserNames();
            var userName = searchPage.SearchByName(userNames);
            
            var searchedUser = _userManager.GetUserBObjWithoutId(userName);
            Action initiateSearchController = SearchControllerInteraction;
            if(searchedUser != null)
            {
                _profileController = ProfileController.GetInstance;
                _profileController.Initialize(initiateSearchController, searchedUser.Id);
            }
            else
            {
                searchPage.UserNotFoundMessage();
                SearchControllerInteraction();
            }
        
        }
    }
}
