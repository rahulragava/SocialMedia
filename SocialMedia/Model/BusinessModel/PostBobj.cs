using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Model.BusinessModel
{
    public class PostBobj
    {
        public int Id { get; set; }
        public int PostedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string? Title { get; set; }
        
        public List<CommentBobj>? Comments;

        public List<Reaction>? Reactions;
    }
}
