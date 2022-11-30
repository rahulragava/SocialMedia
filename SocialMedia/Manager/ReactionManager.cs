using SocialMedia.DataSet;
using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;


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

        public static ReactionManager Instance
        {
            get
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

        public Reaction ConvertCommentBobjToCommentEntityModel(CommentBObj commentBobj)
        {
            return new Reaction();
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
