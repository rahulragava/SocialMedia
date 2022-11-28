using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.EntityModel;

namespace SocialMedia.DataSet
{
    public class PollPostSet : IPollPostSet
    {
        private static List<PollPost> _pollPosts = new List<PollPost>();
        public void AddPost(PollPost pollPost)
        {
            if (pollPost != null)
                _pollPosts.Add(pollPost);
        }

        public void RemovePost(PollPost pollPost)
        {
            
            if(pollPost != null)
                _pollPosts.Remove(pollPost);
        }

        public List<PollPost> RetrievePollPostList()
        {
            return _pollPosts;
        }

        public void UpdatePost(int index, PollPost pollPost)
        {
            if(pollPost != null)
                _pollPosts[index] = pollPost;
        }
    }
}
