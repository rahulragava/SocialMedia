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
    public sealed class CommentManager
    {

        private static CommentManager? commentManager = null;
        private static readonly object padlock = new object();

        CommentManager()
        {
        }

        public static CommentManager GetCommentManager()
        {
            if (commentManager == null)
            {
                lock (padlock)
                {
                    if (commentManager == null)
                    {
                        commentManager = new CommentManager();
                    }
                }
            }
            return commentManager;
        }

        ICommentSet commentSet = new CommentSet();
        ReactionManager reactionManager = ReactionManager.GetReactionManager();

        public List<CommentBobj> GetCommentBobjs()
        {
            List<CommentBobj> commentBobjs = new List<CommentBobj>();
            List<Comment> comments = commentSet.GetCommentList();
            List<Reaction> reactions = reactionManager.GetReaction();
            CommentBobj commentBobj;

            for (int i = 0; i < comments.Count; i++)
            {
                var commentReactions = reactions.Where((reaction)
                    => (reaction.ReactionOnType == ReactedOnType.PollPost) && (reaction.ReactionOnId == comments[i].Id)).ToList();

                commentBobj = new CommentBobj();
                commentBobj.Id = comments[i].Id;
                commentBobj.CommentedOn = comments[i].CommentedOn;
                commentBobj.PostId = comments[i].PostId;
                commentBobj.ParentCommentId = comments[i].ParentCommentId;
                commentBobj.CommentedBy = comments[i].CommentedBy;
                commentBobj.CommentedAt = comments[i].CommentedAt;
                commentBobj.Content = comments[i].Content;
                commentBobj.Reactions = reactions;
                
                
                commentBobjs.Add(commentBobj);
            }

            return commentBobjs;
        }

        
        public void AddComment(CommentBobj comment)
        {
            commentSet.AddComment(ConvertCommentBobjToEntity(comment));
        }

        public void RemoveComment(CommentBobj comment)
        {
            if (comment.Reactions != null && comment.Reactions.Count > 0)
                reactionManager.RemoveReactions(comment.Reactions);
            var entityComment = ConvertCommentBobjToEntity(comment);
            commentSet.RemoveComment(entityComment);
        }

        private Comment ConvertCommentBobjToEntity(CommentBobj commentBobj)
        {
            Comment comment = new Comment();
            comment.Id = commentBobj.Id;   
            comment.ParentCommentId= commentBobj.ParentCommentId;
            comment.CommentedBy = commentBobj.CommentedBy;
            comment.CommentedOn = commentBobj.CommentedOn;
            comment.CommentedAt = commentBobj.CommentedAt;
            comment.Content = commentBobj.Content;
            comment.PostId = commentBobj.PostId;

            return comment;

        }

        public int GetLastCommentId()
        {
            var commentSets = commentSet.GetCommentList();
            if (commentSets.Count > 0)
            {
                return commentSets[commentSets.Count - 1].Id;
            }
            else
            {
                return 0;
            }
        }
        internal void RemoveComments(List<CommentBobj> comments)
        {
            foreach (var comment in comments)
            {
                RemoveComment(comment);
            }
        }

    }
}
