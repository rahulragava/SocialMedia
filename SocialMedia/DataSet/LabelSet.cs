using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataSet
{
    public class LabelSet : ILabelSet
    {
        private static readonly List<Label> _labelSet = new List<Label>();

        public void AddLabel(Label label)
        {
            if(label != null)
            {
                _labelSet.Add(label);
            }
        }

        public List<string> GetLabel(string name) // need to check and ignore the string case, or at the creation can make it to lower
        {
            //if(name.Equals(/*new string(),comparisonType.*/))
            return _labelSet.Where(label => label.Name == name).Select(label => label.PostId).ToList();
           
        }

        public List<Label> GetLabels()
        {
            return _labelSet;
        }

        public void RemoveLabel(Label label)
        {
            if(label != null)
            {
                _labelSet.Remove(label);
            }
        }
    }
}
