using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Text;
using Gem.Views.Pages;
using Prism.Logging;
using System.Threading.Tasks;
using Prism.Navigation;
using Xamarin.Forms;
using Prism.Mvvm;
using Gem.UX;
using Prism.Logging.AppCenter;
using Unity;
using Prism.Container.Extensions;
using System.Diagnostics;
using Gem.AppCenter;
using Gem.Diagnostics;

namespace Gem
{
    public abstract class GemApp : PrismApplication
    {
        private StyleKit _styleKit;

        protected AppInitializer AppInitializer;

        public GemAppOptions Options { get; private set; } = new GemAppOptions();

        public StyleKit StyleKit
        {
            get { return _styleKit; }
            set
            {
                _styleKit = value;
                UpdateAppStyle();
            }
        }

        public abstract void Configure(GemAppOptions options);

        protected override void Initialize()
        {
            Configure(Options);
            base.Initialize();
        }

        protected override async void OnStart()
        {
            base.OnStart();
            await AppInitializeInternal();
        }

        protected virtual async Task AppInitializeInternal()
        {
            AppInitializer = Container.Resolve<AppInitializer>();
            await AppInitializer.Load(null);
        }


        protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterRequiredTypes(containerRegistry);
            ConfigureGem(containerRegistry);
        }

        private void ConfigureGem(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(Options);

            containerRegistry.RegisterSingleton<ILoggerFacade, GemLogger>();
            containerRegistry.RegisterSingleton<ExceptionHandler, DefaultExceptionHandler>();

            if (Options.UseAppCenter)
            {
                containerRegistry.RegisterManySingleton<AppCenterLogger>(typeof(ILogger), typeof(ILoggerFacade), typeof(IAnalyticsService), typeof(ICrashesService));
                HookAppCenterNavigationEvents();
            }

            containerRegistry.RegisterSingleton(typeof(AppInitializer), Options.InitializerType);
            ViewModelLocationProvider.Register(Options.SplashPageType.ToString(), typeof(AppInitializer));

            if (Options.UseSplashPage)
            {
                containerRegistry.RegisterForNavigation(Options.SplashPageType, Options.SplashPageType.Name);
                
            }


        }

        protected virtual void HookAppCenterNavigationEvents()
        {
            PageAppearing += (s, p) =>
            {
                try
                {
                    Container.Resolve<IAnalyticsService>().TrackEvent("Nav:" + p.GetType().Name);
                }
                catch (Exception ex)
                {
                    Trace.Fail(ex.Message);
                }
            };
        }

        protected virtual void UpdateAppStyle()
        {
            StyleKit?.Apply(this);
        }

        protected override IContainerExtension CreateContainerExtension()
        {
            return PrismContainerExtension.Current ?? PrismContainerExtension.Create();
        }
    }

    public class GemAppOptions
    {
        public Type InitializerType { get; set; } = typeof(DefaultInitializer);

        public bool UseSplashPage { get; set; } = true;

        public Type SplashPageType { get; set; } = typeof(DefaultSplashPage);

        public bool UseAppCenter { get; set; }


    }
}
