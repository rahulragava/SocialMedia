using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataSet.DataSetInterface
{
    public interface IUserReactionSet
    {
        void AddReactionSet(Reaction userReaction);
        void RemoveReaction(Reaction userReaction);
        List<Reaction> RetrieveReactionList();
    }
}
