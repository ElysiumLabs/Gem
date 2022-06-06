using Gem;
using Gem.UX;
using GemSandApp.Views.Pages;
using GemSandApp.Views.Shell;
using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using Shiny;
using Shiny.Push;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GemSandApp
{
    public partial class App : GemApp
    {
        public override void Configure(GemAppOptions options)
        {
            options.InitializerType = typeof(YourAppInitializer);
            //options.SplashPageType = typeof(YourCustomXamlSplashPage); if u want a custom splash
            //options.SplashPageType = null; if u don't want any splash

            //Hint, generate your styles here: https://material.io/resources/color/
            StyleKit = new MaterialStyleKit()
            {
                PrimaryColor = Color.FromHex("#FF303f9f"),
                SecondaryColor = Color.FromHex("#FF8e24aa"),
                TextColorOfPrimaryColor = Color.FromHex("#ffffff"),
                BackgroundColorPage = Color.FromHex("#ffffff")
            };


        }

        protected override void OnInitialized()
        {
            InitializeComponent();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationShell>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();

        }
    }

    public class ShinyApp : ShinnyGemApp<App>
    {
        public override void ConfigureServices(IServiceCollection services, IPlatform platform)
        {
            base.ConfigureServices(services, platform);

            //services.UsePushAzureNotificationHubs<SandPushDelegate>("Your Listener Connectionstring", "Your Hub Name");
        }
    }

    public class SandPushDelegate : IPushDelegate
    {
        private readonly App app;

        public SandPushDelegate(App app)
        {
            this.app = app;
        }
        public async Task OnEntry(PushNotification data)
        {
        }

        public async Task OnReceived(PushNotification data)
        {
        }

        public async Task OnTokenRefreshed(string token)
        {
        }
    }
}
