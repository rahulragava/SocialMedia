using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataSet
{
    public class PostSet : IPostSet
    {
        private static readonly List<Post> _postSet = new();

        public void AddPost(Post post)
        {
            if (post != null)
            {
                _postSet.Add(post);
            }
        }

        public void RemovePost(Post post)
        {
            if (post != null)
            {
                _postSet.Remove(post);
            }
        }

        public List<Post> RetrievePostList()
        {
            return _postSet;
        }

        public void UpdatePost(int index, Post post)
        {
            if (post != null && (index >= 0 && index < _postSet.Count))
            {
                _postSet[index] = post;
            }
        }
    }
}
