using SocialMedia.Constant;
using SocialMedia.Model.BusinessModel;
using SocialMedia.Model.EntityModel;

namespace SocialMedia.Manager
{
    public class InitializeData
    {
        UserManager userManager = UserManager.Instance;
        UserCredentialManager userCredentialManager = UserCredentialManager.Instance;
        TextPostManager textPostManager = TextPostManager.Instance;
        PollPostManager pollPostManager = PollPostManager.Instance;
        PollChoiceManager pollChoiceManager = PollChoiceManager.Instance;
        CommentManager commentManager = CommentManager.Instance;
        ReactionManager reactionManager = ReactionManager.Instance;
        public void Initialize()
        {
            userCredentialManager.AddUserCredential(new UserCredential()
            {
                UserId = "User1",
                Password = "1234567",
            });

            userCredentialManager.AddUserCredential(new UserCredential()
            {
                UserId = "User2",
                Password = "abcdefg",
            });

            userCredentialManager.AddUserCredential(new UserCredential()
            {
                UserId = "User3",
                Password = "dinesh",
            });
            userManager.AddUser(new User()
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

            userManager.AddUser(new User()
            {
                Id = "User2",
                UserName = "shriniwaz007",
                CreatedAt = new DateTime(2021, 12, 15),
                FirstName = "shriniwaz",
                Gender = Gender.Male,
                PhoneNumber = "8123456789",
                MailId = "shriniwaz@gmail.com",
                MaritalStatus = MaritalStatus.UnMarried,
                Education = "vidhyalaya matric school",
                Occupation = "Data Analyst",
                Place = "chennai",
            });
            userManager.AddUser(new User()
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

            pollPostManager.AddPollPost(new PollPostBObj()
            {
                Id = "PP1",
                Title = "social science",
                CreatedAt = DateTime.Now,
                LastModifiedAt = null,
                PostedBy = "User1",
                Question = "what is the capital of india ? ",
            });

            pollPostManager.AddPollPost(new PollPostBObj()
            {
                Id = "PP2",
                Title = "General knowledge",
                CreatedAt = new DateTime(2020, 2, 12),
                LastModifiedAt = null,
                PostedBy = "User3",
                Question = "what color is sky",

            });

            pollPostManager.AddPollPost(new PollPostBObj()
            {
                Id = "PP3",
                Title = "General Knowledge",
                CreatedAt = new DateTime(2019, 1, 1),
                LastModifiedAt = null,
                PostedBy = "User3",
                Question = "how many days in a non leap year ? ",
            });

            pollPostManager.AddPollPost(new PollPostBObj()
            {
                Id = "PP5",
                Title = "common Knowledge",
                CreatedAt = new DateTime(2019, 10, 10),
                LastModifiedAt = null,
                PostedBy = "User3",
                Question = "how are you ? ",
            });

            pollPostManager.AddPollPost(new PollPostBObj()
            {
                Id = "PP4",
                Title = "programming",
                CreatedAt = new DateTime(2012, 12, 12),
                LastModifiedAt = null,
                PostedBy = "User2",
                Question = "console.log() is used in which language ? ",
            });


            pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice1",
                Choice = "Chennai",
                PostId = "PP1",

            });
            pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice2",
                Choice = "Mumbai",
                PostId = "PP1",

            });
            pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice3",
                Choice = "Kolkatta",
                PostId = "PP1",
            });
            pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice4",
                Choice = "Delhi",
                PostId = "PP1",
            });
            pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice5",
                Choice = "Red",
                PostId = "PP2",
            });
            pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice6",
                Choice = "Blue",
                PostId = "PP2",
            });
            pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice7",
                Choice = "Green",
                PostId = "PP2",
            });
            pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice8",
                Choice = "364",
                PostId = "PP3",
            });
            pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice9",
                Choice = "365",
                PostId = "PP3",
            });
            pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice10",
                Choice = "345",
                PostId = "PP3",
            });
            pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice11",
                Choice = "python",
                PostId = "PP4",
            });
            pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice12",
                Choice = "C#",
                PostId = "PP4",

            });
            pollChoiceManager.AddPollChoice(new PollChoiceBObj()
            {
                Id = "PollChoice13",
                Choice = "js",
                PostId = "PP4",
            });

            textPostManager.AddTextPost(new TextPostBObj()
            {
                Id = "TP1",
                Content = "world is such a beautiful place to live",
                CreatedAt = new DateTime(2021, 10, 10),
                LastModifiedAt = null,
                PostedBy = "User1",
                Title = "world quote",
            });
            textPostManager.AddTextPost(new TextPostBObj()
            {
                Id = "TP2",
                Content = "inner peace is important",
                CreatedAt = new DateTime(2015, 10, 10),
                LastModifiedAt = new DateTime(2019, 9, 10),
                PostedBy = "User3",
                Title = "peace",
            });
            textPostManager.AddTextPost(new TextPostBObj()
            {
                Id = "TP3",
                Content = "life is all about ups and downs ..",
                CreatedAt = new DateTime(2022, 10, 10),
                LastModifiedAt = null,
                PostedBy = "User2",
                Title = "life quote",
            });

            textPostManager.AddTextPost(new TextPostBObj()
            {
                Id = "TP4",
                Content = "try hard to achieve",
                CreatedAt = new DateTime(2022, 10, 10),
                LastModifiedAt = null,
                PostedBy = "User2",
                Title = "life quote",
            });

            textPostManager.AddTextPost(new TextPostBObj()
            {
                Id = "TP5",
                Content = "nothing easy is worth doing..",
                CreatedAt = new DateTime(2022, 10, 10),
                LastModifiedAt = null,
                PostedBy = "User2",
                Title = "life quote",
            });

            textPostManager.AddTextPost(new TextPostBObj()
            {
                Id = "TP6",
                Content = "We have gained 100 followers !!!",
                CreatedAt = new DateTime(2022, 10, 10),
                LastModifiedAt = null,
                PostedBy = "User3",
                Title = "Achievements",
            });

            commentManager.AddComment(new CommentBObj()
            {
                Id = "CO1",
                PostId = "TP1",
                CommentedAt = new DateTime(2015, 12, 12),
                CommentedBy = "User2",
                ParentCommentId = null,
                Content = "nice post",
            });

            commentManager.AddComment(new CommentBObj()
            {
                Id = "CO2",
                PostId = "TP1",
                Content = "Thanks for the comment",
                ParentCommentId = "CO1",
                CommentedAt = new DateTime(2015, 12, 13),
                CommentedBy = "User3",
            });
        }
    }
}
