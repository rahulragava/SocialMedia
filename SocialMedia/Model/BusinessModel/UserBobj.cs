using SocialMedia.Constant;
using SocialMedia.Model.EntityModel;

namespace SocialMedia.Model.BusinessModel
{
    public class UserBObj : User
    {
        public List<PollPostBObj>? PollPosts;
        public List<TextPostBObj>? TextPosts;
        public List<string> FollowersId;
        public List<string> FollowingsId;
    }
}
