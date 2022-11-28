using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataSet.DataSetInterface
{
    public interface IUserPollChoiceSelectionSet
    {
        void AddUserPollChoiceSelection(UserPollChoiceSelection userPollChoiceSelection);
        void AddUserPollChoiceSelections(List<UserPollChoiceSelection>? choiceSelectedUsers);
        void RemovePollChoiceSelections(List<UserPollChoiceSelection>? choiceSelectedUsers);
        void RemoveUserPollChoiceSelection(UserPollChoiceSelection userPollChoiceSelection);
        List<UserPollChoiceSelection> RetrievePollChoiceSelectionList();
    }
}
