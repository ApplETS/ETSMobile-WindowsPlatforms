using Messaging.Interfaces.Common;
using Messaging.Interfaces.Notifications;
using ReactiveUI.Xaml.Controls.Handlers;
using Splat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

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
        
        public static readonly DependencyProperty DisableErrorNotificationProperty =
            DependencyProperty.Register("DisableErrorNotification", typeof(bool), typeof(ReactivePresenter), new PropertyMetadata(false));

        public static readonly DependencyProperty DefaultErrorMessageProperty =
            DependencyProperty.Register("DefaultErrorMessage", typeof(string), typeof(ReactivePresenter), new PropertyMetadata(null));

        public static readonly DependencyProperty DefaultEmptyMessageProperty =
            DependencyProperty.Register("DefaultEmptyMessage", typeof(string), typeof(ReactivePresenter), new PropertyMetadata(null));

        public static readonly DependencyProperty DefaultErrorTitleKeyProperty =
            DependencyProperty.Register("DefaultErrorTitleKey", typeof(string), typeof(ReactivePresenter), new PropertyMetadata(null));

        public static readonly DependencyProperty UseEmptyMessagesProperty =
            DependencyProperty.Register("UseEmptyMessages", typeof(bool), typeof(ReactivePresenter), new PropertyMetadata(null));

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
            get { return GetValue(CurrentErrorProperty); }
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
        
        public bool DisableErrorNotification
        {
            get { return (bool)GetValue(DisableErrorNotificationProperty); }
            set { SetValue(DisableErrorNotificationProperty, value); }
        }

        public string DefaultErrorMessage
        {
            get { return (string)GetValue(DefaultErrorMessageProperty); }
            set { SetValue(DefaultErrorMessageProperty, value); }
        }

        public string DefaultEmptyMessage
        {
            get { return (string)GetValue(DefaultEmptyMessageProperty); }
            set { SetValue(DefaultEmptyMessageProperty, value); }
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

        public bool UseEmptyMessages
        {
            get { return (bool)GetValue(UseEmptyMessagesProperty); }
            set { SetValue(UseEmptyMessagesProperty, value); }
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

        private bool _isReactiveSourceInitialized;
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

            if (PresenterSource == null || !(PresenterSource is IReactivePresenterHandler))
            {
                return;
            }

            if ((reactiveSource == null && previousValue != null) || (previousValue != null && reactiveSource != previousValue))
            {
                DisposeSubscriptions(false);
            }

            if (!_isReactiveSourceInitialized)
            {
                _subscriptions = new CompositeDisposable();
                _isReactiveSourceInitialized = true;
                var source = ((IReactivePresenterHandler)PresenterSource) ?? (IReactivePresenterHandler)reactiveSource;

                // Put the according state
                SetStateDuringInitialization(source);

                // When New Content Arrives
                _subscriptions.Add(source.Content.Where(x => x != null).Subscribe(content =>
                {
                    SetContent(content, previousValue);
                }));
                // When Ready, Ensures that the state is Value
                _subscriptions.Add(source.IsReady.Subscribe(x =>
                {
                    SetIsReady();
                }));
                // When a message says that the Value is empty, set Empty
                _subscriptions.Add(
                    source.EmptyMessage.Where(message => message != null)
                    .Subscribe(messagingContent =>
                    {
                        SetIsEmpty(true, messagingContent);
                    })
                );
                _subscriptions.Add(source.IsRefreshing.Where(isRefreshing => isRefreshing).Subscribe(SetIsRefreshing));
                _subscriptions.Add(source.ThrownExceptions.Where(error => error != null).Subscribe(ex => {
                    SetThrownException(ex.Message);
                }));
            }
        }

        private void SetContent(object value, object previousValue = null, bool hasBeenInjected = false)
        {
            Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                PreviousSource = previousValue != null && value != null && previousValue.GetType() != value.GetType() ? previousValue : null;
                CurrentSource = value;
                ReactiveState = ReactiveState.Value;
            }).GetAwaiter().OnCompleted(() => { });
        }

        private void SetIsReady()
        {
            Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                if (ReactiveState != ReactiveState.Value)
                {
                    ReactiveState = ReactiveState.Value;
                }
            }).GetAwaiter().OnCompleted(() => { });
        }

        private void SetIsEmpty(bool isEmpty, IMessagingContent content = null, bool hasBeenInjected = false)
        {
            Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                if (UseEmptyMessages && content != null)
                {
                    DefaultEmptyMessage = content.Message;
                }
                CurrentIsEmpty = isEmpty;
                ReactiveState = ReactiveState.Empty;
            }).GetAwaiter().OnCompleted(() => { });
        }

        private void SetIsRefreshing(bool hasBeenInjected = false)
        {
            Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                CurrentIsRefreshing = true;
                ReactiveState = ReactiveState.Refreshing;
            }).GetAwaiter().OnCompleted(() => { });
        }

        private void SetThrownException(string message, bool hasBeenInjected = false)
        {
            Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                CurrentError = message;
                ReactiveState = ReactiveState.Error;
            }).GetAwaiter().OnCompleted(() => { });
        }

        private void SetStateDuringInitialization(IReactivePresenterHandler source)
        {
            // This puts the right content/state
            Task.Run(async () =>
            {
                var eventsList = new List<string>();
                
                var getValue = source.GetLastValue();
                var getEmptyMessage = source.GetLastEmptyMessage();
                var getThrownException = source.GetLastThrownException();
                var getRefreshing = source.GetLastRefreshing();

                Action checkForEmptyOrThrownException = async () =>
                {
                    var subSubTaskCompleted = await Task.WhenAny(Task.WhenAny(getEmptyMessage, getThrownException), Task.Delay(TimeSpan.FromSeconds(10)));
                    if (subSubTaskCompleted == getEmptyMessage && getEmptyMessage.Result != null)
                    {
                        SetIsEmpty(true, getEmptyMessage.Result, true);
                    }
                    else if (subSubTaskCompleted == getThrownException && getThrownException.Result != null)
                    {
                        SetThrownException(getThrownException.Result.Message, true);
                    }
                };

                Action checkForCollectionCount = () =>
                {
                    var reactiveCollection = getValue.Result as IReactiveCollection<object>;
                    if (reactiveCollection != null)
                    {
                        // it's a reactivelist or reactiveDerivedList
                        if (!reactiveCollection.Any())
                        {
                            SetIsEmpty(true, hasBeenInjected: true);
                        }
                        else
                        {
                            SetContent(reactiveCollection, hasBeenInjected:true);
                        }
                        return;
                    }
                    if (getValue.Result != null)
                    {
                        SetContent(getValue.Result, hasBeenInjected: true);
                        return;
                    }

                    checkForEmptyOrThrownException();
                };

                eventsList.Add("Recoverering State");
                await getRefreshing;
                if (getRefreshing.Result)
                {
                    eventsList.Add("1- State Recovered: Refreshing");
                    SetIsRefreshing(true);
                    var subTaskCompleted = await Task.WhenAny(getEmptyMessage, getThrownException, getValue);
                    if (subTaskCompleted == getValue)
                    {
                        eventsList.Add("2- State Recovered: Value");
                        checkForCollectionCount();
                    }
                    else if (subTaskCompleted == getEmptyMessage)
                    {
                        eventsList.Add("2- State Recovered: Empty");
                        SetIsEmpty(true, getEmptyMessage.Result, hasBeenInjected: true);
                    }
                    else if (subTaskCompleted == getThrownException)
                    {
                        eventsList.Add("2- State Recovered: Thrown Exception");
                        SetThrownException(getThrownException.Result.Message, true);
                    }
                }
                else
                {
                    eventsList.Add("1- State Recovered: Not Refreshing");
                    var taskCompleted = await Task.WhenAny(getValue, getEmptyMessage, getThrownException);
                    if (taskCompleted == getValue && getValue.Result != null)
                    {
                        eventsList.Add("2- State Recovered: Value");
                        checkForCollectionCount();
                    }
                    else if (taskCompleted == getEmptyMessage && getEmptyMessage.Result != null)
                    {
                        eventsList.Add("2- State Recovered: Empty");
                        SetIsEmpty(true, getEmptyMessage.Result, true);
                    }
                    else if (taskCompleted == getThrownException && getThrownException.Result != null)
                    {
                        eventsList.Add("2- State Recovered: Thrown Exception");
                        SetThrownException(getThrownException.Result.Message, true);
                    }
                }

                RxApp.MainThreadScheduler.Schedule(() =>
                {
                    foreach (var e in eventsList)
                    {
                        this.Log().Debug($"ReactivePresenter[{Name}]" + e);
                    }
                });
            });
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

                    if (CurrentSource != null)
                    {
                        if (!DisableErrorNotification && errorAsPopup != null)
                        {
                            Locator.Current.GetService<INotificationManager>("InApp").Notify(errorAsPopup);
                        }
                        
                        ReactiveState = ReactiveState.Value;
                        return;
                    }

                    SetTemplate(_errorPresenter, ErrorTemplate, CurrentSource, CurrentError as Exception);
                    break;
                case ReactiveState.Empty:
                    if (UseEmptyMessages)
                    {
                        SetTemplate(_emptyPresenter, EmptyTemplate);
                    }
                    else
                    {
                        SetTemplate(_emptyPresenter, EmptyTemplate ?? ValueTemplate);
                    }
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
#if DEBUG
            Debug.WriteLine($"{Name} -> State({state}) StateNavigated({stateToNavigateTo}) : HasNavigated({hasNavigated})");
#endif
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