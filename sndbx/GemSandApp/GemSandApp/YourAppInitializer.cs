using Gem;
using Gem.Bindings;
using GemSandApp.Views.Pages;
using Prism.AppModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GemSandApp
{
    public class YourAppInitializer : AppInitializer
    {
        public YourAppInitializer(GemAppOptions appOptions, ApplicationStore applicationStore, ViewModelBaseServices viewModelBaseServices) : base(appOptions, applicationStore, viewModelBaseServices)
        {
        }

        public override async Task InitializeAsync()
        {

            //here u put your app initialization logic
            
            BusyLoader.SetIsLoading(true, "Your message of loader");

            //someload
            await Task.Delay(3000);

            await this.NavigateTAsync(nameof(HomePage));

            BusyLoader.SetIsLoading(false);

        }
    }
}
