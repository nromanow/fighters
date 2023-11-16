using Core.AppProxy.Loading.Api;
using Core.AppProxy.Loading.UI.Api;
using Core.AppProxy.Loading.UI.ViewModels;
using System;
using UniRx;

namespace Core.AppProxy.Loading.App {
	public class LoadingScreenService : ILoadingScreenService, IDisposable {
		private readonly ILoadingUIScreenService _uiService;
		private readonly CompositeDisposable _disposable = new();

		public LoadingScreenService (ILoadingUIScreenService uiService) {
			_uiService = uiService;
		}

		public void ShowLoadingScreen (IObservable<int> progress = default, int staticProgress = 0) {
			var viewModel = new LoadingScreenViewModel();

			viewModel.UpdateProgress(staticProgress);
			
			progress?
				.Subscribe(viewModel.UpdateProgress)
				.AddTo(_disposable);

			_uiService.OpenLoadingScreen(viewModel);
		}

		public void HideLoadingScreen () {
			_uiService.CloseLoadingScreen();
		}

		public void Dispose() {
			_disposable?.Dispose();
		}
	}
}
