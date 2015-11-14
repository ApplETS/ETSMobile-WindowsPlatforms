using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;

namespace Ets.Mobile.ViewModel.StateManager
{
    public class LoadingStateManager : ViewModelBase
    {
        private string _viewMessage;
        public string ViewMessage
        {
            get { return _viewMessage; }
            set { Set(() => ViewMessage, ref _viewMessage, value); }
        }

        private Visibility _loadingVisibility;
        public Visibility LoadingVisibility
        {
            get { return _loadingVisibility; }
            set { Set(() => LoadingVisibility, ref _loadingVisibility, value); }
        }

        private bool _isViewMessageVisible;
        public bool IsViewMessageVisible
        {
            get { return _isViewMessageVisible; }
            set
            {
                if (Set(() => IsViewMessageVisible, ref _isViewMessageVisible, value))
                {
                    LoadingVisibility = (IsViewMessageVisible) ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }
    }
}
