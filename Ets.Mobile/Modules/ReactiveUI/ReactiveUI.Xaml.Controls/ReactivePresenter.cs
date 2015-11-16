﻿using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Messaging.Interfaces.Common;
using Messaging.Interfaces.Notifications;
using Messaging.Interfaces.Popup;
using ReactiveUI.Xaml.Controls.ViewModel;
using Splat;

namespace ReactiveUI.Xaml.Controls
{
    [TemplatePart(Name = RefreshingPresenterPartName, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = ValuePresenterPartName, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = ErrorPresenterPartName, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = EmptyPresenterPartName, Type = typeof(ContentPresenter))]
    [TemplateVisualState(Name = RefresingStateName, GroupName = ReactiveGroupName)]
    [TemplateVisualState(Name = ValueStateName, GroupName = ReactiveGroupName)]
    [TemplateVisualState(Name = ErrorStateName, GroupName = ReactiveGroupName)]
    [TemplateVisualState(Name = EmptyStateName, GroupName = ReactiveGroupName)]
    [ContentProperty(Name = "ValueTemplate")]
    public class ReactivePresenter : ContentControl, IEnableLogger, IDisposable
    {
        #region Names

        private const string ReactiveGroupName = "ReactiveGroup";

        private const string RefresingStateName = "Refreshing";
        private const string ValueStateName = "Value";
        private const string ErrorStateName = "Error";
        private const string EmptyStateName = "Empty";

        private const string RefreshingPresenterPartName = "PART_RefreshingPresenter";
        private const string ValuePresenterPartName = "PART_ValuePresenter";
        private const string ErrorPresenterPartName = "PART_ErrorPresenter";
        private const string EmptyPresenterPartName = "PART_EmptyPresenter";
        // private const string WaitingPresenterPartName = "PART_WaitingPresenter";

        #endregion

        #region Content Presenters
        
        private ContentPresenter _refreshingPresenter;
        private ContentPresenter _valuePresenter;
        private ContentPresenter _errorPresenter;
        private ContentPresenter _emptyPresenter;

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty CurrentErrorProperty =
            DependencyProperty.Register("CurrentError", typeof(Exception), typeof(ReactivePresenter), new PropertyMetadata(null));

        public static readonly DependencyProperty CurrentIsEmptyProperty =
            DependencyProperty.Register("CurrentIsEmpty", typeof(bool), typeof(ReactivePresenter), new PropertyMetadata(null));

        public static readonly DependencyProperty CurrentIsRefreshingProperty =
            DependencyProperty.Register("CurrentIsRefreshing", typeof(bool), typeof(ReactivePresenter), new PropertyMetadata(null));

        public static readonly DependencyProperty WaitingTemplateProperty =
            DependencyProperty.Register("WaitingTemplate", typeof(DataTemplate), typeof(ReactivePresenter), new PropertyMetadata(null));

        public static readonly DependencyProperty RefreshingTemplateProperty =
            DependencyProperty.Register("RefreshingTemplate", typeof(DataTemplate), typeof(ReactivePresenter), new PropertyMetadata(null/*, OnRefreshTemplateChanged*/));

        public static readonly DependencyProperty ErrorTemplateProperty =
            DependencyProperty.Register("ErrorTemplate", typeof(DataTemplate), typeof(ReactivePresenter), new PropertyMetadata(null));

        public static readonly DependencyProperty ValueTemplateProperty =
            DependencyProperty.Register("ValueTemplate", typeof(DataTemplate), typeof(ReactivePresenter), new PropertyMetadata(null));

        public static readonly DependencyProperty EmptyTemplateProperty =
            DependencyProperty.Register("EmptyTemplate", typeof(DataTemplate), typeof(ReactivePresenter), new PropertyMetadata(null));

        public static readonly DependencyProperty RefreshingMessageProperty =
            DependencyProperty.Register("RefreshingMessage", typeof(string), typeof(ReactivePresenter), new PropertyMetadata(null));

        public static readonly DependencyProperty WaitingMessageProperty =
            DependencyProperty.Register("WaitingMessage", typeof(string), typeof(ReactivePresenter), new PropertyMetadata(null));

        public static readonly DependencyProperty EmptyMessageProperty =
            DependencyProperty.Register("EmptyMessage", typeof(string), typeof(ReactivePresenter), new PropertyMetadata(null));

        public static readonly DependencyProperty PreviousPresenterSourceProperty =
            DependencyProperty.Register("PreviousPresenterSource", typeof(object), typeof(ReactivePresenter), new PropertyMetadata(null));

        public static readonly DependencyProperty PresenterSourceProperty =
            DependencyProperty.Register("PresenterSource", typeof(object), typeof(ReactivePresenter), new PropertyMetadata(null, OnReactiveSourceChanged));

        public static readonly DependencyProperty CurrentSourceProperty =
            DependencyProperty.Register("CurrentSource", typeof(object), typeof(ReactivePresenter), new PropertyMetadata(null));
        
        public static readonly DependencyProperty PreviousSourceProperty =
			DependencyProperty.Register("PreviousSource", typeof(object), typeof(ReactivePresenter), new PropertyMetadata(null));
        
        public static readonly DependencyProperty ReactiveStateProperty =
            DependencyProperty.Register("ReactiveState", typeof(ReactivePresenter), typeof(ReactivePresenter), new PropertyMetadata(propertyChangedCallback: DataStateChanged, defaultValue: ReactiveState.Value));

        public static readonly DependencyProperty PreviousReactiveStateProperty =
            DependencyProperty.Register("PreviousReactiveState", typeof(ReactivePresenter), typeof(ReactivePresenter), null);
        
        public static readonly DependencyProperty UseDialogForErrorsProperty =
            DependencyProperty.Register("UseDialogForErrors", typeof(bool), typeof(ReactivePresenter), new PropertyMetadata(false));

        public static readonly DependencyProperty DefaultErrorMessageProperty =
            DependencyProperty.Register("DefaultErrorMessage", typeof(string), typeof(ReactivePresenter), new PropertyMetadata(null));

        public static readonly DependencyProperty DefaultErrorMessageKeyProperty =
            DependencyProperty.Register("DefaultErrorMessageKey", typeof(string), typeof(ReactivePresenter), new PropertyMetadata(null));

        public static readonly DependencyProperty DefaultErrorTitleKeyProperty =
            DependencyProperty.Register("DefaultErrorTitleKey", typeof(string), typeof(ReactivePresenter), new PropertyMetadata(null));
        
        #endregion
        
        #region Properties
        
        public DataTemplate RefreshingTemplate
        {
            get { return GetValue(RefreshingTemplateProperty) as DataTemplate; }
            set { SetValue(RefreshingTemplateProperty, value); }
        }

        public string RefreshingMessage
        {
            get { return GetValue(RefreshingMessageProperty) as string; }
            set { SetValue(RefreshingMessageProperty, value); }
        }

        public DataTemplate ErrorTemplate
        {
            get { return GetValue(ErrorTemplateProperty) as DataTemplate; }
            set { SetValue(ErrorTemplateProperty, value); }
        }

        public DataTemplate ValueTemplate
        {
            get { return GetValue(ValueTemplateProperty) as DataTemplate; }
            set { SetValue(ValueTemplateProperty, value); }
        }

        public DataTemplate EmptyTemplate
        {
            get { return GetValue(EmptyTemplateProperty) as DataTemplate; }
            set { SetValue(EmptyTemplateProperty, value); }
        }

        public string EmptyMessage
        {
            get { return GetValue(EmptyMessageProperty) as string; }
            set { SetValue(EmptyMessageProperty, value); }
        }

        public object PreviousSource
        {
            get { return GetValue(PreviousSourceProperty); }
            set { SetValue(PreviousSourceProperty, value); }
        }

        public object CurrentSource
        {
            get { return GetValue(CurrentSourceProperty); }
            set { SetValue(CurrentSourceProperty, value); }
        }

        public object PreviousPresenterSource
        {
            get { return GetValue(PreviousPresenterSourceProperty); }
            set { SetValue(PreviousPresenterSourceProperty, value); }
        }

        public object PresenterSource
        {
            get { return GetValue(PresenterSourceProperty); }
            set { SetValue(PresenterSourceProperty, value); }
        }

        public object CurrentError
        {
            get { return GetValue(CurrentErrorProperty) as Exception; }
            set { SetValue(CurrentErrorProperty, value); }
        }

        public object CurrentIsEmpty
        {
            get { return GetValue(CurrentIsEmptyProperty) as bool?; }
            set { SetValue(CurrentIsEmptyProperty, value); }
        }

        public object CurrentIsRefreshing
        {
            get { return GetValue(CurrentIsRefreshingProperty) as bool?; }
            set { SetValue(CurrentIsRefreshingProperty, value); }
        }
        
        public bool UseDialogForErrors
        {
            get { return (bool)GetValue(UseDialogForErrorsProperty); }
            set { SetValue(UseDialogForErrorsProperty, value); }
        }

        public string DefaultErrorMessage
        {
            get { return (string)GetValue(DefaultErrorMessageProperty); }
            set { SetValue(DefaultErrorMessageProperty, value); }
        }

        public ReactiveState PreviousReactiveState
        {
            get { return (ReactiveState)GetValue(PreviousReactiveStateProperty); }
            set { SetValue(PreviousReactiveStateProperty, value); }
        }

        public ReactiveState ReactiveState
        {
            get { return (ReactiveState)GetValue(ReactiveStateProperty); }
            set { SetValue(ReactiveStateProperty, value); }
        }

        #endregion

        #region _properties

        private CompositeDisposable _subscriptions;

        #endregion

        public ReactivePresenter()
        {
            DefaultStyleKey = typeof (ReactivePresenter);

            Loaded += (sender, e) => { Initialize(PresenterSource, null);  };
            Unloaded += (sender, e) => { Dispose(); };
        }

        #region Control Methods

        protected sealed override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            _refreshingPresenter = GetTemplateChild(RefreshingPresenterPartName) as ContentPresenter ?? new ContentPresenter();
            _valuePresenter = GetTemplateChild(ValuePresenterPartName) as ContentPresenter ?? new ContentPresenter();
            _errorPresenter = GetTemplateChild(ErrorPresenterPartName) as ContentPresenter ?? new ContentPresenter();
            _emptyPresenter = GetTemplateChild(EmptyPresenterPartName) as ContentPresenter ?? new ContentPresenter();

            Initialize(PresenterSource, null);
            if(ReactiveState == ReactiveState.Waiting)
            {
                GoToState(PreviousReactiveState);
            }
        }

        #endregion

        #region Methods

        bool _isReactiveSourceInitialized;
        public static void OnReactiveSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var presenter = d as ReactivePresenter;
            if (presenter != null)
            {
                if (DesignMode.DesignModeEnabled)
                {
                    if (e.NewValue != null)
                    {
                        presenter.SetTemplate(presenter._valuePresenter, presenter.ValueTemplate, e.NewValue);
                        VisualStateManager.GoToState(presenter, "Value", false);
                    }
                    return;
                }

                presenter.Initialize(e.NewValue, e.OldValue);
            }
        }

        private void Initialize(object reactiveSource, object previousValue)
        {
            PreviousPresenterSource = previousValue;

            if (!(PresenterSource is IReactivePresenterViewModel))
            {
                return;
            }

            if ((reactiveSource == null && previousValue != null) || (previousValue != null && reactiveSource != previousValue))
            {
                DisposeSubscriptions(false);
            }

            if (!_isReactiveSourceInitialized && reactiveSource != previousValue)
            {
                _subscriptions = new CompositeDisposable();
                _isReactiveSourceInitialized = true;
                var source = (IReactivePresenterViewModel)PresenterSource;

                _subscriptions.Add(source.Content.Where(x => x != null).Subscribe(x =>
                {
                    PreviousSource = previousValue;
                    CurrentSource = x;
                    ReactiveState = ReactiveState.Value;
                    this.Log().Info($"[{typeof(ReactivePresenter)}]: Value ({x}) State for {Name}");
                }));
                _subscriptions.Add(
                    source.IsContentEmpty.Where(isEmpty => isEmpty)
                    .Subscribe(x =>
                    {
                        CurrentIsEmpty = x;
                        ReactiveState = ReactiveState.Empty;
                        this.Log().Info($"[{typeof(ReactivePresenter)}]: IsEmpty State for {Name}");
                    })
                );
                _subscriptions.Add(source.IsRefreshing.Where(isRefreshing => isRefreshing).Subscribe(x =>
                {
                    CurrentIsRefreshing = x;
                    ReactiveState = ReactiveState.Refreshing;
                    this.Log().Info($"[{typeof(ReactivePresenter)}]: Refreshing State for {Name}");
                }));
                _subscriptions.Add(source.ThrownExceptions.Where(error => error != null).Subscribe(x =>
                {
                    CurrentError = x;
                    ReactiveState = ReactiveState.Error;
                    this.Log().Error($"[{typeof(ReactivePresenter)}]: Error State for {Name}");
                }));
            }
        }

        private static void DataStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var reactivePresenter = (d as ReactivePresenter);
            if (reactivePresenter != null)
            {
                reactivePresenter.GoToState((ReactiveState)e.NewValue);
            }
        }

        private void GoToState(ReactiveState state)
        {
            var stateToNavigateTo = state;

            switch (state)
            {
                case ReactiveState.Value:
                    SetTemplate(_valuePresenter, ValueTemplate, CurrentSource);
                    break;
                case ReactiveState.Refreshing:
                    SetTemplate(_refreshingPresenter, RefreshingTemplate, CurrentSource);
                    break;
                case ReactiveState.Error:
                    var errorAsPopup = CurrentError as IExceptionMessagingContent;

                    if (UseDialogForErrors)
                    {
                        if (errorAsPopup != null)
                        {
                            Locator.Current.GetService<IPopupManager>().ShowMessage(errorAsPopup.Message, errorAsPopup.Title);
                        }
                        else
                        {
                            var exception = CurrentError as Exception;
                            if (exception != null)
                            {
                                Locator.Current.GetService<IPopupManager>().ShowMessage(exception.Message, "");
                            }
                        }
                    }

                    if (CurrentSource != null)
                    {
                        if (!UseDialogForErrors && errorAsPopup != null)
                        {
                            Locator.Current.GetService<INotificationManager>("InApp").Notify(errorAsPopup);
                        }
                        
                        ReactiveState = ReactiveState.Value;
                        return;
                    }

                    SetTemplate(_errorPresenter, ErrorTemplate, CurrentSource, CurrentError as Exception);

                    break;
                case ReactiveState.Empty:
                    SetTemplate(_emptyPresenter, EmptyTemplate ?? ValueTemplate);
                    break;
                case ReactiveState.Disposed:
                case ReactiveState.Waiting: return;
            }

            var hasNavigated = VisualStateManager.GoToState(this, stateToNavigateTo.ToString(), true);
            if(!hasNavigated)
            {
                PreviousReactiveState = ReactiveState;
                ReactiveState = ReactiveState.Waiting;
            }
            Debug.WriteLine("State({0}) StateNavigated({1}) : HasNavigated({2})", state, stateToNavigateTo, hasNavigated);
        }

        private void SetTemplate(ContentPresenter presenter, DataTemplate template, object data = null,
            Exception error = null)
        {
            if (presenter == null)
            {
                return;
            }

            // Set Error if Any
            if (error != null)
            {
                DefaultErrorMessage = error.Message;

                if (data != null)
                {
                    return;
                }
            }

            // Attempt to set the container with the presenter content
            var container = presenter.Content as FrameworkElement;
            
            if (container != null)
            {
                container.DataContext = data ?? this;
            }

            var templateContainer = template.LoadContent() as FrameworkElement;

            // Attempt to set the container with the template
            if (templateContainer != null)
            {
                templateContainer.DataContext = data ?? this;
                container = templateContainer;
            }

            presenter.Content = container;
        }

        #endregion

        #region Dispose

        private void DisposeSubscriptions(bool withPresenter)
        {
            _isReactiveSourceInitialized = false;

            PresenterSource = null;
            PreviousPresenterSource = null;
            CurrentSource = null;
            PreviousSource = null;

            if (withPresenter)
            {
                ReactiveState = ReactiveState.Disposed;
            
                ClearPresenter(_valuePresenter);
                ClearPresenter(_errorPresenter);
                ClearPresenter(_emptyPresenter);
                ClearPresenter(_refreshingPresenter);
            }

            _subscriptions?.Dispose();
            _subscriptions = null;
        }

        private void ClearPresenter(ContentPresenter presenter)
        {
            if (presenter != null)
            {
                var frameworkElement = presenter.Content as FrameworkElement;
                if (frameworkElement != null)
                {
                    frameworkElement.DataContext = null;
                }
                presenter.DataContext = null;
                presenter.Visibility = Visibility.Collapsed;
            }
        }
        
        public void Dispose()
        {
            DisposeSubscriptions(true);
        }

        #endregion
    }

    public enum ReactiveState
    {
        Value,
        Refreshing,
        Error,
        Empty,
        Waiting,
        Disposed
    }
}
