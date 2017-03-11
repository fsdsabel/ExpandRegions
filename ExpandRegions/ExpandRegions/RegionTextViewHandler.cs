using System;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Outlining;

namespace ExpandRegions
{
    class RegionTextViewHandler
    {
        private IWpfTextView _textView;
        private IOutliningManager _outliningManager;

        public static void CreateHandler(IWpfTextView textView, IOutliningManagerService outliningManagerService)
        {
            new RegionTextViewHandler(textView, outliningManagerService);
        }

        private RegionTextViewHandler(IWpfTextView textView, IOutliningManagerService outliningManagerService)
        {
            
            _outliningManager = outliningManagerService.GetOutliningManager(textView);
            if (_outliningManager == null)
            {
                return;
            }
            _textView = textView;
            _outliningManager.RegionsCollapsed += OnRegionsCollapsed;
            _textView.Closed += OnClosed;

        }

        private void OnClosed(object sender, EventArgs e)
        {
            if (_outliningManager != null)
            {
                _outliningManager.RegionsCollapsed -= OnRegionsCollapsed;
            }
            if (_textView != null)
            {
                _textView.Closed -= OnClosed;
            }
            _textView = null;
            _outliningManager = null;
        }
        
        private void OnRegionsCollapsed(object sender, RegionsCollapsedEventArgs e)
        {
            foreach (var cr in e.CollapsedRegions)
            {
                _outliningManager.Expand(cr);
            }
            _outliningManager.RegionsCollapsed -= OnRegionsCollapsed;
        }

       
    }
}
