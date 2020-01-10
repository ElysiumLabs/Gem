using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GemSandApp.Views.Shell
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationShell : NavigationPage, INavigationPageOptions, IDestructible
    {
        public NavigationShell()
        {
            InitializeComponent();
        }

        public bool ClearNavigationStackOnNavigation
        {
            get { return false; }
        }

        public void Destroy() { }
    }
}