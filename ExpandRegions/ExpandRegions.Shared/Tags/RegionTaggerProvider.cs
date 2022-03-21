using System.ComponentModel.Composition;

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace ExpandRegions.Tags
{
    [Export(typeof(IViewTaggerProvider))]
    [ContentType(Constants.CSharpContentType)]
    [ContentType(Constants.BasicContentType)]
    [TagType(typeof(RegionTag))]
    public class RegionTaggerProvider : IViewTaggerProvider
    {
        #region Internal Properties

        [Import]
        internal IClassificationTypeRegistryService ClassificationTypeRegistryService
        {
            get;
            set;
        }

        #endregion

        #region Interface Implementation: IViewTaggerProvider

        public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag
        {
            if (textView.TextBuffer != buffer)
            {
                return null;
            }

            return new RegionTagger(textView, buffer, ClassificationTypeRegistryService) as ITagger<T>;
        }

        #endregion
    }
}