using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.DataSet;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.Model.EntityModel.EnumTypes;
using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace SocialMedia.Manager
{
    public sealed class PollPostManager
    {

        private static PollPostManager? pollPostManager = null;
        private static readonly object padlock = new object();

        PollPostManager()
        {
        }

        public static PollPostManager GetPollPostManager()
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
        IPollPostSet pollPostSet = new PollPostSet();
        PollChoiceManager pollChoiceManager = PollChoiceManager.GetPollPostManager();
        

        ReactionManager reactionManager = ReactionManager.GetReactionManager();
        CommentManager commentManager = CommentManager.GetCommentManager();

        public List<PollPostBobj> GetPollPostBobjs()
        {
            List<PollPostBobj> pollPostBobjs = new List<PollPostBobj>();
            List<CommentBobj> commentBobjs = commentManager.GetCommentBobjs();
            List<Reaction> reactions= reactionManager.GetReaction();
            List<PollPost> pollPosts = pollPostSet.RetrievePollPostList();
            List<PollChoiceBobj> pollChoices = pollChoiceManager.GetPollChoices();
            PollPostBobj pollPostBobj;

            for(int i = 0; i < pollPosts.Count;i++)
            {
                pollPostBobj = new PollPostBobj();
                var pollPostCommentBobjs = commentBobjs
                    .Where((commentBobj) => commentBobj.PostId == pollPosts[i].Id)
                    .ToList();
                var sortedPollCommentBobjs = GetSortedComments(pollPostCommentBobjs);
                var pollPostReactions = reactions
                    .Where((reaction) => (reaction.ReactionOnType == ReactedOnType.PollPost) && (reaction.ReactionOnId == pollPosts[i].Id))
                    .ToList();
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

        public List<CommentBobj> GetSortedComments(List<CommentBobj> pollPostCommentBobjs)
        {
            List<CommentBobj> comments = pollPostCommentBobjs;
            var sortedComments = new List<CommentBobj>();

            List<CommentBobj> levelZeroComments = comments.Where(x => x.ParentCommentId == null).OrderBy(x => x.Id).ToList();
            foreach (CommentBobj comment in levelZeroComments)
            {
                sortedComments.Add(comment);
                comment.Depth = 0;
                RecusiveSort(comment.Id, 1);
            }

            void RecusiveSort(int id, int depth)
            {
                List<CommentBobj> childComments = comments.Where(x => x.ParentCommentId == id).OrderBy(x => x.Id).ToList();

                foreach (CommentBobj comment in childComments)
                {
                    sortedComments.Add(comment);
                    comment.Depth = depth;
                    RecusiveSort(comment.Id, depth + 1);
                }

            }
            return sortedComments;
        }

        public void AddPollPost(PollPostBobj pollPostBobj)
        {
            if(pollPostBobj.choices != null)
            {
                pollChoiceManager.AddPollChoices(pollPostBobj.choices);
            }
            var pollPost = ConvertToEntityModel(pollPostBobj);
            pollPostSet.AddPost(pollPost);
        }
        public void EditPollPost(PollPostBobj pollPostBobj)
        {

            int pollPostAt = GetPollPostBobjs().FindIndex(pollPost => pollPost.Id == pollPostBobj.Id);
            var pollPost = ConvertToEntityModel(pollPostBobj);
            pollPostSet.UpdatePost(pollPostAt, pollPost);
        }

        private PollPost ConvertToEntityModel(PollPostBobj pollPostBobj)
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

        public void RemovePollPost(PollPostBobj pollPostBobj)
        {
            var pollPost = ConvertToEntityModel(pollPostBobj);
            pollPostSet.RemovePost(pollPost);
            if (pollPostBobj.Reactions != null && pollPostBobj.Reactions.Count > 0)
                reactionManager.RemoveReactions(pollPostBobj.Reactions);
            if (pollPostBobj.Comments != null)
                commentManager.RemoveComments(pollPostBobj.Comments);
            pollChoiceManager.RemovePollChoices(pollPostBobj.choices);
        }

        public int GetLastPostId()
        {
            var pollPostSets = pollPostSet.RetrievePollPostList();
            if (pollPostSets.Count > 0)
            {
                return pollPostSets[pollPostSets.Count-1].Id;
            }
            else
            {
                return 0;
            }
        }
    }
}
