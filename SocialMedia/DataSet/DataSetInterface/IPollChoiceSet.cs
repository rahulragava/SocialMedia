using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataSet.DataSetInterface
{
    public interface IPollChoiceSet
    {
        void AddPollChoice(PollChoice pollChoice);
        void RemovePollChoice(PollChoice pollChoice);
        List<PollChoice> RetrievePollChoiceList();
    }
}
