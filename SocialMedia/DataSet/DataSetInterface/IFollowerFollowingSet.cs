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
        void AddFollowerFollowing(FollowerFollowing followerFollowing);
        void RemoveFollowerFollowing(FollowerFollowing followerFollowing);
        List<FollowerFollowing> GetFollowerFollowingList();

    }
}
