using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gem
{
	public class ViewModelLoader : BindableBase
	{
		private bool isLoading;

		public bool IsLoading
		{
			get { return isLoading; }
			set { SetProperty(ref isLoading, value); RaiseIsLoadingChanged(isLoading); }
		}

		private string loadingMessage;

		public string LoadingMessage
		{
			get { return loadingMessage; }
			set { SetProperty(ref loadingMessage, value); }
		}

		public event EventHandler<bool> IsLoadingChanged;

		private void RaiseIsLoadingChanged(bool isLoading)
		{
			IsLoadingChanged?.Invoke(this, isLoading);
		}

		public void SetIsLoading(bool loading = true, string message = "")
		{
			IsLoading = loading;
			LoadingMessage = message;
		}
	}
}
