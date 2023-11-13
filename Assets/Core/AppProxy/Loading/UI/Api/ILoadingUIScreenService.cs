using Core.AppProxy.Loading.UI.ViewModels;

namespace Core.AppProxy.Loading.UI.Api {
	public interface ILoadingUIScreenService {
		void OpenLoadingScreen (LoadingScreenViewModel viewModel);

		void CloseLoadingScreen ();
	}
}
