using SocialMedia.Model.EntityModel;


namespace SocialMedia.DataSet.DataSetInterface
{
    public interface IPollPostSet
    {
        List<PollPost> RetrievePollPostList();
        void AddPost(PollPost PollPost);
        void RemovePost(PollPost pollPost);
        void UpdatePost(int postIndex,PollPost pollPost);
    }
}
