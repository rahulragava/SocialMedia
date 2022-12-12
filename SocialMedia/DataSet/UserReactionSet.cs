using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataSet
{
    public class UserReactionSet : IUserReactionSet
    {
        private static readonly List<Reaction> _userReactions = new();
        public void AddReactionSet(Reaction userReaction)
        {
            if (userReaction != null)
                _userReactions.Add(userReaction);
        }

        public void RemoveReaction(Reaction userReaction)
        {
            if(userReaction != null)
                _userReactions.Remove(userReaction);
        }

        public List<Reaction> RetrieveReactionList()
        {
            return _userReactions;
        }

    }
}
