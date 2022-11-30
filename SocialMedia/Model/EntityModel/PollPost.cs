using SocialMedia.Model.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Model.EntityModel
{
    public class PollPost : Post
    {
        public string? Question { get; set; }
    }
    
}
