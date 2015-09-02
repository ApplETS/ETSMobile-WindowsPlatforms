using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using StoreFramework.Converters;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace StoreFramework.Controls
{
    public sealed partial class LocalizedTextBlock : UserControl
    {
        public LocalizedTextBlock()
        {
            InitializeComponent();

            TextWrapping = TextWrapping.NoWrap;

            DataContext = this;
        }

        #region Key

        private string _key;
        public string Key
        {
            get { return _key; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("value");
                }

                _key = value;

                if (!DesignMode.DesignModeEnabled)
                {
                    PutLocalizedValue(_key);
                }
            }
        }
        
        #endregion

        #region Fallback Value

        private string _defaultValue = "";
        public string DefaultValue
        {
            get { return _defaultValue; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("value");
                }

                _defaultValue = value;

                if (DesignMode.DesignModeEnabled)
                {
                    OutputText = GetAppliedTextStyleOptions(_defaultValue);
                }
            }
        }

        #endregion

        #region Output Text

        public static readonly DependencyProperty OutputTextProperty =
            DependencyProperty.Register(
                "OutputText",
                typeof(string),
                typeof(LocalizedTextBlock),
                new PropertyMetadata("")
            );

        public string OutputText
        {
            get { return (string)GetValue(OutputTextProperty); }
            set { SetValue(OutputTextProperty, value); }
        }

        #endregion

        #region TextWrapping

        public static readonly DependencyProperty TextWrappingProperty =
            DependencyProperty.Register(
                "TextWrapping",
                typeof(TextWrapping),
                typeof(LocalizedTextBlock),
                new PropertyMetadata(default(TextWrapping))
            );

        public TextWrapping TextWrapping
        {
            get { return (TextWrapping)GetValue(TextWrappingProperty); }
            set { SetValue(TextWrappingProperty, value); }
        }

        #endregion

        #region Uppercase

        private CharacterCasing _characterCase;
        public CharacterCasing CharacterCase
        {
            get { return _characterCase; }
            set { _characterCase = value; PutLocalizedValue(Key); }
        }

        public static readonly DependencyProperty CharacterCasingProperty =
            DependencyProperty.Register(
                "CharacterCase",
                typeof(CharacterCasing),
                typeof(LocalizedTextBlock),
                new PropertyMetadata("Normal")
            );

        #endregion

        private void PutLocalizedValue(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                OutputText = DefaultValue;
                return;
            }

            object text;
            try
            {
                text = new StringToLocalizedStringConverter().Convert(key, null, null, "");
            }
            catch(KeyNotFoundException)
            {
                text = DefaultValue;
            }

            var outtext = GetAppliedTextStyleOptions(text.ToString());

            OutputText = outtext;
        }

        private string GetAppliedTextStyleOptions(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            switch (CharacterCase)
            {
                case CharacterCasing.Lower:
                    text = text.ToLower();
                    break;

                case CharacterCasing.Upper:
                    text = text.ToUpper();
                    break;
            }

            return text;

            // Add other options...
        }
    }

    public enum CharacterCasing
    {
        Normal,
        Lower,
        Upper
    }
}
