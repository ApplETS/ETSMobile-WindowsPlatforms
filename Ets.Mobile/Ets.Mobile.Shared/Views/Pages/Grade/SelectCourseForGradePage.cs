﻿using Windows.UI.Xaml;
using Ets.Mobile.ViewModel.Pages.Grade;
using ReactiveUI;

namespace Ets.Mobile.Pages.Grade
{
    public partial class SelectCourseForGradePage : IViewFor<SelectCourseForGradeViewModel>
    {
        #region IViewFor<T>

        public SelectCourseForGradeViewModel ViewModel
        {
            get { return (SelectCourseForGradeViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(SelectCourseForGradeViewModel), typeof(GradePage), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (SelectCourseForGradeViewModel)value; }
        }

        #endregion

        public SelectCourseForGradePage()
        {
            InitializeComponent();
        }
    }
}
