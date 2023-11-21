using Core.Meta.Loading.UI.ViewModels;

namespace Core.Meta.Loading.UI.Api {
	public interface ILoadingUIScreenService {
		void OpenLoadingScreen (LoadingScreenViewModel viewModel);

		void CloseLoadingScreen ();
	}
}
