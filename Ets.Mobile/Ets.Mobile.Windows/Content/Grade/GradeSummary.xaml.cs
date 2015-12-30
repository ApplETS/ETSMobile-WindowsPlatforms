using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Ets.Mobile.Content.Grade
{
    public sealed partial class GradeSummary : UserControl, INotifyPropertyChanged
    {
        public GradeSummary()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(GradeSummary), null);

        public string Title
        {
            get { return GetValue(TitleProperty).ToString(); }
            set { SetValueDp(TitleProperty, value); }
        }

        #region Grade

        public static readonly DependencyProperty GradeProperty =
            DependencyProperty.Register("Grade", typeof(string), typeof(GradeSummary), null);

        public string Grade
        {
            get { return GetValue(GradeProperty).ToString(); }
            set { SetValueDp(GradeProperty, value); }
        }

        #endregion

        public static readonly DependencyProperty BackgroundBrushProperty =
            DependencyProperty.Register("BackgroundBrush", typeof(Brush), typeof(GradeSummary), null);

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
