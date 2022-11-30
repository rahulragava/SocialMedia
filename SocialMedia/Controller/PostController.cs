using SocialMedia.View;


namespace SocialMedia.Controller
{
    public class PostController
    {
        private static readonly object _lock = new object();
        private static PostController _instance;
        PostPage _postPage;
        PollPostController _pollPostController;
        TextPostController _textPostController;
        Action _BackToHomePageController;

        private PostController()
        {

        }

        public static PostController GetInstance
        {
            get
            {
                if(_instance == null )
                {
                    lock (_lock)
                    {
                        if(_instance == null)
                        {
                            _instance = new PostController();
                        }
                    }
                }
                return _instance;
            }
        }

        public void Initialize(Action BackToHomePageController)
        {
            _BackToHomePageController = BackToHomePageController;
            _postPage = new PostPage();
            InitiatePostController();
        }
        

        public void InitiatePostController()
        {
            var userChoice = _postPage.InitiatePostPage();
            Action initiatePostController = InitiatePostController;

            switch (userChoice)
            {
                case 1:
                    _pollPostController = PollPostController.GetInstance;
                    _pollPostController.Initialize(initiatePostController);
                    break;
                case 2:
                    _textPostController = TextPostController.GetInstance;
                    _textPostController.Initialize(initiatePostController);
                   
                    break;
                case 3:
                    _BackToHomePageController?.Invoke();
                    break;
            }
        }
    }
}
