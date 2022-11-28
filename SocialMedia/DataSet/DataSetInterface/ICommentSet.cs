using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataSet.DataSetInterface
{
    public interface ICommentSet
    {
        public void AddComment(Comment comment);
        public void RemoveComment(Comment comment);
        public List<Comment> GetCommentList();
    }
}
