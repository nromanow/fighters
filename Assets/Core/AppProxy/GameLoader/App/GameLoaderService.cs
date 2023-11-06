using Core.AppProxy.GameLoader.Api;
using UnityEngine.SceneManagement;

namespace Core.AppProxy.GameLoader.App {
	public class GameLoaderService : IGameLoaderService {
		public void LoadGame () {
			SceneManager.LoadScene("Scenes/GameStartup");
		}
	}
}
