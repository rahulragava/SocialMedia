using SocialMedia.Model.EntityModel;
using SocialMedia.Model.EntityModel.EnumTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Model.BusinessModel
{
    public class CommentBobj
    {
        public int Id { get; set; }
        public int CommentedBy { get; set; }
        public DateTime CommentedAt { get; set; }
        public string? Content { get; set; }
        public CommentedOnType CommentedOn { get; set; }
        public int PostId { get; set; }
        public int Depth { get; set; }
        public int? ParentCommentId { get; set; }
        public List<Reaction>? Reactions { get; set; }
    }
}
