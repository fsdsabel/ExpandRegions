using System.ComponentModel.Composition;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace ExpandRegions.Classifications
{
    internal sealed class ClassifierProvider
    {
        #region Internal Fields

        [Export]
        [Name(Constants.ActiveRegionClassificationTypeNames)]
        internal static ClassificationTypeDefinition ActiveRegionDefinition;

        [Export]
        [Name(Constants.InactiveRegionClassificationTypeNames)]
        internal static ClassificationTypeDefinition InactiveRegionDefinition;

        #endregion
    }
}