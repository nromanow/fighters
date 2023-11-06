using Core.Meta.GameStart.Api;
using Core.Meta.Menu.Api;

namespace Core.Meta.GameStart.App {
	public class GameStartService : IGameStartService {
		private readonly IMenuLoaderService _menuLoaderService;
		private readonly IMenuScreenService _menuScreenService;

		public GameStartService (IMenuLoaderService menuLoaderService, IMenuScreenService menuScreenService) {
			_menuLoaderService = menuLoaderService;
			_menuScreenService = menuScreenService;
		}

		public void StartGame () {
			_menuLoaderService.LoadMenu(() => { _menuScreenService.OpenMenuScreen(); });
		}
	}
}
