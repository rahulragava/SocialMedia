using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.EntityModel;

namespace SocialMedia.DataSet
{
    public class CommentSet : ICommentSet
    {
        private readonly List<Comment> _comments = new();

        public void AddComment(Comment comment)
        {
            if(comment != null)
            {
                _comments.Add(comment);
            }
        }

        public List<Comment> GetCommentList()
        {
            return _comments;
        }

        public void RemoveComment(Comment comment)
        {
            if(comment != null)
            {
                _comments.Remove(comment);
            }   
        }
      
    }
}
