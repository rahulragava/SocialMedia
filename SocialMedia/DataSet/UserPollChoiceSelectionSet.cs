using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.EntityModel;

namespace SocialMedia.DataSet
{
    public class UserPollChoiceSelectionSet : IUserPollChoiceSelectionSet
    {
        private List<UserPollChoiceSelection> _userPollChoiceSelections = new List<UserPollChoiceSelection>();
        public void AddUserPollChoiceSelection(UserPollChoiceSelection userPollChoiceSelection)
        {
            _userPollChoiceSelections.Add(userPollChoiceSelection); 
        }

        public void AddUserPollChoiceSelections(List<UserPollChoiceSelection>? choiceSelectedUsers)
        {
            if(choiceSelectedUsers != null)
            {
                foreach (var choiceSelectedUser in choiceSelectedUsers)
                {
                    AddUserPollChoiceSelection(choiceSelectedUser);
                }
            }
        }

        public void RemovePollChoiceSelections(List<UserPollChoiceSelection>? choiceSelectedUsers)
        {
            if(choiceSelectedUsers != null)
            {
                foreach (var choiceSelectedUser in choiceSelectedUsers)
                {
                    RemoveUserPollChoiceSelection(choiceSelectedUser);  
                }
            }
        }


        public void RemoveUserPollChoiceSelection(UserPollChoiceSelection userPollChoiceSelection)
        {
            if(userPollChoiceSelection != null)
                _userPollChoiceSelections.Remove(userPollChoiceSelection);
        }

        public List<UserPollChoiceSelection> RetrievePollChoiceSelectionList()
        {
            return _userPollChoiceSelections;
        }
    }
}
