using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Controls.Bordered
{
    public sealed class BorderedControlDesign : INotifyPropertyChanged
    {
        public BorderedControlDesign()
        {
            Header = new TextBlock
            {
                Text = "Header",
                FontSize = 32,
                Foreground = new SolidColorBrush(Colors.White)
            };
            Body = new TextBlock
            {
                Text = "Body",
                FontSize = 32,
                Foreground = new SolidColorBrush(Colors.White)
            };
            BackgroundBrush = new SolidColorBrush(Colors.Orange);
        }

        private object _body;
        public object Body
        {
            get { return _body; }
            set { _body = value; OnPropertyChanged(); }
        }

        private object _header;
        public object Header
        {
            get { return _header; }
            set { _header = value; OnPropertyChanged(); }
        }

        private Brush _backgroundBrush;
        public Brush BackgroundBrush
        {
            get { return _backgroundBrush; }
            set { _backgroundBrush = value; OnPropertyChanged(); }
        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}