using Gem.Diagnostics;
using Prism.Behaviors;
using Prism.Common;
using Prism.Events;
using Prism.Ioc;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Plugin.Popups;
using Prism.Xaml;
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
        public IGemNavigationService NavigationService { get; protected set; }
    }

    public class ViewModelBaseServices
    {
        public ViewModelBaseServices(
            //ILoggerFacade logger,
            IEventAggregator eventAggregator,
            ExceptionHandler exceptionHandler,
            IGemNavigationService navigationService
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
        public IGemNavigationService NavigationService { get; }

    }

    public class ViewModelBaseExceptionEvent : PubSubEvent<Exception>
    {

    }

    public interface IGemNavigationService 
    {
        PopupPageNavigationService Popup { get; }
        INavigationService NavigationService { get; }

        Task<INavigationResult> GoBackAsync();

        Task<INavigationResult> GoBackAsync(INavigationParameters parameters);

        Task<INavigationResult> GoBackAsync(INavigationParameters parameters, bool? useModalNavigation, bool animated);

        Task<INavigationResult> GoBackToRootAsync(INavigationParameters parameters);

        Task<INavigationResult> NavigateAsync(Uri uri);

        Task<INavigationResult> NavigateAsync(Uri uri, INavigationParameters parameters);

        Task<INavigationResult> NavigateAsync(string name);

        Task<INavigationResult> NavigateAsync(string name, INavigationParameters parameters);

        Task<INavigationResult> NavigateAsync(string name, INavigationParameters parameters, bool? useModalNavigation, bool animated);

        Task<INavigationResult> NavigateAsync(Uri uri, INavigationParameters parameters, bool? useModalNavigation, bool animated);
    }


    public class GemNavigationService : IGemNavigationService
    {
        public PopupPageNavigationService Popup { get; private set; }

        public INavigationService NavigationService { get; private set; }

        public GemNavigationService(PopupPageNavigationService popupPageNavigationService, INavigationService navigationService) 
        {
            Popup = popupPageNavigationService;
            NavigationService = navigationService;
        }

        public Task<INavigationResult> GoBackAsync()
        {
            return NavigationService.GoBackAsync();
        }

        public Task<INavigationResult> GoBackAsync(INavigationParameters parameters)
        {
            return NavigationService.GoBackAsync(parameters);
        }

        public Task<INavigationResult> GoBackAsync(INavigationParameters parameters, bool? useModalNavigation, bool animated)
        {
            return NavigationService.GoBackAsync(parameters, useModalNavigation, animated);
        }

        public Task<INavigationResult> GoBackToRootAsync(INavigationParameters parameters)
        {
            return NavigationService.GoBackToRootAsync(parameters);
        }

        public Task<INavigationResult> NavigateAsync(Uri uri)
        {
            return NavigationService.NavigateAsync(uri);
        }

        public Task<INavigationResult> NavigateAsync(Uri uri, INavigationParameters parameters)
        {
            return NavigationService.NavigateAsync(uri, parameters);
        }

        public Task<INavigationResult> NavigateAsync(string name)
        {
            return NavigationService.NavigateAsync(name);
        }

        public Task<INavigationResult> NavigateAsync(string name, INavigationParameters parameters)
        {
            return NavigationService.NavigateAsync(name, parameters);
        }

        public Task<INavigationResult> NavigateAsync(string name, INavigationParameters parameters, bool? useModalNavigation, bool animated)
        {
            return NavigationService.NavigateAsync(name, parameters, useModalNavigation, animated);
        }

        public Task<INavigationResult> NavigateAsync(Uri uri, INavigationParameters parameters, bool? useModalNavigation, bool animated)
        {
            return NavigationService.NavigateAsync(uri, parameters, useModalNavigation, animated);
        }
    }
}
