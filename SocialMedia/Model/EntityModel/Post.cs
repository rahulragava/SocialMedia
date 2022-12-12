using SocialMedia.Constant;


namespace SocialMedia.Model.EntityModel
{
    public abstract class Post
    {
        public string Id { get; set; }
        public string PostedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
        public string Title { get; set; }
            
        public Post()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
