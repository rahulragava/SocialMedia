using SocialMedia.DataSet;
using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;
using SocialMedia.Model.EntityModel.EnumTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Manager
{
    public sealed class TextPostManager
    {
        private static TextPostManager? textPostManager = null;
        private static readonly object padlock = new object();

        TextPostManager()
        {
        }

        public static TextPostManager GetTextPostManager()
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

        ITextPostSet textPostSet = new TextPostSet();

        ReactionManager reactionManager = ReactionManager.GetReactionManager();
        CommentManager commentManager = CommentManager.GetCommentManager();

        public List<TextPostBobj> GetTextPostBobjs()
        {
            List<TextPostBobj> textPostBobjs = new List<TextPostBobj>();
            List<Reaction> reactions = reactionManager.GetReaction();
            List<CommentBobj> commentBobjs = commentManager.GetCommentBobjs();
            List<TextPost> textPosts = textPostSet.RetrieveTextPostList();
            TextPostBobj textPostBobj;

            for (int i = 0; i < textPosts.Count; i++)
            {
                textPostBobj = new TextPostBobj();
                var textPostCommentBobjs = commentBobjs.Where((commentBobj) => commentBobj.PostId == textPosts[i].Id).ToList();
                List<CommentBobj> sortedCommentBobjs = GetSortedComments(textPostCommentBobjs);

                var textPostReactionBobjs = reactions.Where((reaction)
                    => (reaction.ReactionOnType == ReactedOnType.TextPost) && (reaction.ReactionOnId == textPosts[i].Id)).ToList();

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

        public List<CommentBobj> GetSortedComments(List<CommentBobj> textPostCommentBobjs)
        {
            List<CommentBobj> comments = textPostCommentBobjs;
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

        public void AddTextPost(TextPostBobj textPostBobj)
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

        public int GetLastPostId()
        {
            var textPostSets = textPostSet.RetrieveTextPostList();
            if (textPostSets.Count > 0)
            {
                return textPostSets[textPostSets.Count - 1].Id;
            }
            else
            {
                return 0;
            }
        }

        public void EditTextPost(TextPostBobj textPostBobj)
        {
            int textPostAt = GetTextPostBobjs().FindIndex(textPost => textPost.Id == textPostBobj.Id);
            var textPost = ConvertBobjToEntityModel(textPostBobj);
            textPostSet.UpdatePost(textPostAt, textPost);
        }

        private TextPost ConvertBobjToEntityModel(TextPostBobj  textPostBobj)
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

        public void RemoveTextPost(TextPostBobj textPostBobj)
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
