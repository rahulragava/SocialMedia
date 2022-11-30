using SocialMedia.View;

namespace SocialMedia.Controller
{
    public class HomePageController
    {

        private static readonly object padlock = new object();
        private static HomePageController instance;
        SearchController searchController;
        PostController postController;
        private HomePage _homePage;
        Action _BackToApplicationController;

        HomePageController()
        {
        }

        public static HomePageController GetInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new HomePageController();
                        }
                    }
                }
                return instance;
            }
        }
        
        public void InitiateHomePageController(Action BackToApplicationController)
        {
            _homePage = new HomePage();
            _BackToApplicationController = BackToApplicationController;
            HomePageControllerInteraction();
        }

        public void HomePageControllerInteraction()
        {
            var userHomePageChoice = _homePage.InitiateHomePage();

            Action initiateHomePageController = HomePageControllerInteraction;

            switch (userHomePageChoice)
            {
                case 1: // search
                    searchController = SearchController.GetInstance;
                    searchController.InitiateSearchController(initiateHomePageController);
                    break;
                case 2: //Post
                    postController = PostController.GetInstance;
                    postController.Initialize(initiateHomePageController);
                    break;
                case 3: // exit
                    _BackToApplicationController(); // use delegate
                    break;
            }
        }
        
    }
}
