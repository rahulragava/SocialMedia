using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataSet.DataSetInterface
{
    public interface ITextPostSet
    {
        void AddTextPost(TextPost textPost);
        void RemoveTextPost(TextPost textPost);
        List<TextPost> RetrieveTextPostList();
        void UpdatePost(int textPostAt, TextPost textPost);
    }
}

