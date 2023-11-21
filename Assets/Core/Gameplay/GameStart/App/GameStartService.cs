using Core.Gameplay.GameStart.Api;
using Core.Gameplay.Menu.Api;

namespace Core.Gameplay.GameStart.App {
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
