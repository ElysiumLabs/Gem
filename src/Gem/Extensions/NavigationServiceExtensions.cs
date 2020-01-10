using Gem.Bindings;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gem
{
    public static class NavigationServiceExtensions
    {
        public static async Task NavigateTAsync(this ViewModelBase viewModelBase, string name)
        {
            var r = await viewModelBase.NavigationService.NavigateAsync(name);
            ThrowIfNotSuccess(r);
        }

        public static async Task NavigateTAsync(this ViewModelBase viewModelBase, string name, INavigationParameters parameters)
        {
            var r = await viewModelBase.NavigationService.NavigateAsync(name, parameters);
            ThrowIfNotSuccess(r);
        }

        public static async Task NavigateTAsync(this ViewModelBase viewModelBase, Uri uri)
        {
            var r = await viewModelBase.NavigationService.NavigateAsync(uri);
            ThrowIfNotSuccess(r);
        }

        public static async Task NavigateTAsync(this ViewModelBase viewModelBase, Uri uri, INavigationParameters parameters)
        {
            var r = await viewModelBase.NavigationService.NavigateAsync(uri, parameters);
            ThrowIfNotSuccess(r);
        }

        public static async Task GoBackTAsync(this ViewModelBase viewModelBase, INavigationParameters parameters)
        {
            var r = await viewModelBase.NavigationService.GoBackAsync(parameters);
            ThrowIfNotSuccess(r);
        }

        public static async Task GoBackTAsync(this ViewModelBase viewModelBase)
        {
            var r = await viewModelBase.NavigationService.GoBackAsync();
            ThrowIfNotSuccess(r);
        }

        private static void ThrowIfNotSuccess(INavigationResult r)
        {
            if (!r.Success)
            {
                throw r.Exception;
            }
        }


    }
}
