using Gem.Bindings;
using Prism.AppModel;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gem.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DefaultChangeEnvPage : ContentPage
    {
        public DefaultChangeEnvPage()
        {
            InitializeComponent();
        }
    }

    public class DefaultChangeEnvViewModel : ViewModelBase
    {
        private readonly ApplicationStore applicationStore;
        private readonly AppInitializer appInitializer;
        private readonly GemApp gemApp;

        public DefaultChangeEnvViewModel(ViewModelBaseServices viewModelBaseServices, ApplicationStore applicationStore, AppInitializer appInitializer) : base(viewModelBaseServices)
        {
            this.applicationStore = applicationStore;
            this.appInitializer = appInitializer;
        }

        private string GetCurrentEnv()
        {
            var env =
#if RELEASE
                "prod";
#else
                "prod";
#endif

            try
            {
                if (applicationStore.Properties.ContainsKey("env"))
                {
                    env = applicationStore.Properties["env"].ToString();
                }
                else
                {
                    applicationStore.Properties["env"] = env;
                    applicationStore.SavePropertiesAsync();
                }
            }
            catch
            {
            }

            return env;
        }

        public override async Task Load(INavigationParameters parameters)
        {
            try
            {
                BusyLoader.SetIsLoading(true);

                var env = GetCurrentEnv();
                switch (env)
                {
                    case "dev":
                        CurrentEnv = "Desenvolvimento";
                        break;

                    case "test":
                        CurrentEnv = "Teste";
                        break;

                    case "stag":
                        CurrentEnv = "Homologação";
                        break;

                    case "prod":
                        CurrentEnv = "Produção";
                        break;
                }
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

        public Command<string> ChangeEnvCommand => new Command<string>(async (s) =>
        {
            try
            {
                BusyLoader.SetIsLoading(true, "Alterando ambiente");

                applicationStore.Properties["env"] = s;

                await applicationStore.SavePropertiesAsync();

                EventAggregator.GetEvent<GemAppRestartEvent>().Publish();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally 
            { 
                BusyLoader.SetIsLoading(false); 
            }
        });

        private string _currentEnv;
        public string CurrentEnv
        {
            get { return _currentEnv; }
            set { SetProperty(ref _currentEnv, value); }
        }
    }
}