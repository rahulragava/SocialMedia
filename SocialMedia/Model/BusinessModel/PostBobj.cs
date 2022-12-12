using SocialMedia.Model.EntityModel;

namespace SocialMedia.Model.BusinessModel
{
    public abstract class PostBObj : Post
    {
        public List<CommentBObj> Comments { get; set; }
        public List<Reaction> Reactions { get; set; }
    }
}
