using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel;

namespace Ets.Mobile.ViewModel.DesignTime
{
    public class DesignTimeBase : INotifyPropertyChanged
    {
        public static bool IsInDesignMode => DesignMode.DesignModeEnabled;

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
