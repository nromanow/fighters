using Core.Gameplay.Menu.Api;
using System;
using UnityEngine.SceneManagement;

namespace Core.Gameplay.Menu.App {
	public class MenuLoaderService : IMenuLoaderService {
		public void LoadMenu (Action onLoaded) {
			SceneManager
				.LoadSceneAsync("Scenes/MainMenuScene")
				.completed += operation => {
				onLoaded?.Invoke();
			};
		}
	}
}
