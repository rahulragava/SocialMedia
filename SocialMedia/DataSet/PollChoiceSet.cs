using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataSet
{
    public class PollChoiceSet : IPollChoiceSet
    {
        private readonly List<PollChoice> _pollChoices = new();

        public void AddPollChoice(PollChoice pollChoice)
        {
            if(pollChoice != null)
            {
                _pollChoices.Add(pollChoice);
            }
        }

        public void RemovePollChoice(PollChoice pollChoice)
        {
            if(pollChoice != null)
            {
                _pollChoices.Remove(pollChoice);
            }
           
        }

        public List<PollChoice> RetrievePollChoiceList()
        {
            return _pollChoices;
        }
    }
}
