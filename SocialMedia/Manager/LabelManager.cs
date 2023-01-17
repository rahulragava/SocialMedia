using SocialMedia.DataSet;
using SocialMedia.DataSet.DataSetInterface;
using SocialMedia.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Manager
{
    public class LabelManager
    {
        private static LabelManager _labelManager;
        private static readonly object _lock = new object();

        LabelManager() { }

        public static LabelManager Instance
        {
            get
            {
                if(_labelManager == null)
                {
                    lock (_lock)
                    {
                        if(_labelManager == null)
                        {
                            _labelManager = new LabelManager();
                        }
                    }
                }
                return _labelManager;
            }
        }

        readonly ILabelSet _labelSet = new LabelSet();

        public void AddLabel(Label label)
        {
            if(label == null)
            {
                return;
            }
            _labelSet.AddLabel(label);
        }

        public List<Label> GetLabels()
        {
            return _labelSet.GetLabels();
        }

        public List<string> GetLabelByName(string labelName)
        {
            
            if (string.IsNullOrEmpty(labelName))
            {
                return null;
            }
            return _labelSet.GetLabel(labelName);
        }

        public List<Label> GetUserLabels(string userId)
        {
            var labels = GetLabels();
            var userLabels = labels.Where(label => PostManager.Instance.GetUserId(label.PostId) == userId).ToList();

            return userLabels;
        }

        public void RemoveLabel(Label label)
        {
            if(label == null)
            {
                return;
            }
            _labelSet.RemoveLabel(label);
        }

        public void RemoveLabelByPostId(string PostId)
        {
            var labels = _labelSet.GetLabels();
            var removableLabels = labels.Where(label => label.PostId == PostId).ToList();
            if (!removableLabels.Any())
                return;

            foreach(var label in removableLabels)
            {
                RemoveLabel(label);
            }
        }
    }
}
