using Gem.Bindings;
using Gem.Views.Pages;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GemSandApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }
    }

    public class HomePageViewModel : ViewModelBase
    {
        public HomePageViewModel(ViewModelBaseServices viewModelBaseServices) : base(viewModelBaseServices)
        {
        }

        public override async Task Load(INavigationParameters parameters)
        {
        }

        public Command ChangeEnv => new Command(async () => 
        {
            await NavigationService.NavigateAsync(nameof(DefaultChangeEnvPage));
        });
    }
}