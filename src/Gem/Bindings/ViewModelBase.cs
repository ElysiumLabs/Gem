using Plugin.Connectivity;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gem.Bindings
{
    //public abstract class AppViewModelBase : ViewModelBase
    //{
    //    public AppViewModelBase() : base(null)
    //    {
    //        GemApp<>.Current
    //        NavigationService = ((GemApp)GemApp.Current).GetNavigationService();
    //    }
    //}

    public abstract class ViewModelBase : BindableBase, IInitialize, INavigationAware
    {

        public ViewModelBase(PageNavigationService navigationService)
        {
            NavigationService = navigationService;
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

        public virtual void HandleException(Exception ex) { }

        public virtual Task Load(INavigationParameters parameters)
        {
            return Task.FromResult(0);
        }

        private ViewModelLoader busyLoader = new ViewModelLoader();

        public ViewModelLoader BusyLoader
        {
            get { return busyLoader; }
            set { SetProperty(ref busyLoader, value); }
        }

        public INavigationService NavigationService { get; protected set; }
    }
}
