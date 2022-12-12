using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.EntityModel;

namespace SocialMedia.DataSet
{
    public class FollowerSet : IFollowerFollowingSet
    {
        private static readonly List<Follower> _followerSet = new();
        public void AddFollowerFollowing(Follower followerFollowing)
        {
            if(followerFollowing != null)
            {
                _followerSet.Add(followerFollowing);
            }
        }

        public List<Follower> GetFollowerFollowingList()
        {
           return _followerSet;
        }

        public void RemoveFollowerFollowing(Follower followerFollowing)
        {
            if (followerFollowing != null)
            {
                _followerSet.Remove(followerFollowing);
            }
        }
    }
}
