using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataSet
{
    public class CommentSet : ICommentSet
    {
        private List<Comment> _comments = new List<Comment>();

        public void AddComment(Comment comment)
        {
            if(comment != null)
                _comments.Add(comment);
        }

        public List<Comment> GetCommentList()
        {
            return _comments;
        }

        public void RemoveComment(Comment comment)
        {
            if(comment != null)
                _comments.Remove(comment);
        }
      
    }
}
