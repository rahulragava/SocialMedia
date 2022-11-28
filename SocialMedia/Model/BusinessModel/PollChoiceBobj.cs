using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Model.BusinessModel
{
    public class PollChoiceBobj
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string? Choice { get; set; }
        public List<UserPollChoiceSelection>? choiceSelectedUsers;
    }
}
