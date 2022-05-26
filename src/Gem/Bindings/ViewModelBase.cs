using Gem.Diagnostics;
using Prism.Events;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Threading.Tasks;

namespace Gem.Bindings
{

    public abstract partial class ViewModelBase : BindableBase, IInitialize, INavigationAware
    {

        public ViewModelBase(ViewModelBaseServices viewModelBaseServices)
        {
            if (viewModelBaseServices == null) { throw new Exception(nameof(ViewModelBaseServices) + " is null"); }

            //Logger = viewModelBaseServices.Logger;
            EventAggregator = viewModelBaseServices.EventAggregator;
            NavigationService = viewModelBaseServices.PageNavigationService;
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

       // public ILoggerFacade Logger { get; protected set; }
        public IEventAggregator EventAggregator { get; protected set; }
        public ExceptionHandler ExceptionHandler { get; private set; }
        public INavigationService NavigationService { get; protected set; }
    }

    public class ViewModelBaseServices
    {
        public ViewModelBaseServices(
            //ILoggerFacade logger,
            IEventAggregator eventAggregator,
            ExceptionHandler exceptionHandler,
            PageNavigationService pageNavigationService
            )
        {
            //Logger = logger;
            EventAggregator = eventAggregator;
            ExceptionHandler = exceptionHandler;
            PageNavigationService = pageNavigationService;
        }

        //public ILoggerFacade Logger { get; }
        public IEventAggregator EventAggregator { get; }
        public ExceptionHandler ExceptionHandler { get; }
        public PageNavigationService PageNavigationService { get; }
    }

    public class ViewModelBaseExceptionEvent : PubSubEvent<Exception>
    {

    }
}
