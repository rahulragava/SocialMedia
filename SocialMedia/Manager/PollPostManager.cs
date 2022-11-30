using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.DataSet;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;
using SocialMedia.Constant;

namespace SocialMedia.Manager
{
    public sealed class PollPostManager
    {

        private static PollPostManager? pollPostManager = null;
        private static readonly object padlock = new object();

        PollPostManager()
        {
        }

        public static PollPostManager Instance
        {
            get
            {
                if (pollPostManager == null)
                {
                    lock (padlock)
                    {
                        if (pollPostManager == null)
                        {
                            pollPostManager = new PollPostManager();
                        }
                    }
                }
                return pollPostManager;
            }
        }

        IPollPostSet pollPostSet = new PollPostSet();
        PollChoiceManager pollChoiceManager = PollChoiceManager.Instance;
        

        ReactionManager reactionManager = ReactionManager.Instance;
        CommentManager commentManager = CommentManager.Instance;

        public List<PollPostBObj> GetPollPostBobjs()
        {
            List<PollPostBObj> pollPostBobjs = new List<PollPostBObj>();
            List<CommentBObj> commentBobjs = commentManager.GetCommentBobjs();
            List<Reaction> reactions= reactionManager.GetReaction();
            List<PollPost> pollPosts = pollPostSet.RetrievePollPostList();
            List<PollChoiceBObj> pollChoices = pollChoiceManager.GetPollChoices();
            PollPostBObj pollPostBobj;

            for(int i = 0; i < pollPosts.Count;i++)
            {
                pollPostBobj = new PollPostBObj();
                var pollPostCommentBobjs = commentBobjs
                    .Where((commentBobj) => commentBobj.PostId == pollPosts[i].Id)
                    .ToList();
                var sortedPollCommentBobjs = GetSortedComments(pollPostCommentBobjs);
                var pollPostReactions = reactions
                    .Where((reaction) => reaction.ReactionOnId == pollPosts[i].Id).ToList();
                var choices = pollChoices.Where((choice) => choice.PostId == pollPosts[i].Id).ToList();

                pollPostBobj.Id = pollPosts[i].Id;
                pollPostBobj.Title = pollPosts[i].Title;
                pollPostBobj.Question = pollPosts[i].Question;
                pollPostBobj.PostedBy = pollPosts[i].PostedBy;
                pollPostBobj.CreatedAt = pollPosts[i].CreatedAt;
                pollPostBobj.LastModifiedAt = pollPosts[i].LastModifiedAt;
                pollPostBobj.Comments = sortedPollCommentBobjs;
                pollPostBobj.Reactions = pollPostReactions;
                pollPostBobj.choices = choices;

                pollPostBobjs.Add(pollPostBobj);
            }
            
            return pollPostBobjs;
        }

        public List<CommentBObj> GetSortedComments(List<CommentBObj> pollPostCommentBobjs)
        {
            List<CommentBObj> comments = pollPostCommentBobjs;
            var sortedComments = new List<CommentBObj>();

            List<CommentBObj> levelZeroComments = comments.Where(x => x.ParentCommentId == null).OrderBy(x => x.CommentedAt).ToList();
            foreach (CommentBObj comment in levelZeroComments)
            {
                sortedComments.Add(comment);
                comment.Depth = 0;
                RecusiveSort(comment.Id, 1);
            }

            void RecusiveSort(string id, int depth)
            {
                List<CommentBObj> childComments = comments.Where(x => x.ParentCommentId == id).OrderBy(x => x.CommentedAt).ToList();

                foreach (CommentBObj comment in childComments)
                {
                    sortedComments.Add(comment);
                    comment.Depth = depth;
                    RecusiveSort(comment.Id, depth + 1);
                }

            }
            return sortedComments;
        }

        public void AddPollPost(PollPostBObj pollPostBobj)
        {
            if(pollPostBobj.choices != null)
            {
                pollChoiceManager.AddPollChoices(pollPostBobj.choices);
            }
            var pollPost = ConvertToEntityModel(pollPostBobj);
            pollPostSet.AddPost(pollPost);
        }
        public void EditPollPost(PollPostBObj pollPostBobj)
        {

            int pollPostAt = GetPollPostBobjs().FindIndex(pollPost => pollPost.Id == pollPostBobj.Id);
            var pollPost = ConvertToEntityModel(pollPostBobj);
            pollPostSet.UpdatePost(pollPostAt, pollPost);
        }

        public PollPostBObj GetPollPostBObj(string postId)
        {
            var pollPost = pollPostSet.RetrievePollPostList().Single(pollPost => pollPost.Id == postId);
            var comments = commentManager.GetCommentBobjs().Where(comment => comment.PostId == postId).ToList();
            var reactions = reactionManager.GetReaction().Where(reaction => reaction.ReactionOnId == postId).ToList();
            var choices = pollChoiceManager.GetPollChoices();
            var pollPostBObj = convertEntityToBObj(pollPost, comments, reactions, choices);

            return pollPostBObj;
        }

        private PollPostBObj convertEntityToBObj(PollPost pollPost, List<CommentBObj> comments, List<Reaction> reactions,List<PollChoiceBObj> pollChoices)
        {
            var pollPostBObj = new PollPostBObj();
            pollPostBObj.Id = pollPost.Id;
            pollPostBObj.Title = pollPost.Title;
            pollPostBObj.PostedBy = pollPost.PostedBy;
            pollPostBObj.Question = pollPost.Question;
            pollPostBObj.choices = pollChoices;
            pollPostBObj.CreatedAt = pollPost.CreatedAt;
            pollPostBObj.LastModifiedAt = pollPost.LastModifiedAt;
            pollPostBObj.Comments = comments;
            pollPostBObj.Reactions = reactions;

            return new PollPostBObj();
        }
        private PollPost ConvertToEntityModel(PollPostBObj pollPostBobj)
        {
            PollPost pollPost = new PollPost();

            pollPost.Id = pollPostBobj.Id;
            pollPost.PostedBy = pollPostBobj.PostedBy;
            pollPost.Question = pollPostBobj.Question;
            pollPost.Title = pollPostBobj.Title;
            pollPost.CreatedAt = pollPostBobj.CreatedAt;
            pollPost.LastModifiedAt = pollPostBobj.LastModifiedAt;
            
            return pollPost;

        }

        public void RemovePollPost(PollPostBObj pollPostBobj)
        {
            var pollPost = ConvertToEntityModel(pollPostBobj);
            pollPostSet.RemovePost(pollPost);
            if (pollPostBobj.Reactions != null && pollPostBobj.Reactions.Count > 0)
                reactionManager.RemoveReactions(pollPostBobj.Reactions);
            if (pollPostBobj.Comments != null)
                commentManager.RemoveComments(pollPostBobj.Comments);
            pollChoiceManager.RemovePollChoices(pollPostBobj.choices);
        }

    }
}
