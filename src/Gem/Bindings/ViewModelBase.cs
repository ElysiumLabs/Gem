using Gem.Diagnostics;
using Prism.Behaviors;
using Prism.Common;
using Prism.Events;
using Prism.Ioc;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Plugin.Popups;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using System;
using System.Threading.Tasks;

namespace Gem.Bindings
{

    public abstract partial class ViewModelBase : BindableBase, IInitialize, INavigationAware
    {

        public ViewModelBase(ViewModelBaseServices viewModelBaseServices)
        {
            if (viewModelBaseServices == null) { throw new Exception(nameof(ViewModelBaseServices) + " is null"); }

            EventAggregator = viewModelBaseServices.EventAggregator;
            NavigationService = viewModelBaseServices.NavigationService;
            ExceptionHandler = viewModelBaseServices.ExceptionHandler;
        }

        public virtual void Initialize(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.GetNavigationMode() == NavigationMode.New) 
            {
                try
                {
                    BusyLoader.SetIsLoading(true);

                    await Load(parameters);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
                finally
                {
                    BusyLoader.SetIsLoading(false);
                }
                
            }
        }

        public abstract Task Load(INavigationParameters parameters);

        public virtual void HandleException(Exception ex) 
        {
            ExceptionHandler?.Handle(ex);
            EventAggregator?.GetEvent<ViewModelBaseExceptionEvent>()?.Publish(ex);
        }

        private ViewModelLoader busyLoader = new ViewModelLoader();
        public ViewModelLoader BusyLoader
        {
            get { return busyLoader; }
            set { SetProperty(ref busyLoader, value); }
        }

        public IEventAggregator EventAggregator { get; protected set; }
        public ExceptionHandler ExceptionHandler { get; private set; }
        public IGemAppNavigationService NavigationService { get; protected set; }
    }

    public class ViewModelBaseServices
    {
        public ViewModelBaseServices(
            //ILoggerFacade logger,
            IEventAggregator eventAggregator,
            ExceptionHandler exceptionHandler,
            NavigationService navigationService
            )
        {
            //Logger = logger;
            EventAggregator = eventAggregator;
            ExceptionHandler = exceptionHandler;
            NavigationService = navigationService;
        }

        //public ILoggerFacade Logger { get; }
        public IEventAggregator EventAggregator { get; }
        public ExceptionHandler ExceptionHandler { get; }
        public NavigationService NavigationService { get; }

}

    public class ViewModelBaseExceptionEvent : PubSubEvent<Exception>
    {

    }

    public class NavigationService : PageNavigationService, IGemAppNavigationService
    {
        public NavigationService(IContainerProvider container, IApplicationProvider applicationProvider, IPageBehaviorFactory pageBehaviorFactory, PopupPageNavigationService popupPageNavigationService) : base(container, applicationProvider, pageBehaviorFactory)
        {
            Popup = popupPageNavigationService;
        }

        public PopupPageNavigationService Popup { get; }

        public Task<INavigationResult> NavigateAsync<T>()
        {
            return typeof(T).BaseType == typeof(PopupPage) ?
                Popup.NavigateAsync(typeof(T).Name)
                : base.NavigateAsync(typeof(T).Name);
        }

        public Task<INavigationResult> NavigateAsync<T>(INavigationParameters parameters)
        {
            return typeof(T).BaseType == typeof(PopupPage) ?
                Popup.NavigateAsync(typeof(T).Name, parameters)
                : base.NavigateAsync(typeof(T).Name, parameters);
        }
    }

    public interface IGemAppNavigationService : INavigationService
    {
        PopupPageNavigationService Popup { get; }
        Task<INavigationResult> NavigateAsync<T>();
        Task<INavigationResult> NavigateAsync<T>(INavigationParameters parameters);
    }
}
