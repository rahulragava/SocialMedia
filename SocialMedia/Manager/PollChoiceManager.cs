using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.DataSet;
using SocialMedia.Model.EntityModel;
using SocialMedia.Model.BusinessModel;

namespace SocialMedia.Manager
{
    public sealed class PollChoiceManager
    {
        private static PollChoiceManager? pollChoiceManager = null;
        private static readonly object padlock = new object();

        PollChoiceManager()
        {
        }

        public static PollChoiceManager Instance
        {
            get
            {
                if (pollChoiceManager == null)
                {
                    lock (padlock)
                    {
                        if (pollChoiceManager == null)
                        {
                            pollChoiceManager = new PollChoiceManager();
                        }
                    }
                }
                return pollChoiceManager;
            }
        }

        IPollChoiceSet pollChoiceSet = new PollChoiceSet();
        IUserPollChoiceSelectionSet userPollChoiceSelectionSet = new UserPollChoiceSelectionSet();

        public List<PollChoiceBObj> GetPollChoices()
        {
            List<PollChoiceBObj> pollChoiceBobjs = new List<PollChoiceBObj>();
            List<PollChoice> pollChoices = pollChoiceSet.RetrievePollChoiceList();
            List<UserPollChoiceSelection> userPollChoiceSelections = userPollChoiceSelectionSet.RetrievePollChoiceSelectionList();
            

            for (int i = 0; i < pollChoices.Count; i++)
            {
                PollChoiceBObj pollChoiceBobj = new PollChoiceBObj();
                var pollChoiceSelectedUsers = userPollChoiceSelections
                    .Where(userPollChoiceSelection => userPollChoiceSelection.ChoiceId == pollChoices[i].Id).ToList();
                    

                pollChoiceBobj.Id = pollChoices[i].Id;
                pollChoiceBobj.Choice = pollChoices[i].Choice;
                pollChoiceBobj.PostId = pollChoices[i].PostId;
                pollChoiceBobj.choiceSelectedUsers = pollChoiceSelectedUsers;

                pollChoiceBobjs.Add(pollChoiceBobj);
            }

            return pollChoiceBobjs;
        }

        public void AddPollChoice(PollChoiceBObj pollChoice)
        {
            var pollChoiceEntityModel = ConvertPollChoiceBobjToEntityModel(pollChoice);
            pollChoiceSet.AddPollChoice(pollChoiceEntityModel);
        }

        public PollChoice ConvertPollChoiceBobjToEntityModel(PollChoiceBObj pollChoiceBobj)
        {
            PollChoice pollChoice = new PollChoice();
            pollChoice.Id = pollChoiceBobj.Id;
            pollChoice.Choice = pollChoiceBobj.Choice;
            pollChoice.PostId = pollChoiceBobj.PostId;

            return pollChoice;
        }
        public void RemovePollChoice(PollChoice pollChoice)
        {
            pollChoiceSet.RemovePollChoice(pollChoice);
        }

        public void AddPollChoices(List<PollChoiceBObj> pollChoiceBobjList)
        {
            foreach (var pollChoice in pollChoiceBobjList)
            {
                if(pollChoice.choiceSelectedUsers != null && pollChoice.choiceSelectedUsers.Count > 0)
                {
                    userPollChoiceSelectionSet.AddUserPollChoiceSelections(pollChoice.choiceSelectedUsers);
                }
                AddPollChoice(pollChoice);
            }
        }

        public void RemovePollChoices(List<PollChoiceBObj> choices)
        {
            
            foreach(var pollChoice in choices)
            {
                if(pollChoice.choiceSelectedUsers != null && pollChoice.choiceSelectedUsers.Count > 0)
                    userPollChoiceSelectionSet.RemovePollChoiceSelections(pollChoice.choiceSelectedUsers);
                RemovePollChoice(ConvertPollChoiceBobjToEntityModel(pollChoice));
            }
        }

        public void AddChoiceSelectedUser(UserPollChoiceSelection userSelectionPollChoice)
        {
            if (userSelectionPollChoice != null)
            {
                userPollChoiceSelectionSet.AddUserPollChoiceSelection(userSelectionPollChoice);
            }
        }
    }
}
