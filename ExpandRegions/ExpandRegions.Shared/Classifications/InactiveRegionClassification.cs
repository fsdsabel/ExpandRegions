using System.ComponentModel.Composition;
using System.Windows.Media;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace ExpandRegions.Classifications
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.InactiveRegionClassificationTypeNames)]
    [Name(Constants.InactiveRegionName)]
    [DisplayName(Constants.InactiveRegionName)]
    [UserVisible(true)]
    [Order(After =Constants.OrderAfterPriority, Before = Constants.OrderBeforePriority)]
    internal sealed class InactiveRegionClassification : ClassificationFormatDefinition
    {
        #region Initialization

        // Methods
        public InactiveRegionClassification()
        {
            ForegroundColor = Colors.Gray;
            FontRenderingSize = 10;
            ForegroundOpacity = 0.5;
        }

        #endregion
    }
}