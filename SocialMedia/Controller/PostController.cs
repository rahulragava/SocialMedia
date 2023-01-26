using SocialMedia.View;


namespace SocialMedia.Controller
{
    public class PostController
    {
        private static readonly object _lock = new object();
        private static PostController _instance;
        PostPage _postPage;
        //PollPostController _pollPostController;
        GenericPostController _genericPostController;
        //TextPostController _textPostController;
        LabelController _labelController;
        Action _backToHomePageController;

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
                        _instance ??= new PostController();
                    }
                }
                return _instance;
            }
        }

        public void Initialize(Action backToHomePageController)
        {
            _backToHomePageController = backToHomePageController;
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
                    _genericPostController = GenericPostController.GetInstance;
                    _genericPostController.Initialize(initiatePostController);
                    break;

                case 2:
                    _labelController = LabelController.Instance;
                    _labelController.Initialize(initiatePostController);
                    break;

                case 3:
                    _backToHomePageController?.Invoke();
                    break;
            }
        }
    }
}
