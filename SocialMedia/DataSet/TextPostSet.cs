using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataSet
{
    public class TextPostSet : ITextPostSet
    {
        private static List<TextPost> _textPosts = new List<TextPost>();

        public void AddTextPost(TextPost textPost)
        {
            if(textPost != null)
                _textPosts.Add(textPost);
        }

        public void RemoveTextPost(TextPost textPost)
        {
            if (textPost != null)
                _textPosts.Remove(textPost);
        }

        public List<TextPost> RetrieveTextPostList()
        {
            return _textPosts;
        }

        public void UpdatePost(int index, TextPost textPost)
        {
            if (textPost != null)
                _textPosts[index] = textPost;
        }
    }
}
