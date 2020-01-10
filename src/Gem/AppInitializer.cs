using Gem.Bindings;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Gem.Views.Pages;
using Prism.AppModel;

namespace Gem
{
    public abstract class AppInitializer : ViewModelBase
    {
        private readonly GemAppOptions appOptions;
        private readonly ApplicationStore applicationStore;

        public AppInitializer(GemAppOptions appOptions, ApplicationStore applicationStore, ViewModelBaseServices viewModelBaseServices) : base(viewModelBaseServices)
        {
            this.appOptions = appOptions;
            this.applicationStore = applicationStore;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            //override navigation events cause we "Load" first
        }
        
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            //override navigation events cause we "Load" first
        }

        public override void Initialize(INavigationParameters parameters)
        {
            //override navigation events cause we "Load" first
        }

        public override async Task Load(INavigationParameters parameters)
        {
            try
            {
                BusyLoader.SetIsLoading(true);

                if (appOptions.UseSplashPage)
                {
                    await this.NavigateTAsync(appOptions.SplashPageType.Name);
                }

                await InitializeAsync();

                BusyLoader.SetIsLoading(false);

                var qq = (NavigationService as PageNavigationService).GetNavigationUriPath();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public abstract Task InitializeAsync();
      
    }
}
