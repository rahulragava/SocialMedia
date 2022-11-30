using SocialMedia.Constant;
using SocialMedia.DataSet;
using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;
using System.Xml.Linq;

namespace SocialMedia.Manager
{
    public sealed class TextPostManager
    {
        private static TextPostManager? textPostManager = null;
        private static readonly object padlock = new object();

        TextPostManager()
        {
        }

        public static TextPostManager Instance
        {
            get
            {
                if (textPostManager == null)
                {
                    lock (padlock)
                    {
                        if (textPostManager == null)
                        {
                            textPostManager = new TextPostManager();
                        }
                    }
                }
                return textPostManager;
            }
        }

        ITextPostSet textPostSet = new TextPostSet();

        ReactionManager reactionManager = ReactionManager.Instance;
        CommentManager commentManager = CommentManager.Instance;

        public List<TextPostBObj> GetTextPostBobjs()
        {
            List<TextPostBObj> textPostBobjs = new List<TextPostBObj>();
            List<Reaction> reactions = reactionManager.GetReaction();
            List<CommentBObj> commentBobjs = commentManager.GetCommentBobjs();
            List<TextPost> textPosts = textPostSet.RetrieveTextPostList();
            TextPostBObj textPostBobj;

            for (int i = 0; i < textPosts.Count; i++)
            {
                textPostBobj = new TextPostBObj();
                var textPostCommentBobjs = commentBobjs.Where((commentBobj) => commentBobj.PostId == textPosts[i].Id).ToList();
                List<CommentBObj> sortedCommentBobjs = GetSortedComments(textPostCommentBobjs);

                var textPostReactionBobjs = reactions.Where((reaction) => (reaction.ReactionOnId == textPosts[i].Id)).ToList();

                textPostBobj.Id = textPosts[i].Id;
                textPostBobj.Title = textPosts[i].Title;
                textPostBobj.PostedBy = textPosts[i].PostedBy;
                textPostBobj.Content = textPosts[i].Content;
                textPostBobj.CreatedAt = textPosts[i].CreatedAt;
                textPostBobj.LastModifiedAt = textPosts[i].LastModifiedAt;
                textPostBobj.Comments = sortedCommentBobjs;
                textPostBobj.Reactions = textPostReactionBobjs;

                textPostBobjs.Add(textPostBobj);
            }

            return textPostBobjs;
        }

        public List<CommentBObj> GetSortedComments(List<CommentBObj> textPostCommentBobjs)
        {
            List<CommentBObj> comments = textPostCommentBobjs;
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

        
        public void AddTextPost(TextPostBObj textPostBobj)
        {
            TextPost textPost = new TextPost();
            textPost.Id = textPostBobj.Id;
            textPost.Title = textPostBobj.Title;
            textPost.PostedBy = textPostBobj.PostedBy;
            textPost.CreatedAt = textPostBobj.CreatedAt;
            textPost.LastModifiedAt = textPostBobj.LastModifiedAt;
            textPost.Content = textPostBobj.Content;
            textPostSet.AddTextPost(textPost);
        }

        public TextPostBObj GetTextPostBObj(string postId)
        {
            var textPost = textPostSet.RetrieveTextPostList().Single(textPost => textPost.Id == postId);
            var comments = commentManager.GetCommentBobjs().Where(comment => comment.PostId == postId).ToList();
            var reactions = reactionManager.GetReaction().Where(reaction => reaction.ReactionOnId == postId).ToList();
            var textPostBObj = convertEntityToBObj(textPost, comments, reactions);

            return textPostBObj;
        }

        private TextPostBObj convertEntityToBObj(TextPost textPost, List<CommentBObj> comments, List<Reaction> reactions)
        {
            var textPostBObj = new TextPostBObj();
            textPost.Id = textPost.Id;
            textPostBObj.Title = textPost.Title;
            textPostBObj.PostedBy = textPost.PostedBy;
            textPostBObj.Content = textPost.Content;
            textPostBObj.CreatedAt = textPost.CreatedAt;
            textPostBObj.LastModifiedAt = textPost.LastModifiedAt;
            textPostBObj.Comments = comments;
            textPostBObj.Reactions = reactions;

            return new TextPostBObj();
        }

        public void EditTextPost(TextPostBObj textPostBobj)
        {
            int textPostAt = GetTextPostBobjs().FindIndex(textPost => textPost.Id == textPostBobj.Id);
            var textPost = ConvertBobjToEntityModel(textPostBobj);
            textPostSet.UpdatePost(textPostAt, textPost);
        }

        private TextPost ConvertBobjToEntityModel(TextPostBObj  textPostBobj)
        {
            TextPost textPost = new TextPost();

            textPost.Id = textPostBobj.Id;
            textPost.PostedBy = textPostBobj.PostedBy;
            textPost.Title = textPostBobj.Title;
            textPost.Content = textPostBobj.Content;
            textPost.CreatedAt = textPostBobj.CreatedAt;
            textPost.LastModifiedAt = textPostBobj.LastModifiedAt;

            return textPost;

        }

        public void RemoveTextPost(TextPostBObj textPostBobj)
        {

            var textPost = ConvertBobjToEntityModel(textPostBobj);
            if(textPost != null)
            {
                textPostSet.RemoveTextPost(textPost);
                if (textPostBobj.Reactions != null)
                    reactionManager.RemoveReactions(textPostBobj.Reactions);
                if (textPostBobj.Comments != null)
                    commentManager.RemoveComments(textPostBobj.Comments);
            }
        }
    }
}
