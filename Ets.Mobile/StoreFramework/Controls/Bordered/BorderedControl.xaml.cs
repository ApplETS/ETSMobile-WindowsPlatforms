using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace StoreFramework.Controls.Bordered
{
    public sealed partial class BorderedControl : UserControl, INotifyPropertyChanged
    {
        public BorderedControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty BodyProperty =
            DependencyProperty.Register("Body", typeof(FrameworkElement), typeof(BorderedControl), new PropertyMetadata(default(FrameworkElement)));

        public FrameworkElement Body
        {
            get { return (FrameworkElement)GetValue(BodyProperty); }
            set { SetValueDp(BodyProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(FrameworkElement), typeof(BorderedControl), new PropertyMetadata(default(object)));

        public FrameworkElement Header
        {
            get { return (FrameworkElement)GetValue(HeaderProperty); }
            set { SetValueDp(HeaderProperty, value); }
        }

        public static readonly DependencyProperty BackgroundBrushProperty =
            DependencyProperty.Register("BackgroundBrush", typeof(Brush), typeof(BorderedControl), new PropertyMetadata(default(Brush)));

        public Brush BackgroundBrush
        {
            get { return (Brush)GetValue(BackgroundBrushProperty); }
            set { SetValueDp(BackgroundBrushProperty, value); }
        }

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
