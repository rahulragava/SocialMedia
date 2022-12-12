using SocialMedia.Constant;

namespace SocialMedia.Model.EntityModel
{
    public class Comment
    {
        public string Id { get; set; }
        public string CommentedBy { get; set; }   
        public DateTime CommentedAt { get; set; }
        public string Content { get; set; }
        public string PostId { get; set; }
        public string ParentCommentId { get; set; }

        public Comment()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
