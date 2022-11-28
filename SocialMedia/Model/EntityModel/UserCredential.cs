using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Model.EntityModel
{
    public class UserCredential
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? MailId { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
