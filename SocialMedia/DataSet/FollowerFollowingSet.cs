using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataSet
{
    public class FollowerFollowingSet : IFollowerFollowingSet
    {
        private static List<FollowerFollowing> _followerFollowingSet = new List<FollowerFollowing>();
        public void AddFollowerFollowing(FollowerFollowing followerFollowing)
        {
            if(followerFollowing != null)
            {
                _followerFollowingSet.Add(followerFollowing);
            }
        }

        public List<FollowerFollowing> GetFollowerFollowingList()
        {
           return _followerFollowingSet;
        }

        public void RemoveFollowerFollowing(FollowerFollowing followerFollowing)
        {
            if (followerFollowing != null)
            {
                _followerFollowingSet.Remove(followerFollowing);
            }
        }
    }
}
