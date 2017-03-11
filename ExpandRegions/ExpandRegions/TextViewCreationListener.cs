using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Outlining;
using Microsoft.VisualStudio.Utilities;

namespace ExpandRegions
{
    [TextViewRole("DOCUMENT"), ContentType("CSharp"), ContentType("Basic"), Export(typeof(IWpfTextViewCreationListener))]
    public class TextViewCreationListener : IWpfTextViewCreationListener
    {
      

        [Import(typeof(IOutliningManagerService))]
        public IOutliningManagerService OutliningManagerService
        {
            get;
            set;
        }

        public void TextViewCreated(IWpfTextView textView)
        {
            if (textView == null || OutliningManagerService == null)
            {
                return;
            }
            RegionTextViewHandler.CreateHandler(textView, OutliningManagerService);
        }
    }
}
