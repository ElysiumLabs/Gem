using Gem;
using Gem.UX;
using GemSandApp.Views.Pages;
using GemSandApp.Views.Shell;
using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using Shiny;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GemSandApp
{
    public class ShinySandAppInitializer : ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection services, IPlatform platform)
        {
        }
    }

    public partial class App : ShinyGemApp<ShinySandAppInitializer>
    {
        public App()
        {
        }

        public App(Prism.IPlatformInitializer platformInitializer) : base(platformInitializer)
        {
        }

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
}
