using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataSet.DataSetInterface
{
    public interface IFollowerFollowingSet
    {
        void AddFollowerFollowing(Follower followerFollowing);
        void RemoveFollowerFollowing(Follower followerFollowing);
        List<Follower> GetFollowerFollowingList();

    }
}
