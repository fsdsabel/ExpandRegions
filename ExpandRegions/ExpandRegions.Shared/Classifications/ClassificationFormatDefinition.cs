using System.Globalization;
using System.Windows;
using System.Windows.Media;

using Microsoft.VisualStudio.Text.Classification;

namespace ExpandRegions.Classifications
{
    internal abstract class ClassificationFormatDefinition : EditorFormatDefinition
    {
        #region Protected Methods

        protected override ResourceDictionary CreateResourceDictionaryFromDefinition()
        {
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            AddOverridableProperties(resourceDictionary);
            if (ForegroundBrush != null)
            {
                resourceDictionary["Foreground"] = ForegroundBrush;
                if (ForegroundBrush.Opacity != 1.0)
                {
                    resourceDictionary["ForegroundOpacity"] = ForegroundBrush.Opacity;
                }
            }
            if (BackgroundBrush != null)
            {
                resourceDictionary["Background"] = BackgroundBrush;
                if (BackgroundBrush.Opacity != 1.0)
                {
                    resourceDictionary["BackgroundOpacity"] = BackgroundBrush.Opacity;
                }
            }
            if (FontTypeface != null)
            {
                resourceDictionary.Add("Typeface", FontTypeface);
                if (FontTypeface.Weight == FontWeights.Bold)
                {
                    resourceDictionary["IsBold"] = true;
                }
                if (FontTypeface.Style == FontStyles.Italic)
                {
                    resourceDictionary["IsItalic"] = true;
                }
            }
            if (FontRenderingSize.HasValue)
            {
                resourceDictionary.Add("FontRenderingSize", FontRenderingSize.Value);
            }
            if (FontHintingSize.HasValue)
            {
                resourceDictionary.Add("FontHintingSize", FontHintingSize.Value);
            }
            if (TextDecorations != null)
            {
                resourceDictionary.Add("TextDecorations", TextDecorations);
            }
            if (TextEffects != null)
            {
                resourceDictionary.Add("TextEffects", TextEffects);
            }
            if (CultureInfo != null)
            {
                resourceDictionary.Add("CultureInfo", CultureInfo);
            }
            return resourceDictionary;
        }

        #endregion

        #region Private Methods

        private void AddOverridableProperties(ResourceDictionary resourceDictionary)
        {
            if (ForegroundOpacity.HasValue)
            {
                resourceDictionary.Add("ForegroundOpacity", ForegroundOpacity.Value);
            }
            if (BackgroundOpacity.HasValue)
            {
                resourceDictionary.Add("BackgroundOpacity", BackgroundOpacity.Value);
            }
            if (IsBold.HasValue)
            {
                resourceDictionary.Add("IsBold", IsBold.Value);
            }
            if (IsItalic.HasValue)
            {
                resourceDictionary.Add("IsItalic", IsItalic.Value);
            }
            if (ForegroundColor.HasValue)
            {
                resourceDictionary["Foreground"] = new SolidColorBrush(ForegroundColor.Value);
            }
            if (BackgroundColor.HasValue)
            {
                resourceDictionary["Background"] = new SolidColorBrush(BackgroundColor.Value);
            }
        }

        #endregion

        #region Public Properties

        public double? BackgroundOpacity
        {
            get;
            protected set;
        }

        public CultureInfo CultureInfo
        {
            get;
            protected set;
        }

        public double? FontHintingSize
        {
            get;
            protected set;
        }

        public double? FontRenderingSize
        {
            get;
            protected set;
        }

        public Typeface FontTypeface
        {
            get;
            protected set;
        }

        public double? ForegroundOpacity
        {
            get;
            protected set;
        }

        public bool? IsBold
        {
            get;
            protected set;
        }

        public bool? IsItalic
        {
            get;
            protected set;
        }

        public TextDecorationCollection TextDecorations
        {
            get;
            protected set;
        }

        public TextEffectCollection TextEffects
        {
            get;
            protected set;
        }

        #endregion
    }
}