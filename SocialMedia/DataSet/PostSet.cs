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

                _postSet.Remove(_postSet.First(postItem => postItem.Id == post.Id));
                
            }
        }

        public List<Post> RetrievePostList()
        {
            return _postSet;
        }

        public void UpdatePost(Post post)
        {
            var postToBeUpdated = _postSet.Single(p => post.Id == p.Id);
            
            var index = _postSet.IndexOf(postToBeUpdated);
            if (post != null && (index >= 0 && index < _postSet.Count))
            {
                 _postSet[index] = post;
            }
        }
    }
}
