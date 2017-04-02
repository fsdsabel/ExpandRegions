using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;

namespace ExpandRegions.Tags
{
    public sealed class RegionTag : ClassificationTag
    {
        #region Initialization

        public RegionTag(IClassificationType type)
            : base(type)
        {
        }

        #endregion
    }
}