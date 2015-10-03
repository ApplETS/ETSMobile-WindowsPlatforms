using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Controls.Bordered
{
    public sealed partial class BorderedControl : UserControl, INotifyPropertyChanged
    {
        public BorderedControl()
        {
            InitializeComponent();
        }

        #region Body

        private static readonly DependencyProperty BodyField =
            DependencyProperty.Register("Body", typeof(FrameworkElement), typeof(BorderedControl), new PropertyMetadata(default(FrameworkElement)));

        internal static DependencyProperty BodyProperty => BodyField;

        public FrameworkElement Body
        {
            get { return (FrameworkElement)GetValue(BodyField); }
            set { SetValueDp(BodyField, value); }
        }

        #endregion

        #region Header

        private static readonly DependencyProperty HeaderField =
            DependencyProperty.Register("Header", typeof(FrameworkElement), typeof(BorderedControl), new PropertyMetadata(default(object)));

        internal static DependencyProperty HeaderProperty => BackgroundBrushField;

        public FrameworkElement Header
        {
            get { return (FrameworkElement)GetValue(HeaderField); }
            set { SetValueDp(HeaderField, value); }
        }

        #endregion

        #region BackgroundBrush

        private static readonly DependencyProperty BackgroundBrushField =
            DependencyProperty.Register("BackgroundBrush", typeof(Brush), typeof(BorderedControl), new PropertyMetadata(default(Brush)));

        internal static DependencyProperty BackgroundBrushProperty => BackgroundBrushField;

        public Brush BackgroundBrush
        {
            get { return (Brush)GetValue(BackgroundBrushField); }
            set { SetValueDp(BackgroundBrushField, value); }
        }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        
        private void SetValueDp(DependencyProperty property, object value, [CallerMemberName] string propertyName = null)
        {
            SetValue(property, value);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
