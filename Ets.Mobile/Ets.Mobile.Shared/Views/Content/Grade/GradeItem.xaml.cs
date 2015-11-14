using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Ets.Mobile.Views.Content.Grade
{
    public sealed partial class GradeItem : UserControl, INotifyPropertyChanged
    {
        public GradeItem()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(GradeItem), null);

        public string Title
        {
            get { return GetValue(TitleProperty).ToString(); }
            set { SetValueDp(TitleProperty, value); }
        }

        public static readonly DependencyProperty GradeProperty =
            DependencyProperty.Register("Grade", typeof(string), typeof(GradeItem), null);

        public string Grade
        {
            get { return GetValue(GradeProperty).ToString(); }
            set { SetValueDp(GradeProperty, value); }
        }

        #region Average

        public static readonly DependencyProperty AverageProperty =
            DependencyProperty.Register("Average", typeof(string), typeof(GradeItem), null);

        public string Average
        {
            get { return GetValue(AverageProperty).ToString(); }
            set { SetValueDp(AverageProperty, value); }
        }

        #endregion

        #region Median

        public static readonly DependencyProperty MedianProperty =
            DependencyProperty.Register("Median", typeof(string), typeof(GradeItem), null);

        public string Median
        {
            get { return GetValue(MedianProperty).ToString(); }
            set { SetValueDp(MedianProperty, value); }
        }

        #endregion

        #region Percentile

        public static readonly DependencyProperty PercentileProperty =
            DependencyProperty.Register("Percentile", typeof(string), typeof(GradeItem), null);

        public string Percentile
        {
            get { return GetValue(PercentileProperty).ToString(); }
            set { SetValueDp(PercentileProperty, value); }
        }

        #endregion

        #region StandardDeviation

        public static readonly DependencyProperty StandardDeviationProperty =
            DependencyProperty.Register("StandardDeviation", typeof(string), typeof(GradeItem), null);

        public string StandardDeviation
        {
            get { return GetValue(StandardDeviationProperty).ToString(); }
            set { SetValueDp(StandardDeviationProperty, value); }
        }

        #endregion

        #region Weighting

        public static readonly DependencyProperty WeightingProperty =
            DependencyProperty.Register("Weighting", typeof(string), typeof(GradeItem), null);

        public string Weighting
        {
            get { return GetValue(WeightingProperty).ToString(); }
            set { SetValueDp(WeightingProperty, value); }
        }

        #endregion

        public static readonly DependencyProperty BackgroundBrushProperty =
            DependencyProperty.Register("BackgroundBrush", typeof(Brush), typeof(GradeItem), null);

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
