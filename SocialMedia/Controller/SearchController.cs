using SocialMedia.Manager;
using SocialMedia.View;

namespace SocialMedia.Controller
{
    public class SearchController
    {
        private static readonly object _padlock = new object();
        private static SearchController _instance;
        
        SearchPage _searchPage;
        ProfileController _profileController;
        readonly UserManager _userManager = UserManager.Instance;

        Action _backToHomePageController;

        SearchController()
        {
        }

        public static SearchController GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_padlock)
                    {
                        _instance ??= new SearchController();
                    }
                }
                return _instance;
            }
        }

        public void InitiateSearchController(Action backToHomePageController)
        {
            _backToHomePageController = backToHomePageController;
            _searchPage = new SearchPage();
            SearchControllerInteraction();
        }

        public void SearchControllerInteraction()
        {
            var userChoice = _searchPage.InitiateSearchPageView();
            
            switch (userChoice)
            {
                case 1 : //Search By UserName
                    Search();
                    break;

                case 2: // back to HomePage
                    _backToHomePageController?.Invoke();
                    break;
            }
            
        }

        public void Search()
        {
            var userNames = _userManager.GetUserNames();
            var userName = _searchPage.SearchByName(userNames);
            
            var searchedUser = _userManager.GetUserBObjWithoutId(userName);
            Action initiateSearchController = SearchControllerInteraction;
            if(searchedUser != null)
            {
                _profileController = ProfileController.GetInstance;
                _profileController.Initialize(initiateSearchController, searchedUser.Id);
            }
            else
            {
                _searchPage.UserNotFoundMessage();
                SearchControllerInteraction();
            }
        
        }
    }
}
