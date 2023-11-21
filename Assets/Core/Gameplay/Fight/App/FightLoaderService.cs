using Core.Gameplay.Fight.Api;
using System;
using UnityEngine.SceneManagement;

namespace Core.Gameplay.Fight.App {
	public class FightLoaderService : IFightLoaderService {
		public void LoadFightScene (Action onLoaded) {
			SceneManager
				.LoadSceneAsync("Scenes/FightScene")
				.completed += operation => { onLoaded?.Invoke(); };
		}
	}
}
