using Gem.Bindings;
using Prism.Navigation;
using Prism.Plugin.Popups;
using Prism.Xaml;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Gem
{
    public static class NavigationServiceExtensions
    {
        public static async Task NavigateTAsync<T>(this ViewModelBase viewModelBase)
        {
            var r = typeof(T).BaseType == typeof(PopupPage) ?
                await viewModelBase.NavigationService.Popup.NavigateAsync(typeof(T).Name)
                : await viewModelBase.NavigationService.NavigateAsync(typeof(T).Name);
            ThrowIfNotSuccess(r);
        }

        public static async Task NavigateTAsync<T>(this ViewModelBase viewModelBase, INavigationParameters parameters)
        {
            var r = typeof(T).BaseType == typeof(PopupPage) ?
                await viewModelBase.NavigationService.Popup.NavigateAsync(typeof(T).Name, parameters)
                : await viewModelBase.NavigationService.NavigateAsync(typeof(T).Name, parameters);
            ThrowIfNotSuccess(r);
        }

        public static async Task NavigateTAsync<U,T>(this ViewModelBase viewModelBase) where U : NavigationPage
        {
            var r = await viewModelBase.NavigationService.NavigateAsync(typeof(U).Name + "/"+ typeof(T).Name);
            ThrowIfNotSuccess(r);
        }

        public static async Task NavigateTAsync<U,T>(this ViewModelBase viewModelBase, INavigationParameters parameters) where U : NavigationPage
        {
            var r = await viewModelBase.NavigationService.NavigateAsync(typeof(U).Name + "/" + typeof(T).Name, parameters);
            ThrowIfNotSuccess(r);
        }

        public static async Task NavigateTAsync<T>(this ViewModelBase viewModelBase, string prefix)
        {
            var r = await viewModelBase.NavigationService.NavigateAsync(typeof(T).Name + $"/{prefix}");
            ThrowIfNotSuccess(r);
        }

        public static async Task NavigateTAsync<T>(this ViewModelBase viewModelBase, string prefix, INavigationParameters parameters)
        {
            var r = await viewModelBase.NavigationService.NavigateAsync(typeof(T).Name + $"/{prefix}", parameters);
            ThrowIfNotSuccess(r);
        }

        public static async Task NavigateTAsync<U,T>(this ViewModelBase viewModelBase, string prefix) where U : NavigationPage
        {
            var r = await viewModelBase.NavigationService.NavigateAsync(typeof(U).Name + "/" + typeof(T).Name + $"/{prefix}");
            ThrowIfNotSuccess(r);
        }

        public static async Task NavigateTAsync<U,T>(this ViewModelBase viewModelBase, string prefix, INavigationParameters parameters) where U : NavigationPage
        {
            var r = await viewModelBase.NavigationService.NavigateAsync(typeof(U).Name + "/" + typeof(T).Name + $"/{prefix}", parameters);
            ThrowIfNotSuccess(r);
        }
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
