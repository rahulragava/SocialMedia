using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.DataSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static PollChoiceManager GetPollPostManager()
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


        IPollChoiceSet pollChoiceSet = new PollChoiceSet();
        IUserPollChoiceSelectionSet userPollChoiceSelectionSet = new UserPollChoiceSelectionSet();

        public List<PollChoiceBobj> GetPollChoices()
        {
            List<PollChoiceBobj> pollChoiceBobjs = new List<PollChoiceBobj>();
            List<PollChoice> pollChoices = pollChoiceSet.RetrievePollChoiceList();
            List<UserPollChoiceSelection> userPollChoiceSelections = userPollChoiceSelectionSet.RetrievePollChoiceSelectionList();
            

            for (int i = 0; i < pollChoices.Count; i++)
            {
                PollChoiceBobj pollChoiceBobj = new PollChoiceBobj();
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

        public void AddPollChoice(PollChoiceBobj pollChoice)
        {
            var pollChoiceEntityModel = ConvertPollChoiceBobjToEntityModel(pollChoice);
            pollChoiceSet.AddPollChoice(pollChoiceEntityModel);
        }

        public PollChoice ConvertPollChoiceBobjToEntityModel(PollChoiceBobj pollChoiceBobj)
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

        public int GetLastPollChoiceId()

        {
            var choiceSet = pollChoiceSet.RetrievePollChoiceList();
            if(choiceSet.Count <= 0)
            {
                return 1;
            }
            return choiceSet[choiceSet.Count-1].Id; 
        }

        public void AddPollChoices(List<PollChoiceBobj> pollChoiceBobjList)
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

        public void RemovePollChoices(List<PollChoiceBobj> choices)
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
