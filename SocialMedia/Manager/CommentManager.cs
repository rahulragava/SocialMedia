using SocialMedia.Constant;
using SocialMedia.DataSet;
using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;


namespace SocialMedia.Manager
{
    public sealed class CommentManager
    {

        private static CommentManager? commentManager = null;
        private static readonly object padlock = new object();

        CommentManager()
        {
        }

        public static CommentManager Instance
        {
            get
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
        }

        ICommentSet commentSet = new CommentSet();
        ReactionManager reactionManager = ReactionManager.Instance;

        public List<CommentBObj> GetCommentBobjs()
        {
            List<CommentBObj> commentBobjs = new List<CommentBObj>();
            List<Comment> comments = commentSet.GetCommentList();
            List<Reaction> reactions = reactionManager.GetReaction();
            CommentBObj commentBobj;

            for (int i = 0; i < comments.Count; i++)
            {
                var commentReactions = reactions.Where((reaction) => reaction.ReactionOnId == comments[i].Id).ToList();

                commentBobj = new CommentBObj();
                commentBobj.Id = comments[i].Id;
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

        
        public void AddComment(CommentBObj comment)
        {
            commentSet.AddComment(ConvertCommentBobjToEntity(comment));
        }

        public void RemoveComment(CommentBObj comment)
        {
            if (comment.Reactions != null && comment.Reactions.Count > 0)
                reactionManager.RemoveReactions(comment.Reactions);
            var entityComment = ConvertCommentBobjToEntity(comment);
            commentSet.RemoveComment(entityComment);
        }

        private Comment ConvertCommentBobjToEntity(CommentBObj commentBobj)
        {
            Comment comment = new Comment();
            comment.Id = commentBobj.Id;   
            comment.ParentCommentId= commentBobj.ParentCommentId;
            comment.CommentedBy = commentBobj.CommentedBy;
            comment.CommentedAt = commentBobj.CommentedAt;
            comment.Content = commentBobj.Content;
            comment.PostId = commentBobj.PostId;

            return comment;

        }

        //public string GetNewCommentId()
        //{
        //    var commentSets = commentSet.GetCommentList();
        //    if (commentSets.Count > 0)
        //    {
        //        var count = commentSets[commentSets.Count - 1].Id + 1 ;
        //        return $"CO{count}";
        //    }
        //    else
        //    {
        //        var count = 1;
        //        return $"CO{count}";
        //    }
        //}
        internal void RemoveComments(List<CommentBObj> comments)
        {
            foreach (var comment in comments)
            {
                RemoveComment(comment);
            }
        }

    }
}
