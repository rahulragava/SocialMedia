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
        public string UserId { get; set; }

        public string Password { get; set; }

        public UserCredential(string userId, string password)        {
            UserId = userId;
            Password = password;
        }

    }
}
