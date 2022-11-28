using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.Model.EntityModel.EnumTypes;

namespace SocialMedia.Model.EntityModel
{
    public class Comment
    {
        public int Id { get; set; }
        public int CommentedBy { get; set; }   
        public DateTime CommentedAt { get; set; }
        public string? Content { get; set; }
        public CommentedOnType CommentedOn { get; set; }
        public int PostId { get; set; }
        public int? ParentCommentId { get; set; }
    }
}
