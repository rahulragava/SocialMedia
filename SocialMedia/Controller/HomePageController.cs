using SocialMedia.View;

namespace SocialMedia.Controller
{
    public class HomePageController
    {

        private static readonly object _padlock = new object();
        private static HomePageController _instance;
        SearchController _searchController;
        PostController _postController;
        private HomePage _homePage;
        Action _backToApplicationController;

        HomePageController()
        {
        }

        public static HomePageController GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_padlock)
                    {
                        _instance ??= new HomePageController();
                    }
                }
                return _instance;
            }
        }
        
        public void InitiateHomePageController(Action backToApplicationController)
        {
            _homePage = new HomePage();
            _backToApplicationController = backToApplicationController;
            HomePageControllerInteraction();
        }

        public void HomePageControllerInteraction()
        {
            var userHomePageChoice = _homePage.InitiateHomePage();

            var initiateHomePageController = HomePageControllerInteraction;

            switch (userHomePageChoice)
            {
                case 1: // search
                    _searchController = SearchController.GetInstance;
                    _searchController.InitiateSearchController(initiateHomePageController);
                    break;
                case 2: //Post
                    _postController = PostController.GetInstance;
                    _postController.Initialize(initiateHomePageController);
                    break;
                case 3: // exit
                    _backToApplicationController(); // use delegate
                    break;
            }
        }
        
    }
}
