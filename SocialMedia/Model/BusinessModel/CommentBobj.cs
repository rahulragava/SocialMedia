using SocialMedia.Constant;
using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Model.BusinessModel
{
    public class CommentBObj: Comment
    {
        public int Depth { get; set; }
        public List<Reaction>? Reactions { get; set; }
    }
}
