using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.DataSet;
using SocialMedia.Model.EntityModel;
using SocialMedia.Model.BusinessModel;

namespace SocialMedia.Manager
{
    public sealed class PollChoiceManager
    {
        private static PollChoiceManager _pollChoiceManager = null;
        private static readonly object _padlock = new object();

        PollChoiceManager()
        {
        }

        public static PollChoiceManager Instance
        {
            get
            {
                if (_pollChoiceManager == null)
                {
                    lock (_padlock)
                    {
                        _pollChoiceManager ??= new PollChoiceManager();
                    }
                }
                return _pollChoiceManager;
            }
        }

        readonly IPollChoiceSet _pollChoiceSet = new PollChoiceSet();
        readonly IUserPollChoiceSelectionSet _userPollChoiceSelectionSet = new UserPollChoiceSelectionSet();

        public List<PollChoiceBObj> GetPollChoices()
        {
            List<PollChoiceBObj> pollChoiceBobjs = new List<PollChoiceBObj>();
            List<PollChoice> pollChoices = _pollChoiceSet.RetrievePollChoiceList();
            List<UserPollChoiceSelection> userPollChoiceSelections = _userPollChoiceSelectionSet.RetrievePollChoiceSelectionList();
            

            for (int i = 0; i < pollChoices.Count; i++)
            {
                PollChoiceBObj pollChoiceBobj = new PollChoiceBObj();
                var pollChoiceSelectedUsers = userPollChoiceSelections
                    .Where(userPollChoiceSelection => userPollChoiceSelection.ChoiceId == pollChoices[i].Id).ToList();
                    

                pollChoiceBobj.Id = pollChoices[i].Id;
                pollChoiceBobj.Choice = pollChoices[i].Choice;
                pollChoiceBobj.PostId = pollChoices[i].PostId;
                pollChoiceBobj.ChoiceSelectedUsers = pollChoiceSelectedUsers;

                pollChoiceBobjs.Add(pollChoiceBobj);
            }

            return pollChoiceBobjs;
        }

        public void AddPollChoice(PollChoiceBObj pollChoice)
        {
            var pollChoiceEntityModel = ConvertPollChoiceBobjToEntityModel(pollChoice);
            _pollChoiceSet.AddPollChoice(pollChoiceEntityModel);
        }

        public PollChoice ConvertPollChoiceBobjToEntityModel(PollChoiceBObj pollChoiceBobj)
        {
            PollChoice pollChoice = new PollChoice
            {
                Id = pollChoiceBobj.Id,
                Choice = pollChoiceBobj.Choice,
                PostId = pollChoiceBobj.PostId
            };

            return pollChoice;
        }
        public void RemovePollChoice(PollChoice pollChoice)
        {
            _pollChoiceSet.RemovePollChoice(pollChoice);
        }

        public void AddPollChoices(List<PollChoiceBObj> pollChoiceBobjList)
        {
            foreach (var pollChoice in pollChoiceBobjList)
            {
                if(pollChoice.ChoiceSelectedUsers != null && pollChoice.ChoiceSelectedUsers.Any())
                {
                    _userPollChoiceSelectionSet.AddUserPollChoiceSelections(pollChoice.ChoiceSelectedUsers);
                }
                AddPollChoice(pollChoice);
            }
        }

        public void RemovePollChoices(List<PollChoiceBObj> choices)
        {
            
            foreach(var pollChoice in choices)
            {
                if(pollChoice.ChoiceSelectedUsers != null && pollChoice.ChoiceSelectedUsers.Any())
                    _userPollChoiceSelectionSet.RemovePollChoiceSelections(pollChoice.ChoiceSelectedUsers);
                RemovePollChoice(ConvertPollChoiceBobjToEntityModel(pollChoice));
            }
        }

        public void AddChoiceSelectedUser(UserPollChoiceSelection userSelectionPollChoice)
        {
            if (userSelectionPollChoice != null)
            {
                _userPollChoiceSelectionSet.AddUserPollChoiceSelection(userSelectionPollChoice);
            }
        }
    }
}
