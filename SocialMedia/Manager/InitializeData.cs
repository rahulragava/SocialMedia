using SocialMedia.DataSet;
using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;
using SocialMedia.Model.EntityModel.EnumTypes;

namespace SocialMedia.Manager
{
    public class InitializeData
    {
        UserManager userManager = UserManager.GetUserManager();
        UserCredentialManager userCredentialManager = UserCredentialManager.GetUserCredentialManager();
        TextPostManager textPostManager = TextPostManager.GetTextPostManager();
        PollPostManager pollPostManager = PollPostManager.GetPollPostManager();
        PollChoiceManager pollChoiceManager = PollChoiceManager.GetPollPostManager();
        CommentManager commentManager = CommentManager.GetCommentManager();
        ReactionManager reactionManager = ReactionManager.GetReactionManager();
        public void Initialize()
        {
            userCredentialManager.AddUserCredential(new UserCredential()
            {
                UserId = 1,
                UserName = "sanjei_pranav",
                PhoneNumber = "987654321",
                MailId = "sanjei@gmail.com",
                Password = "1234567",
            });

            userCredentialManager.AddUserCredential(new UserCredential()
            {
                UserId = 2,
                UserName = "shriniwaz007",
                PhoneNumber = "8123456789",
                MailId = "shriniwaz@gmail.com",
                Password = "abcdefg",
            });

            userCredentialManager.AddUserCredential(new UserCredential()
            {
                UserId = 3,
                UserName = "DineshThor",
                PhoneNumber = "7123456789",
                MailId = "dineshsds@gmail.com",
                Password = "dinesh",
            });
            userManager.AddUser(new User()
            {
                Id = 1,
                UserName = "sanjei_pranav",
                CreatedAt = new DateTime(2019, 12, 11),
                FirstName = "Sanjei",
                LastName = "Pranav",
                Gender = GenderType.Male,
                MaritalStatus = MaritalStatusType.Married,
                Education = "vidhyalaya matric school",
                Occupation = "Developer",
                Place = "chennai"

            });

            userManager.AddUser(new User()
            {
                Id = 2,
                UserName = "shriniwaz007",
                CreatedAt = new DateTime(2021, 12, 15),
                FirstName = "shriniwaz",
                Gender = GenderType.Male,
                MaritalStatus = MaritalStatusType.UnMarried,
                Education = "vidhyalaya matric school",
                Occupation = "Data Analyst",
                Place = "chennai",
            });
            userManager.AddUser(new User()
            {
                Id = 3,
                UserName = "DineshThor",
                CreatedAt = new DateTime(2022, 10, 11),
                FirstName = "Dinesh",
                LastName = "Sundar",
                Gender = GenderType.Male,
                MaritalStatus = MaritalStatusType.Married,
                Education = "vidhyalaya matric school",
                Occupation = "Data scientist",
                Place = "vellore"
            });

            pollPostManager.AddPollPost(new PollPostBobj()
            {
                Id = 1,
                Title = "social science",
                CreatedAt = DateTime.Now,
                LastModifiedAt = null,
                PostedBy = 1,
                Question = "what is the capital of india ? ",
            });

            pollPostManager.AddPollPost(new PollPostBobj()
            {
                Id = 2,
                Title = "General knowledge",
                CreatedAt = new DateTime(2020, 2, 12),
                LastModifiedAt = null,
                PostedBy = 3,
                Question = "what color is sky",

            });

            pollPostManager.AddPollPost(new PollPostBobj()
            {
                Id = 3,
                Title = "General Knowledge",
                CreatedAt = new DateTime(2019, 1, 1),
                LastModifiedAt = null,
                PostedBy = 3,
                Question = "how many days in a non leap year ? ",
            });

            pollPostManager.AddPollPost(new PollPostBobj()
            {
                Id = 5,
                Title = "common Knowledge",
                CreatedAt = new DateTime(2019, 10, 10),
                LastModifiedAt = null,
                PostedBy = 3,
                Question = "how are you ? ",
            });

            pollPostManager.AddPollPost(new PollPostBobj()
            {
                Id = 4,
                Title = "programming",
                CreatedAt = new DateTime(2012, 12, 12),
                LastModifiedAt = null,
                PostedBy = 2,
                Question = "console.log() is used in which language ? ",
            });


            pollChoiceManager.AddPollChoice(new PollChoiceBobj()
            {
                Id = 1,
                Choice = "Chennai",
                PostId = 1,

            });
            pollChoiceManager.AddPollChoice(new PollChoiceBobj()
            {
                Id = 2,
                Choice = "Mumbai",
                PostId = 1,

            });
            pollChoiceManager.AddPollChoice(new PollChoiceBobj()
            {
                Id = 3,
                Choice = "Kolkatta",
                PostId = 1,
            });
            pollChoiceManager.AddPollChoice(new PollChoiceBobj()
            {
                Id = 4,
                Choice = "Delhi",
                PostId = 1,
            });
            pollChoiceManager.AddPollChoice(new PollChoiceBobj()
            {
                Id = 5,
                Choice = "Red",
                PostId = 2,
            });
            pollChoiceManager.AddPollChoice(new PollChoiceBobj()
            {
                Id = 6,
                Choice = "Blue",
                PostId = 2,
            });
            pollChoiceManager.AddPollChoice(new PollChoiceBobj()
            {
                Id = 7,
                Choice = "Green",
                PostId = 2,
            });
            pollChoiceManager.AddPollChoice(new PollChoiceBobj()
            {
                Id = 8,
                Choice = "364",
                PostId = 3,
            });
            pollChoiceManager.AddPollChoice(new PollChoiceBobj()
            {
                Id = 9,
                Choice = "365",
                PostId = 3,
            });
            pollChoiceManager.AddPollChoice(new PollChoiceBobj()
            {
                Id = 10,
                Choice = "345",
                PostId = 3,
            });
            pollChoiceManager.AddPollChoice(new PollChoiceBobj()
            {
                Id = 11,
                Choice = "python",
                PostId = 4,
            });
            pollChoiceManager.AddPollChoice(new PollChoiceBobj()
            {
                Id = 12,
                Choice = "C#",
                PostId = 4,

            });
            pollChoiceManager.AddPollChoice(new PollChoiceBobj()
            {
                Id = 13,
                Choice = "js",
                PostId = 4,
            });

            textPostManager.AddTextPost(new TextPostBobj()
            {
                Id = 1,
                Content = "world is such a beautiful place to live",
                CreatedAt = new DateTime(2021, 10, 10),
                LastModifiedAt = null,
                PostedBy = 1,
                Title = "world quote",
            });
            textPostManager.AddTextPost(new TextPostBobj()
            {
                Id = 2,
                Content = "inner peace is important",
                CreatedAt = new DateTime(2015, 10, 10),
                LastModifiedAt = new DateTime(2019, 9, 10),
                PostedBy = 3,
                Title = "peace",
            });
            textPostManager.AddTextPost(new TextPostBobj()
            {
                Id = 3,
                Content = "life is all about ups and downs ..",
                CreatedAt = new DateTime(2022, 10, 10),
                LastModifiedAt = null,
                PostedBy = 2,
                Title = "life quote",
            });

            textPostManager.AddTextPost(new TextPostBobj()
            {
                Id = 4,
                Content = "try hard to achieve",
                CreatedAt = new DateTime(2022, 10, 10),
                LastModifiedAt = null,
                PostedBy = 2,
                Title = "life quote",
            });

            textPostManager.AddTextPost(new TextPostBobj()
            {
                Id = 5,
                Content = "nothing easy is worth doing..",
                CreatedAt = new DateTime(2022, 10, 10),
                LastModifiedAt = null,
                PostedBy = 2,
                Title = "life quote",
            });

            textPostManager.AddTextPost(new TextPostBobj()
            {
                Id = 6,
                Content = "We have gained 100 followers !!!",
                CreatedAt = new DateTime(2022, 10, 10),
                LastModifiedAt = null,
                PostedBy = 3,
                Title = "Achievements",
            });

            commentManager.AddComment(new CommentBobj()
            {
                Id = 1,
                PostId = 1,
                CommentedAt = new DateTime(2015, 12, 12),
                CommentedBy = 2,
                CommentedOn = CommentedOnType.TextPost,
                ParentCommentId = null,
                Content = "nice post",
            });

            commentManager.AddComment(new CommentBobj()
            {
                Id = 2,
                PostId = 1,
                Content = "Thanks for the comment",
                ParentCommentId = 1,
                CommentedAt = new DateTime(2015, 12, 13),
                CommentedBy = 3,
                CommentedOn = CommentedOnType.TextPost

            });
        }
    }
}
