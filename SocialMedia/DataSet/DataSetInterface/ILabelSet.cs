
using SocialMedia.Model.EntityModel;

namespace SocialMedia.DataSet.DataSetInterface
{
    public interface ILabelSet
    {
        void AddLabel(Label label);
        void RemoveLabel(Label label);
        List<string> GetLabel(string name);
        List<Label> GetLabels();
    }
}
