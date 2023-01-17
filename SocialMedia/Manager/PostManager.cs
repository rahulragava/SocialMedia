using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.DataSet;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;

namespace SocialMedia.Manager
{
    public class PostManager
    {
        private static PostManager _postManager = null;
        private static readonly object _padlock = new object();

        PostManager()
        {
        }

        public static PostManager Instance
        {
            get
            {
                if (_postManager == null)
                {
                    lock (_padlock)
                    {
                        if (_postManager == null)
                        {
                            _postManager = new PostManager();
                        }
                    }
                }
                return _postManager;
            }
        }

        readonly IPostSet _postSet = new PostSet();
        readonly PollChoiceManager _pollChoiceManager = PollChoiceManager.Instance;
        readonly ReactionManager _reactionManager = ReactionManager.Instance;
        readonly CommentManager _commentManager = CommentManager.Instance;



        public void AddPost(PostBObj postBObj)
        {
            Post post = ConvertBObjToEntityModel(postBObj);
            if (postBObj is TextPostBObj)
            {
                var textPost = post as TextPost;
                //_textPostSet.AddTextPost(textPost);
                _postSet.AddPost(textPost);
            }
            else if (postBObj is PollPostBObj)
            {
                var pollPost = post as PollPost;
                _postSet.AddPost(pollPost);
                //_pollPostSet.AddPost(pollPost);

                var originalPollPost = postBObj as PollPostBObj;
                if (originalPollPost.Choices != null)
                {
                    _pollChoiceManager.AddPollChoices(originalPollPost.Choices);
                }
            }
        }

        private Post ConvertBObjToEntityModel(PostBObj postBObj)
        {
            Post post;
            post = (postBObj is TextPostBObj) ? new TextPost() : new PollPost();

            post.Id = postBObj.Id;
            post.Title = postBObj.Title;
            post.PostedBy = postBObj.PostedBy;
            post.CreatedAt = postBObj.CreatedAt;
            post.LastModifiedAt = postBObj.LastModifiedAt;
            
            if (postBObj is TextPostBObj)
            {
                TextPost textPost = post as TextPost;
                var textPostBObj = postBObj as TextPostBObj;
                textPost.Content = textPostBObj.Content;
                
                return textPost;
            }
            else if (postBObj is PollPostBObj)
            {
                PollPost pollPost = post as PollPost;
                var pollPostBObj = postBObj as PollPostBObj;
                pollPost.Question = pollPostBObj.Question;

                return pollPost;
            }
            return post;
        }

        private PostBObj ConvertEntityToBObj(Post post, List<CommentBObj> comments, List<Reaction> reactions)
        {
            PostBObj postBObj;

            postBObj = post is TextPost ? new TextPostBObj() : new PollPostBObj();

            postBObj.Id = post.Id;
            postBObj.PostedBy = post.PostedBy;
            postBObj.Reactions = reactions;
            postBObj.Comments = comments;
            postBObj.Title = post.Title;
            postBObj.CreatedAt = post.CreatedAt;
            postBObj.LastModifiedAt = post.LastModifiedAt;

            if (postBObj is TextPostBObj)
            {
                TextPostBObj textPostBObj = postBObj as TextPostBObj;
                var textPost = post as TextPost;
                textPostBObj.Content = textPost.Content;

                return textPostBObj;
            }
            else if (postBObj is PollPostBObj)
            {
                var choices = _pollChoiceManager.GetPollChoices().Where(choice => choice.PostId == post.Id).ToList();
                var pollPostBObj = postBObj as PollPostBObj;
                var pollPost = post as PollPost;
                pollPostBObj.Question = pollPost.Question;
                pollPostBObj.Choices = choices;

                return pollPostBObj;
            }

            return postBObj;
        }

        public void RemovePost(PostBObj postBObj)
        {
            var post = ConvertBObjToEntityModel(postBObj);
            if (post is PollPost)
            {
                var pollPostBObj = postBObj as PollPostBObj;
                var pollPost = post as PollPost;
                _pollChoiceManager.RemovePollChoices(pollPostBObj.Choices);
                _postSet.RemovePost(pollPost);

            }
            else if (post is TextPost)
            {
                var textPost = post as TextPost;
                _postSet.RemovePost(textPost);
            }
            if (postBObj.Reactions != null && postBObj.Reactions.Any())
                _reactionManager.RemoveReactions(postBObj.Reactions);
            if (postBObj.Comments != null && postBObj.Comments.Any())
                _commentManager.RemoveComments(postBObj.Comments);
        }

        public void EditPost(PostBObj postBObj)
        {
            var editedPost = ConvertBObjToEntityModel(postBObj);
            _postSet.UpdatePost(editedPost);
        }

        public PostBObj GetPost(string postId)
        {
            var post = _postSet.RetrievePostList().Single(post => post.Id == postId);
            var comments = _commentManager.GetCommentBObjs().Where(comment => comment.PostId == postId).ToList();
            var reactions = _reactionManager.GetReaction().Where(reaction => reaction.ReactionOnId == postId).ToList();
            var choices = _pollChoiceManager.GetPollChoices().Where(choice => choice.PostId == postId).ToList();
            var postBObj = ConvertEntityToBObj(post, comments, reactions);
            
            return postBObj;
        }

        public string GetUserId(string postId)
        {
            var posts = _postSet.RetrievePostList();
            var userId = posts.Single(post => post.Id == postId).PostedBy;
            
            return userId;
        }

        public List<PostBObj> GetPostBObjs()
        {
            List<PostBObj> postBObjs = new List<PostBObj>();
            List<Reaction> reactions = _reactionManager.GetReaction();
            List<CommentBObj> commentBobjs = _commentManager.GetCommentBObjs();
            List<Post> posts = _postSet.RetrievePostList();            
            List<PollChoiceBObj> pollChoices = _pollChoiceManager.GetPollChoices();
            PostBObj postBObj;

            for (int i = 0; i < posts.Count; i++)
            {
                var postCommentBObjs = commentBobjs.Where((commentBobj) => commentBobj.PostId == posts[i].Id).ToList();
                List<CommentBObj> sortedCommentBobjs = GetSortedComments(postCommentBObjs);
                var postReactions = reactions.Where((reaction) => (reaction.ReactionOnId == posts[i].Id)).ToList();

                postBObj = (posts[i] is TextPost) ? new TextPostBObj() : new PollPostBObj();

                postBObj.Id = posts[i].Id;
                postBObj.Title = posts[i].Title;
                postBObj.PostedBy = posts[i].PostedBy;
                postBObj.CreatedAt = posts[i].CreatedAt;
                postBObj.LastModifiedAt = posts[i].LastModifiedAt;
                postBObj.Comments = sortedCommentBobjs;
                postBObj.Reactions = postReactions;

                if(postBObj is TextPostBObj)
                {
                    TextPostBObj textPostBObj = postBObj as TextPostBObj;
                    TextPost textPost = posts[i] as TextPost;

                    textPostBObj.Content = textPost.Content;
                    postBObjs.Add(textPostBObj);
                    
                }
                else if(postBObj is PollPostBObj)
                {
                    PollPostBObj pollPostBObj = postBObj as PollPostBObj;
                    PollPost pollPost = posts[i] as PollPost;

                    var choices = pollChoices.Where((choice) => choice.PostId == posts[i].Id).ToList();
                    pollPostBObj.Choices = choices;
                    pollPostBObj.Question = pollPost.Question;
                    postBObjs.Add(pollPostBObj);
                }
            }

            return postBObjs;
        }

        public List<CommentBObj> GetSortedComments(List<CommentBObj> postCommentBObjs)
        {
            List<CommentBObj> comments = postCommentBObjs;
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
    }
}
