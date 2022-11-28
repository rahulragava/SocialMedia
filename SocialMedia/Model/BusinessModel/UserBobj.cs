using SocialMedia.Model.EntityModel;
using SocialMedia.Model.EntityModel.EnumTypes;

namespace SocialMedia.Model.BusinessModel
{
    public class UserBobj
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public GenderType Gender { get; set; }
        public MaritalStatusType MaritalStatus { get; set; }
        public string? Place { get; set; }
        public string? Education { get; set; }
        public string? Occupation { get; set; }
        public List<PollPostBobj>? PollPosts;
        public List<TextPostBobj>? TextPosts;
    }
}
