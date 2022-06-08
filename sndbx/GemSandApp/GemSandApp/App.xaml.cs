using Gem;
using Gem.UX;
using GemSandApp.Utils;
using GemSandApp.Views.Pages;
using GemSandApp.Views.Shell;
using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using Shiny;
using Shiny.Notifications;
using Shiny.Push;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ShinyApplication(
    ShinyStartupTypeName = "GemSandApp.ShinyApp",
    XamarinFormsAppTypeName = "GemSandApp.App"
)]

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

        //protected override void Initialize()
        //{
        //    //base.Initialize();
        //}

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
        public ShinyApp(Action<IServiceCollection> registerPlatformServices) : base(registerPlatformServices)
        {
        }

        public override void ConfigureServices(IServiceCollection services, IPlatform platform)
        {
            services.UsePushAzureNotificationHubs<SandPushDelegate>(
                "Endpoint=sb://gemsandapp-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=F7r9hS7VjZ+ygFDvSZ5qkm2BFHLydq+DrLssC3TEgt0=", 
                "gemsandapp");

           //services.UseNotifications<NotificationDelegate>();
        }
    }

    //public class NotificationDelegate : INotificationDelegate
    //{
    //    private readonly INotificationManager notificationManager;

    //    public NotificationDelegate(INotificationManager notificationManager)
    //    {
    //        this.notificationManager = notificationManager;
    //    }

    //    public async Task OnEntry(NotificationResponse response)
    //    {
    //        App.Current.MainPage.BackgroundColor = Color.Red;
    //    }
    //}

    public class SandPushDelegate : IPushDelegate
    {
        //private readonly INotificationManager notificationManager;

        //public SandPushDelegate(
        //    INotificationManager notificationManager
        //    )
        //{
        //    this.notificationManager = notificationManager;
        //}

        public async Task OnEntry(PushNotification data) => Debug.Write("OnEntry");

        public async Task OnReceived(PushNotification data) => Debug.Write("OnReceived");

        //public async Task OnReceived(PushNotification data)
        //{
        //    //toastService.LongAlert("OnReceived");

        //    Debug.Write("OnReceived");
        //    //var n = new Shiny.Notifications.Notification()
        //    //{
        //    //    Title = data.Data["property1"].ToString(),
        //    //    Message = data.Data["property2"].ToString()
        //    //};

        //    //await notificationManager.Send(n);
        //}

        public async Task OnTokenRefreshed(string token) => Debug.Write("OnTokenRefreshed");

    }
}
