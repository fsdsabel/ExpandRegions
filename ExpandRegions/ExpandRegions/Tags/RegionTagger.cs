using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;

namespace ExpandRegions.Tags
{
    public class RegionTagger : ITagger<RegionTag>
    {
        #region Private Fields

        private int _oldLineNumber = -1;
        private SnapshotSpan _oldSnapshotSpan;

        private static readonly object _lock = new object();

        #endregion

        #region Initialization

        public RegionTagger(ITextView view, ITextBuffer sourceBuffer, IClassificationTypeRegistryService classificationTypeRegistryService)
        {
            View = view;
            SourceBuffer = sourceBuffer;
            ClassificationTypeRegistryService = classificationTypeRegistryService;

            view.Caret.PositionChanged += CaretPositionChanged;
            view.LayoutChanged += ViewLayoutChanged;
        }

        #endregion

        #region Private Methods

        private void CaretPositionChanged(object sender, CaretPositionChangedEventArgs e)
        {
            UpdateAtCaretPosition(e.NewPosition);
        }


        private static bool IsRegionOrEndRegion(SnapshotSpan snapshotSpan)
        {
            return Regex.IsMatch(snapshotSpan.GetText().Trim().ToLower(), @"^#\W*(end\W*)*region");
        }

        private void UpdateAtCaretPosition(CaretPosition newPosition)
        {
            SnapshotPoint? point = newPosition.Point.GetPoint(SourceBuffer, newPosition.Affinity);

            if (!point.HasValue)
                return;

            ITextSnapshotLine lineFromPosition = point.Value.Snapshot.GetLineFromPosition(point.Value.Position);

            lock (_lock)
            {
                if (lineFromPosition.LineNumber != _oldLineNumber)
                {
                    SnapshotSpan snapshotSpan = new SnapshotSpan(lineFromPosition.Snapshot, lineFromPosition.Start.Position, lineFromPosition.Length);
                    bool isRegionOrEndRegion = IsRegionOrEndRegion(snapshotSpan);

                    EventHandler<SnapshotSpanEventArgs> tagsChanged = TagsChanged;
                    if (tagsChanged != null)
                    {
                        if (isRegionOrEndRegion)
                        {
                            tagsChanged(this, new SnapshotSpanEventArgs(snapshotSpan));
                        }
                        if (!_oldSnapshotSpan.IsEmpty)
                        {
                            tagsChanged(this, new SnapshotSpanEventArgs(_oldSnapshotSpan));
                        }
                    }

                    _oldSnapshotSpan = snapshotSpan;
                    _oldLineNumber = lineFromPosition.LineNumber;
                }
            }
        }

        private void ViewLayoutChanged(object sender, TextViewLayoutChangedEventArgs e)
        {
            if (e.NewViewState.EditSnapshot != e.OldViewState.EditSnapshot)
            {
                UpdateAtCaretPosition(View.Caret.Position);
            }
        }

        #endregion

        #region Private Properties

        private IClassificationTypeRegistryService ClassificationTypeRegistryService
        {
            get;
        }

        private ITextBuffer SourceBuffer
        {
            get;
        }

        private ITextView View
        {
            get;
        }

        #endregion

        #region Interface Implementation: ITagger<RegionTag>

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public IEnumerable<ITagSpan<RegionTag>> GetTags(NormalizedSnapshotSpanCollection snapShotSpans)
        {
            foreach (SnapshotSpan snapshotSpan in snapShotSpans.Where(IsRegionOrEndRegion))
            {
                SnapshotPoint? point = View.Caret.Position.Point.GetPoint(SourceBuffer, View.Caret.Position.Affinity);

                int pointPosition = point.HasValue ? snapshotSpan.Snapshot.GetLineNumberFromPosition(point.Value) : -1;
                int lineNumberFromPosition = snapshotSpan.Snapshot.GetLineNumberFromPosition(snapshotSpan.Start);
                int index = snapshotSpan.GetText().IndexOf(Constants.RegionIndicatorCharacter);

                SnapshotSpan newSpan = new SnapshotSpan(snapshotSpan.Snapshot, (int)snapshotSpan.Start + index, snapshotSpan.Length - index);

                string classificationTypeNames = lineNumberFromPosition != pointPosition
                    ? Constants.InactiveRegionClassificationTypeNames
                    : Constants.ActiveRegionClassificationTypeNames;

                yield return new TagSpan<RegionTag>(newSpan, new RegionTag(ClassificationTypeRegistryService.GetClassificationType(classificationTypeNames)));
            }
        }

        #endregion
    }
}