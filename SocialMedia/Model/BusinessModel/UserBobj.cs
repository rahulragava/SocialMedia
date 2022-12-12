using SocialMedia.Constant;
using SocialMedia.Model.EntityModel;

namespace SocialMedia.Model.BusinessModel
{
    public class UserBObj : User
    {
        public List<PollPostBObj> PollPosts { get; set; }
        public List<TextPostBObj> TextPosts { get; set; }
        public List<string> FollowersId { get; set; }
        public List<string> FollowingsId { get; set; }
    }
}
