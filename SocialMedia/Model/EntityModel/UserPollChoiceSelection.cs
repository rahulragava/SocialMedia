using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Model.EntityModel
{
    public class UserPollChoiceSelection
    {
        public string ChoiceId { get; set; }
        public string SelectedBy { get; set; }

        public UserPollChoiceSelection(string choiceId, string selectedBy)
        {
            ChoiceId = choiceId;
            SelectedBy = selectedBy;
        }
    }
}
