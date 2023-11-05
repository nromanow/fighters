using Core.Meta.Menu.Api;
using System;
using UnityEngine.SceneManagement;

namespace Core.Meta.Menu.App {
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
