using SocialMedia.Constant;
using SocialMedia.DataSet;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;

namespace SocialMedia.Manager
{
    public class InitializeData
    {
        readonly UserManager _userManager = UserManager.Instance;
        readonly UserCredentialManager _userCredentialManager = UserCredentialManager.Instance;
        readonly PostManager _postManager = PostManager.Instance;
        readonly PollChoiceManager _pollChoiceManager = PollChoiceManager.Instance;
        readonly CommentManager _commentManager = CommentManager.Instance;
        readonly ReactionManager _reactionManager = ReactionManager.Instance;
        public void Initialize()
        {
            _userCredentialManager.AddUserCredential(new UserCredential("User1", "1234567"));
            _userCredentialManager.AddUserCredential(new UserCredential("User2", "abcdefg"));
            _userCredentialManager.AddUserCredential(new UserCredential("User3", "dinesh"));

            _userManager.AddUser(new User()
            {
                Id = "User1",
                UserName = "sanjei_pranav",
                CreatedAt = new DateTime(2019, 12, 11),
                PhoneNumber = "987654321",
                MailId = "sanjei@gmail.com",
                FirstName = "Sanjei",
                LastName = "Pranav",
                Gender = Gender.Male,
                MaritalStatus = MaritalStatus.Married,
                Education = "vidhyalaya matric school",
                Occupation = "Developer",
                Place = "chennai"

            });

            _userManager.AddUser(new User()
            {
                Id = "User2",
                UserName = "shriniwaz007",
                CreatedAt = new DateTime(2021, 12, 15),
                FirstName = "shriniwaz",
                LastName = "K",
                Gender = Gender.Male,
                PhoneNumber = "8123456789",
                MailId = "shriniwaz@gmail.com",
                MaritalStatus = MaritalStatus.UnMarried,
                Education = "vidhyalaya matric school",
                Occupation = "Data Analyst",
                Place = "chennai",
            }); 
            _userManager.AddUser(new User()
            {
                Id = "User3",
                UserName = "DineshThor",
                CreatedAt = new DateTime(2022, 10, 11),
                FirstName = "Dinesh",
                LastName = "Sundar",
                PhoneNumber = "7123456789",
                MailId = "dineshsds@gmail.com",
                Gender = Gender.Male,
                MaritalStatus = MaritalStatus.Married,
                Education = "vidhyalaya matric school",
                Occupation = "Data scientist",
                Place = "vellore"
            });

            _postManager.AddPost(new PollPostBObj()
            {
                Id = "PP1",
                Title = "social science",
                CreatedAt = DateTime.Now,
                LastModifiedAt = DateTime.Now,
                PostedBy = "User1",
                Question = "what is the capital of india ? ",
            });

            _postManager.AddPost(new PollPostBObj()
            {
                Id = "PP2",
                Title = "General knowledge",
                CreatedAt = new DateTime(2020, 2, 12),
                LastModifiedAt = new DateTime(2020, 2, 12),
                PostedBy = "User3",
                Question = "what color is sky",

            });

            _postManager.AddPost(new PollPostBObj()
            {
                Id = "PP3",
                Title = "General Knowledge",
                CreatedAt = new DateTime(2019, 1, 1),
                LastModifiedAt = new DateTime(2019, 1, 1),
                PostedBy = "User3",
                Question = "how many days in a non leap year ? ",
            });

            _postManager.AddPost(new PollPostBObj()
            {
                Id = "PP5",
                Title = "common Knowledge",
                CreatedAt = new DateTime(2019, 10, 10),
                LastModifiedAt = new DateTime(2019, 10, 10),
                PostedBy = "User3",
                Question = "how are you ? ",
            });

            _postManager.AddPost(new PollPostBObj()
            {
                Id = "PP4",
                Title = "programming",
                CreatedAt = new DateTime(2012, 12, 12),
                LastModifiedAt = new DateTime(2012, 12, 12),
                PostedBy = "User2",
                Question = "console.log() is used in which language ? ",
            });


            _pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice1",
                Choice = "Chennai",
                PostId = "PP1",

            });
            _pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice2",
                Choice = "Mumbai",
                PostId = "PP1",

            });
            _pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice3",
                Choice = "Kolkatta",
                PostId = "PP1",
            });
            _pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice4",
                Choice = "Delhi",
                PostId = "PP1",
            });
            _pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice5",
                Choice = "Red",
                PostId = "PP2",
            });
            _pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice6",
                Choice = "Blue",
                PostId = "PP2",
            });
            _pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice7",
                Choice = "Green",
                PostId = "PP2",
            });
            _pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice8",
                Choice = "364",
                PostId = "PP3",
            });
            _pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice9",
                Choice = "365",
                PostId = "PP3",
            });
            _pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice10",
                Choice = "345",
                PostId = "PP3",
            });
            _pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice11",
                Choice = "python",
                PostId = "PP4",
            });
            _pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice12",
                Choice = "C#",
                PostId = "PP4",

            });
            _pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice13",
                Choice = "js",
                PostId = "PP4",
            });

            _postManager.AddPost(new TextPostBObj()
            {
                Id = "TP1",
                Content = "world is such a beautiful place to live",
                CreatedAt = new DateTime(2021, 10, 10),
                LastModifiedAt = new DateTime(2021, 10, 10),
                PostedBy = "User1",
                Title = "world quote",
            });
            _postManager.AddPost(new TextPostBObj()
            {
                Id = "TP2",
                Content = "inner peace is important",
                CreatedAt = new DateTime(2015, 10, 10),
                LastModifiedAt = new DateTime(2019, 9, 10),
                PostedBy = "User3",
                Title = "peace",
            });
            _postManager.AddPost(new TextPostBObj()
            {
                Id = "TP3",
                Content = "life is all about ups and downs ..",
                CreatedAt = new DateTime(2022, 10, 10),
                LastModifiedAt = new DateTime(2022, 10, 10),
                PostedBy = "User2",
                Title = "life quote",
            });

            _postManager.AddPost(new TextPostBObj()
            {
                Id = "TP4",
                Content = "try hard to achieve",
                CreatedAt = new DateTime(2022, 10, 10),
                LastModifiedAt = new DateTime(2022, 10, 10),
                PostedBy = "User2",
                Title = "life quote",
            });

            _postManager.AddPost(new TextPostBObj()
            {
                Id = "TP5",
                Content = "nothing easy is worth doing..",
                CreatedAt = new DateTime(2022, 10, 10),
                LastModifiedAt = new DateTime(2022, 10, 10),
                PostedBy = "User2",
                Title = "life quote",
            });

            _postManager.AddPost(new TextPostBObj()
            {
                Id = "TP6",
                Content = "We have gained 100 followers !!!",
                CreatedAt = new DateTime(2022, 10, 10),
                LastModifiedAt = new DateTime(2022, 10, 10),
                PostedBy = "User3",
                Title = "Achievements",
            });

            _commentManager.AddComment(new CommentBObj()
            {
                Id = "CO1",
                PostId = "TP1",
                CommentedAt = new DateTime(2015, 12, 12),
                CommentedBy = "User2",
                ParentCommentId = null,
                Content = "nice post",
            });

            _commentManager.AddComment(new CommentBObj()
            {
                Id = "CO2",
                PostId = "TP1",
                Content = "Thanks for the comment",
                ParentCommentId = "CO1",
                CommentedAt = new DateTime(2015, 12, 13),
                CommentedBy = "User3",
            });

            LabelSet labelSet = new LabelSet();
            labelSet.AddLabel(new Label()
            {
                Id = "LA1",
                Name = "favourite",
                PostId = "TP1"
            });
            labelSet.AddLabel(new Label()
            {
                Id = "LA2",
                Name = "favourite",
                PostId = "PP1"
            });
            labelSet.AddLabel(new Label()
            {
                Id = "LA3",
                Name = "important",
                PostId = "TP1"
            }); 
            labelSet.AddLabel(new Label()
            {
                Id = "LA4",
                Name = "favourite",
                PostId = "TP2"
            }); 
            labelSet.AddLabel(new Label()
            {
                Id = "LA5",
                Name = "important",
                PostId = "TP2"
            });
        }
    }
}
