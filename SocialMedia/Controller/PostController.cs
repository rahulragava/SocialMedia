using SocialMedia.Model.BusinessModel;
using SocialMedia.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Controller
{
    public class PostController
    {
        UserBobj _user;
        readonly PostPage _postPage;
        PollPostController _pollPostController;
        TextPostController _textPostController;
        Action _BackToHomePageController;

        public PostController(UserBobj user, Action BackToHomePageController)
        {
            _user = user;
            _postPage = new PostPage();
            _BackToHomePageController = BackToHomePageController;
        }

        public void InitiatePostController()
        {
            var userChoice = _postPage.InitiatePostPage();
            Action initiatePostController = InitiatePostController;

            switch (userChoice)
            {
                case 1:
                    _pollPostController = new PollPostController(initiatePostController,_user);
                    _pollPostController.InitiatePollPostController();
                    break;
                case 2:
                    _textPostController = new TextPostController(initiatePostController, _user);
                    _textPostController.InitiateTextPostController();
                    break;
                case 3:
                    _BackToHomePageController?.Invoke();
                    break;
            }
        }
    }
}
