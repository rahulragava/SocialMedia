using SocialMedia.DataSet;
using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;
using SocialMedia.Model.EntityModel.EnumTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Manager
{
    public sealed class ReactionManager
    {
        IUserReactionSet userReactionSet = new UserReactionSet();

        private static ReactionManager? reactionManager = null;
        private static readonly object padlock = new object();

        ReactionManager()
        {
        }

        public static ReactionManager GetReactionManager()
        {
            if (reactionManager == null)
            {
                lock (padlock)
                {
                    if (reactionManager == null)
                    {
                        reactionManager = new ReactionManager();
                    }
                }
            }
            return reactionManager;
        }

        public List<Reaction> GetReaction()
        {
            return userReactionSet.RetrieveReactionList();
        }

        public void AddReaction(Reaction reaction)
        {
            userReactionSet.AddReactionSet(reaction);
        }

        public void RemoveReaction(Reaction reaction)
        {
            userReactionSet.RemoveReaction(reaction);
        }

        public Reaction ConvertCommentBobjToCommentEntityModel(CommentBobj commentBobj)
        {
            return new Reaction();
        }

        public int GetLastReactionId()
        {
            var reactions = userReactionSet.RetrieveReactionList();
            if (reactions.Count > 0)
            {
                return reactions[reactions.Count - 1].Id;
            }
            else
            {
                return 0;
            }
        }
        public void RemoveReactions(List<Reaction> reactions)
        {
            foreach (var reaction in reactions)
            {
                RemoveReaction(reaction);
            }
        }
    }
}
