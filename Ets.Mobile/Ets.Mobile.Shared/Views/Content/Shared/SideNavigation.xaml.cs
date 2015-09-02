﻿using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Ets.Mobile.Views.Content.Shared
{
    public sealed partial class SideNavigation : UserControl
    {
        public SideNavigation()
        {
            InitializeComponent();
            
        }

        public object ProfileDataContext
        {
            get { return Profile.DataContext; }
            set { Profile.DataContext = value; }
        }

        public ICommand LogoutCommand
        {
            get { return Logout.Command; }
            set { Logout.Command = value; }
        }
    }
}
