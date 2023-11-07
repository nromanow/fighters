using Core.AppProxy.Loading.Api;
using Core.AppProxy.Loading.UI.Api;

namespace Core.AppProxy.Loading.App {
	public class LoadingScreenService : ILoadingScreenService {
		private readonly ILoadingUIScreenService _uiService;

		public LoadingScreenService (ILoadingUIScreenService uiService) {
			_uiService = uiService;
		}

		public void ShowLoadingScreen () {
			_uiService.OpenLoadingScreen();
		}

		public void HideLoadingScreen () {
			_uiService.CloseLoadingScreen();
		}
	}
}
