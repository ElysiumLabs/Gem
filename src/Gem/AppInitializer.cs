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

        public AppInitializer(GemAppOptions appOptions, ApplicationStore applicationStore, PageNavigationService navigationService) : base(navigationService)
        {
            this.appOptions = appOptions;
            this.applicationStore = applicationStore;
        }

        public abstract Task InitializeAsync();

        public virtual async Task InitializeInternalAsync()
        {
            BusyLoader.SetIsLoading(true, "Iniciando, aguarde...");

            if (appOptions.UseSplashPage)
            {
                var r = await NavigationService.NavigateAsync(appOptions.SplashPageType.Name);
            }

            await InitializeAsync();

            BusyLoader.SetIsLoading(false);
        }

      
    }
}
