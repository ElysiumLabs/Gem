using Prism;
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
using Prism.Events;
using Prism.Modularity;
using Prism.Ioc;
using System.Linq;
using Shiny;
using Microsoft.Extensions.DependencyInjection;
using Prism.Plugin.Popups;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using Gem.Bindings;

namespace Gem
{
    public abstract class ShinyGemApp<TGemApp> : ShinyStartup
        where TGemApp : GemApp
    {
        protected TGemApp CurrentApp { get; set; }

        public ShinyGemApp()
        {
        }

        public ShinyGemApp(Action<IServiceCollection> registerPlatformServices) : base(registerPlatformServices)
        {
        }

        public TGemApp GetApp(IPlatformInitializer platformInitializer = null)
        {
            if (CurrentApp == null)
            {
                CurrentApp =
                    (platformInitializer != null) ?
                        (TGemApp)Activator.CreateInstance(typeof(TGemApp), platformInitializer)
                        :
                        CurrentApp = (TGemApp)Activator.CreateInstance(typeof(TGemApp));
            }

            return CurrentApp;
        }

    }

    public abstract class GemApp : PrismApplication
    {
        private StyleKit _styleKit;

        protected AppInitializer AppInitializer;

        public IEventAggregator EventAggregator;

        protected GemApp() : base()
        {
        }

        protected GemApp(IPlatformInitializer platformInitializer) : base(platformInitializer)
        {
        }

        protected GemApp(IPlatformInitializer platformInitializer, bool setFormsDependencyResolver) : base(platformInitializer, setFormsDependencyResolver)
        {
        }

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
            
            EventAggregator = Container.Resolve<IEventAggregator>();
        }

        protected override async void OnStart()
        {
            base.OnStart();
            await AppInitializeInternal();

        }

        #region SUBSCRIPTION

        public virtual void EventSubscribe<T, U>(Action<U> action) where T : PubSubEvent<U>, new()
        {
            EventAggregator.GetEvent<T>().Subscribe(action);
        }

        public virtual void EventSubscribe<T, U>(Action<U> action, ThreadOption threadOption) where T : PubSubEvent<U>, new()
        {
            EventAggregator.GetEvent<T>().Subscribe(action, threadOption);
        }

        public virtual void EventSubscribe<T>(Action action, ThreadOption threadOption) where T : PubSubEvent, new()
        {
            EventAggregator.GetEvent<T>().Subscribe(action, threadOption);
        }

        public virtual void EventSubscribe<T>(Action action) where T : PubSubEvent, new()
        {
            EventAggregator.GetEvent<T>().Subscribe(action);
        }

        #endregion SUBSCRIPTION

        #region UNSUBSCRIPTION

        public virtual void EventUnsubscribe<T, U>(Action<U> action) where T : PubSubEvent<U>, new()
        {
            EventAggregator.GetEvent<T>().Unsubscribe(action);
        }

        public virtual void EventUnsubscribe<T>(Action action) where T : PubSubEvent, new()
        {
            EventAggregator.GetEvent<T>().Unsubscribe(action);
        }

        #endregion UNSUBSCRIPTION

        #region TRIGGER

        public virtual void EventTrigger<T, U>(U parameter) where T : PubSubEvent<U>, new()
        {
            EventAggregator.GetEvent<T>().Publish(parameter);
        }

        public virtual void EventTrigger<T>() where T : PubSubEvent, new()
        {
            EventAggregator.GetEvent<T>().Publish();
        }

        #endregion TRIGGER

        protected virtual async Task AppInitializeInternal()
        {
            EventAggregator.GetEvent<GemAppRestartEvent>().Unsubscribe(Restart);
            EventAggregator.GetEvent<GemAppRestartEvent>().Subscribe(Restart);
            AppInitializer = Container.Resolve<AppInitializer>();
            await AppInitializer.Load(null);
        }

        private async void Restart()
        {
            this.Initialize();
            await AppInitializeInternal();
        }

        protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterRequiredTypes(containerRegistry);
            ConfigureGem(containerRegistry);
        }

        private void ConfigureGem(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(Options);

            containerRegistry.RegisterSingleton<ILogger, GemLogger>();
            containerRegistry.RegisterSingleton<ExceptionHandler, DefaultExceptionHandler>();

            if (Options.UseAppCenter)
            {
                containerRegistry.RegisterManySingleton<AppCenterLogger>(typeof(ILogger), typeof(IAnalyticsService), typeof(ICrashesService));
                HookAppCenterNavigationEvents();
            }

            containerRegistry.Register<IGemNavigationService, GemNavigationService>();

            containerRegistry.RegisterSingleton(typeof(AppInitializer), Options.InitializerType);

            if (Options.SplashPageType == null)
            {
                Options.SplashPageType = typeof(ContentPage);
            }

            containerRegistry.RegisterForNavigation(Options.SplashPageType, Options.SplashPageType.Name);
            ViewModelLocationProvider.Register(Options.SplashPageType.ToString(), typeof(AppInitializer));

            containerRegistry.RegisterForNavigation<DefaultChangeEnvPage, DefaultChangeEnvViewModel>();
            containerRegistry.RegisterPopupNavigationService();
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

    }

    public class GemAppOptions
    {
        public Type InitializerType { get; set; } = typeof(DefaultInitializer);

        public Type SplashPageType { get; set; } = typeof(DefaultSplashPage);

        public bool UseAppCenter { get; set; }

    }

    public class GemAppRestartEvent : PubSubEvent
    {
    }
    
}
