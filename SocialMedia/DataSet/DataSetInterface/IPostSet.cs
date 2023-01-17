using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataSet.DataSetInterface
{
    public interface IPostSet
    {
        void AddPost(Post post);
        void RemovePost(Post post);
        List<Post> RetrievePostList();
        void UpdatePost(Post post);
    }
}
