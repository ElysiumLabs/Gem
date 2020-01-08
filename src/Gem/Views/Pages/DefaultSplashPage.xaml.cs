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
    public partial class DefaultSplashPage : ContentPage
    {
        public const string NavigationName = "GemDefaultSplashPage";

        public DefaultSplashPage()
        {
            InitializeComponent();
        }
    }
}