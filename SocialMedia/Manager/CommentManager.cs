using SocialMedia.Constant;
using SocialMedia.DataSet;
using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;


namespace SocialMedia.Manager
{
    public sealed class CommentManager
    {

        private static CommentManager _commentManager = null;
        private static readonly object _padlock = new object();

        CommentManager()
        {
        }

        public static CommentManager Instance
        {
            get
            {
                if (_commentManager == null)
                {
                    lock (_padlock)
                    {
                        if (_commentManager == null)
                        {
                            _commentManager = new CommentManager();
                        }
                    }
                }
                return _commentManager;
            }
        }

        readonly ICommentSet _commentSet = new CommentSet();
        readonly ReactionManager _reactionManager = ReactionManager.Instance;

        public List<CommentBObj> GetCommentBObjs()
        {
            List<CommentBObj> commentBObjs = new List<CommentBObj>();
            List<Comment> comments = _commentSet.GetCommentList();
            List<Reaction> reactions = _reactionManager.GetReaction();
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
                
                
                commentBObjs.Add(commentBobj);
            }

            return commentBObjs;
        }

        
        public void AddComment(CommentBObj comment)
        {
            _commentSet.AddComment(ConvertCommentBObjToEntity(comment));
        }

        public void RemoveComment(CommentBObj comment)
        {
            if (comment.Reactions != null && comment.Reactions.Count > 0)
                _reactionManager.RemoveReactions(comment.Reactions);
            var entityComment = ConvertCommentBObjToEntity(comment);
            _commentSet.RemoveComment(entityComment);
        }

        private Comment ConvertCommentBObjToEntity(CommentBObj commentBobj)
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

        public void RemoveComments(List<CommentBObj> comments)
        {
            foreach (var comment in comments)
            {
                RemoveComment(comment);
            }
        }

    }
}
