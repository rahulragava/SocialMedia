using SocialMedia.Constant;

namespace SocialMedia.Model.EntityModel
{
    public class Reaction
    {
        public string Id { get;set; }
        public string ReactedBy { get; set; }
        public string ReactionOnId { get; set; }
        public ReactionType reactionType { get; set; }  
        //public ReactedOnType ReactionOnType { get; set; }

        public Reaction()
        {
            Id = Guid.NewGuid().ToString();  
        }
    }
}
