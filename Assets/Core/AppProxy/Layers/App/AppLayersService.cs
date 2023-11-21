using Core.AppProxy.Layers.Api;
using UnityEngine.SceneManagement;

namespace Core.AppProxy.Layers.App {
	public class AppLayersService : IAppLayersService {
		public void StartupMetaLayer () {
			SceneManager.LoadScene("Scenes/MetaStartup");
		}

		public void StartupGameplayLayer () {
			SceneManager.LoadScene("Scenes/GameStartup");
		}
	}
}
