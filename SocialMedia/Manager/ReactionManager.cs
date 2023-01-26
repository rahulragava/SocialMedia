using SocialMedia.DataSet;
using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;


namespace SocialMedia.Manager
{
    public sealed class ReactionManager
    {
        readonly IUserReactionSet _userReactionSet = new UserReactionSet();

        private static ReactionManager _reactionManager = null;
        private static readonly object _padlock = new object();

        ReactionManager()
        {
        }

        public static ReactionManager Instance
        {
            get
            {
                if (_reactionManager == null)
                {
                    lock (_padlock)
                    {
                        _reactionManager ??= new ReactionManager();
                    }
                }
                return _reactionManager;
            }
        }

        public List<Reaction> GetReaction()
        {
            return _userReactionSet.RetrieveReactionList();
        }

        public void AddReaction(Reaction reaction)
        {
            _userReactionSet.AddReactionSet(reaction);
        }

        public void RemoveReaction(Reaction reaction)
        {
            _userReactionSet.RemoveReaction(reaction);
        }


        public void RemoveReactions(List<Reaction> reactions)
        {
            var Reactions = reactions;
            if (reactions.Count <= 0 || !(reactions.Any())) return;

            while (true)
            {
                foreach (var reaction in Reactions)
                {
                    RemoveReaction(reaction);
                    break;
                }

                if (Reactions.Count == 0) break;
            }
            //foreach (var reaction in Reactions)
            //{
            //    RemoveReaction(reaction);
            //}
        }
    }
}
