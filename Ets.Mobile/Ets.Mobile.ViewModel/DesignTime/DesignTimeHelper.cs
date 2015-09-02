using Windows.ApplicationModel;

namespace Ets.Mobile.ViewModel.DesignTime
{
    public static class DesignTimeHelper
    {
        public static bool IsInDesignMode => DesignMode.DesignModeEnabled;
    }
}
