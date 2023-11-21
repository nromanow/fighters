using Core.Meta.GameLoader.Api;
using UnityEngine.SceneManagement;

namespace Core.Meta.GameLoader.App {
	public class GameLoaderService : IGameLoaderService {
		public void LoadGame () {
			SceneManager.LoadScene("Scenes/GameStartup");
		}
	}
}
