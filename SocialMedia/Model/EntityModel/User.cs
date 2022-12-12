using SocialMedia.Constant;

namespace SocialMedia.Model.EntityModel
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MailId { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public string Place { get; set; }
        public string Education { get; set; }
        public string Occupation { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString();
            
        }

    }
}
